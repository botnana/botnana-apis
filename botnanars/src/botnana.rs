use data_pool::DataPool;
use program::Program;
use serde_json;
use std::{self,
          boxed::Box,
          collections::{HashMap, VecDeque},
          ffi::CStr,
          os::raw::{c_char, c_void},
          str,
          sync::{mpsc::{self, TryRecvError},
                 Arc, Mutex},
          thread};
use url;
use ws::{self, connect, util::Token, CloseCode, Error, ErrorKind, Handler, Handshake, Message,
         Result};
const WS_TIMEOUT_TOKEN: Token = Token(1);
const WS_WATCHDOG_PERIOD_MS: u64 = 10_000;
const VERSION: &'static str = env!("CARGO_PKG_VERSION");

/// Callback Handler
struct CallbackHandler {
    /// 用來回傳指標給使用者
    pointer: *mut c_void,
    /// callback 函式指標
    callback: Box<Fn(*mut c_void, *const c_char) + Send>,
}

unsafe impl Send for CallbackHandler {}

/// Callback Handler for tag
struct TagCallbackHandler {
    /// 執行次數
    count: u32,
    /// 用來回傳指標給使用者
    pointer: *mut c_void,
    /// callback 函式指標
    /// *mut c_void: 使用者設定的指標
    /// u32: position
    /// u32: channel
    /// u32: value (string)
    callback: Box<Fn(*mut c_void, u32, u32, *const c_char) + Send>,
}

unsafe impl Send for TagCallbackHandler {}

/// Botnana
#[repr(C)]
#[derive(Clone)]
pub struct Botnana {
    ip: Arc<Mutex<String>>,
    port: Arc<Mutex<u16>>,
    user_sender: Arc<Mutex<Option<mpsc::Sender<Message>>>>,
    ws_out: Arc<Mutex<Option<ws::Sender>>>,
    handlers: Arc<Mutex<HashMap<String, Vec<TagCallbackHandler>>>>,
    /// 用來存放 forth 命令的 buffer
    scripts_buffer: Arc<Mutex<VecDeque<String>>>,
    /// 在 polling thread裡，每次從 scripts buffer 裡拿出 scripts_pop_count 個暫存命令送出給 Botnana motion server
    scripts_pop_count: Arc<Mutex<u32>>,
    /// poll thread 啟動的時間
    poll_interval_ms: Arc<Mutex<u64>>,
    is_connected: Arc<Mutex<bool>>,
    is_connecting: Arc<Mutex<bool>>,
    on_open_cb: Arc<Mutex<Option<CallbackHandler>>>,
    on_error_cb: Arc<Mutex<Option<CallbackHandler>>>,
    on_send_cb: Arc<Mutex<Option<CallbackHandler>>>,
    on_message_cb: Arc<Mutex<Option<CallbackHandler>>>,
    pub data_pool: Arc<Mutex<DataPool>>,
    pub(crate) internal_handlers:
        Arc<Mutex<HashMap<String, Box<Fn(&mut DataPool, usize, usize, &str) + Send>>>>,
    pub(crate) init_queries: Arc<Mutex<Vec<String>>>,
    pub(crate) cyclic_queries: Arc<Mutex<Vec<String>>>,
    last_query: Arc<Mutex<usize>>,
    query_count: Arc<Mutex<usize>>,
}

impl Botnana {
    /// New
    pub fn new() -> Botnana {
        Botnana {
            ip: Arc::new(Mutex::new("192.168.7.2".to_string())),
            port: Arc::new(Mutex::new(3012)),
            user_sender: Arc::new(Mutex::new(None)),
            ws_out: Arc::new(Mutex::new(None)),
            handlers: Arc::new(Mutex::new(HashMap::new())),
            scripts_buffer: Arc::new(Mutex::new(VecDeque::with_capacity(1024))),
            scripts_pop_count: Arc::new(Mutex::new(8)),
            poll_interval_ms: Arc::new(Mutex::new(10)),
            is_connected: Arc::new(Mutex::new(false)),
            is_connecting: Arc::new(Mutex::new(false)),
            on_open_cb: Arc::new(Mutex::new(None)),
            on_error_cb: Arc::new(Mutex::new(None)),
            on_send_cb: Arc::new(Mutex::new(None)),
            on_message_cb: Arc::new(Mutex::new(None)),
            data_pool: Arc::new(Mutex::new(DataPool::new())),
            internal_handlers: Arc::new(Mutex::new(HashMap::new())),
            init_queries: Arc::new(Mutex::new(Vec::new())),
            cyclic_queries: Arc::new(Mutex::new(Vec::new())),
            last_query: Arc::new(Mutex::new(0)),
            query_count: Arc::new(Mutex::new(3)),
        }
    }

    /// Clone
    pub fn clone(botnana: Botnana) -> Botnana {
        botnana.clone()
    }

    /// Set IP
    pub fn set_ip(&mut self, ip: &str) -> String {
        if let Ok(_) = url::Url::parse(&("ws://".to_owned() + ip + ":" + &self.port().to_string()))
        {
            *self.ip.lock().expect("") = ip.to_string();
        }
        self.ip()
    }

    /// Set port
    pub fn set_port(&mut self, port: u16) -> u16 {
        if let Ok(_) =
            url::Url::parse(&("ws://".to_owned() + self.ip().as_str() + ":" + &port.to_string()))
        {
            *self.port.lock().expect("") = port;
        }
        self.port()
    }

    /// IP
    pub fn ip(&self) -> String {
        self.ip.lock().expect("").to_string()
    }

    /// Port
    pub fn port(&self) -> u16 {
        *self.port.lock().expect("")
    }

    /// URL
    pub fn url(&self) -> String {
        "ws://".to_owned() + self.ip().as_str() + ":" + &self.port().to_string()
    }

    /// Is connected ?
    pub fn is_connected(&self) -> bool {
        *self.is_connected.lock().expect("")
    }

    /// Set on_open callback
    pub fn set_on_open_cb<F>(&mut self, pointer: *mut c_void, cb: F)
    where
        F: Fn(*mut c_void, *const c_char) + Send + 'static,
    {
        *self.on_open_cb.lock().expect("set_on_open_cb") = Some(CallbackHandler {
            pointer: pointer,
            callback: Box::new(cb),
        });
    }

    /// Set on_error callback
    pub fn set_on_error_cb<F>(&mut self, pointer: *mut c_void, cb: F)
    where
        F: Fn(*mut c_void, *const c_char) + Send + 'static,
    {
        *self.on_error_cb.lock().expect("set_on_error_cb") = Some(CallbackHandler {
            pointer: pointer,
            callback: Box::new(cb),
        });
    }

    /// Connect to botnana
    pub fn connect(&mut self) {
        // 如果已經在等待連線就跳出
        if *self.is_connecting.lock().expect("connecting") {
            return;
        } else {
            *self.is_connecting.lock().expect("connecting") = true;
        }

        // 從 user thread 送到 ws client thread，將指令透過 ws client thread 送到 motion server
        let (user_sender, client_receiver) = mpsc::channel();

        // 從 ws client thread 送到 user thread，將收到的資料送到 user thread
        let (client_sender, user_receiver) = mpsc::channel();

        // 從 poll thread 送到指令到 ws client thread，用來維持 WS 連線
        let (poll_sender, poll_receiver) = mpsc::channel();

        // 用來傳送 ws::sender
        let (thread_tx, thread_rx) = mpsc::channel();

        *self.user_sender.lock().expect("Set user sender") = Some(user_sender);
        let mut botnana = self.clone();

        thread::Builder::new()
            .name("Try Connection".to_string())
            .spawn(move || {
                // 連線到 WS Server，因為 ws::connect 會被 block，所以要使用 thread 來連線才能執行後續的程序。
                let bna = botnana.clone();
                if let Err(e) =
                    thread::Builder::new()
                        .name("WS_CLIENT".to_string())
                        .spawn(move || {
                            // connect ws server
                            let _ = connect(bna.url(), |sender| Client {
                                ws_out: sender,
                                sender: client_sender.clone(),
                                thread_tx: thread_tx.clone(),
                                on_error_cb: bna.on_error_cb.clone(),
                                is_watchdog_refreshed: false,
                            });
                            // 直到 WS Client Event loop 結束， 才會執行以下程式。
                            *bna.user_sender.lock().expect("Exit WS Event Loop") = None;
                            *bna.ws_out.lock().expect("Exit WS Event Loop") = None;
                            bna.scripts_buffer
                                .lock()
                                .expect("Exit WS Event Loop")
                                .clear();
                        })
                {
                    botnana
                        .execute_on_error_cb(&format!("Can't create WS CLIENT thread ({})\n", e));
                }

                // 等待 WS 連線後，將 ws_sender 回傳
                if let Ok(ws_sender) = thread_rx.recv() {
                    let ws_out = ws_sender.clone();
                    // 使用 thread 處理 user 傳過來的 message，透過 ws 送到 botnana
                    thread::spawn(move || {
                        loop {
                            // 如果從 mpsc channel 接收到 user 傳過來的指令，就透過 WebSocket 送到 Server
                            if let Ok(msg) = client_receiver.recv() {
                                // 由 Client handler 處理錯誤
                                if ws_out.send(msg).is_err() {
                                    break;
                                }
                            } else {
                                break;
                            }
                        }
                    });

                    *botnana.ws_out.lock().expect("Set WS sender") = Some(ws_sender.clone());
                    let mut bna = botnana.clone();
                    // 使用 thread 處理 WebSocket Client 透過 mpsc 傳過來的 message
                    if let Err(e) = thread::Builder::new()
                        .name("MESSAGE_PROCESSOR".to_string())
                        .spawn(move || loop {
                            if let Ok(msg) = user_receiver.recv() {
                                let msg = msg.trim_start().trim_start_matches('|');
                                bna.handle_message(msg);
                            } else {
                                // 讓 client receiver 與 poll receiver 知道出現問題了
                                // 故意送任一個訊息，讓 user receiver 與 poll receiver 收到訊息
                                // 依 client receiver 與 poll receiver 的機制進行後續處理。
                                if let Some(ref sender) = *bna.user_sender.lock().expect("") {
                                    let _ = sender.send(Message::Text(" ".to_string()));
                                }
                                let _ = poll_sender.send(Message::Text(" ".to_string()));
                                break;
                            }
                        })
                    {
                        botnana.execute_on_error_cb(&format!(
                            "Can't create MESSAGE_PROCESSOR thread ({})\n",
                            e
                        ));
                    }

                    // poll thread
                    // 不使用 ws::Handler on_timeout 的原因是在測試時產生 timeout 事件最少需要 200 ms
                    let mut bna = botnana.clone();
                    if let Err(e) =
                        thread::Builder::new()
                            .name("POLL".to_string())
                            .spawn(move || {
                                let poll_msg = Message::Text(
                                    "{\"jsonrpc\":\"2.0\",\"method\":\"motion.poll\"}".to_owned(),
                                );
                                loop {
                                    let interval =
                                        *bna.poll_interval_ms.lock().expect("poll thread");
                                    thread::sleep(std::time::Duration::from_millis(interval));
                                    // 當 WS 連線有問題時，用來接收通知，正常時應該不會收到東西
                                    match poll_receiver.try_recv() {
                                        Ok(_) | Err(TryRecvError::Disconnected) => {
                                            break;
                                        }
                                        _ => {}
                                    }
                                    let mut no_command = true;

                                    if bna.scripts_buffer_len() > 0 {
                                        bna.pop_scripts_buffer();
                                        no_command = false;
                                    }

                                    if bna.send_internal_query_command() > 0 {
                                        no_command = false;
                                    }

                                    if no_command {
                                        if ws_sender.send(poll_msg.clone()).is_err() {
                                            break;
                                        }
                                    }
                                }
                            })
                    {
                        botnana.execute_on_error_cb(&format!("Can't create POLL thread ({})\n", e));
                    }
                    *botnana.is_connected.lock().expect("Exit WS Event Loop") = true;
                    // 建制成功後呼叫 on_open callback
                    botnana.execute_on_open_cb();
                }
            })
            .expect("Create Try Connection Thread");
    }

    /// Disconnect
    pub fn disconnect(&mut self) {
        if let Some(ref mut ws_out) = *self.ws_out.lock().expect("disconnect") {
            let _ = ws_out.close(CloseCode::Normal);
        }
    }

    /// Send message to mpsc channel
    pub fn send_message(&mut self, msg: &str) {
        if self.has_ws_sender() {
            self.execute_on_send_cb(msg);
            let mut error_info = Ok(());
            if let Some(ref sender) = *self.user_sender.lock().expect("send message") {
                error_info = sender.send(Message::Text(msg.to_string()));
            }

            if let Err(e) = error_info {
                self.execute_on_error_cb(&format!("Send Message Error: {}\n", e));
            }
        }
    }

    /// Evaluate (立即送出)
    pub fn evaluate(&mut self, script: &str) {
        if self.has_ws_sender() {
            if let Ok(x) = serde_json::to_value(script) {
                let msg = r#"{"jsonrpc":"2.0","method":"script.evaluate","params":{"script":"#
                    .to_owned()
                    + &x.to_string()
                    + r#"}}"#;
                self.execute_on_send_cb(&msg);
                if let Some(ref sender) = *self.user_sender.lock().expect("evaluate") {
                    sender
                        .send(Message::Text(msg.to_string()))
                        .expect("send_message");
                }
            }
        }
    }

    /// Send script to command buffer （將命令送到緩衝區）
    pub fn send_script_to_buffer(&mut self, script: &str) {
        if self.has_ws_sender() {
            self.scripts_buffer
                .lock()
                .expect("")
                .push_back(script.to_owned() + "\n");
        }
    }

    /// Set poll interval_ms
    pub fn set_poll_interval_ms(&mut self, interval: u64) {
        *self.poll_interval_ms.lock().expect("set_scripts_pop_count") = interval;
    }

    /// Set scripts pop count
    pub fn set_scripts_pop_count(&mut self, count: u32) {
        *self
            .scripts_pop_count
            .lock()
            .expect("set_scripts_pop_count") = count;
    }

    /// Scripts buffer length
    pub fn scripts_buffer_len(&self) -> usize {
        self.scripts_buffer.lock().expect("").len()
    }

    /// Pop scripts buffer
    fn pop_scripts_buffer(&mut self) {
        let mut msg = String::new();
        {
            let pop_count = self.scripts_pop_count.lock().expect("pop_scripts_buffer");
            let mut queues = self.scripts_buffer.lock().expect("pop_scripts_buffer");
            let len = pop_count.min(queues.len() as u32);
            for _ in 0..len {
                if let Some(x) = queues.pop_front() {
                    msg.push_str(&x);
                }
            }
        }
        self.evaluate(&msg);
    }

    /// Flush scripts buffer (將緩衝區內的命令送出)
    pub fn flush_scripts_buffer(&mut self) {
        let mut msg = String::new();
        {
            let mut queues = self.scripts_buffer.lock().expect("");
            loop {
                match queues.pop_front() {
                    Some(x) => {
                        msg.push_str(&x);
                    }
                    None => break,
                }
            }
        }
        if msg.len() > 0 {
            self.evaluate(&msg);
        }
    }

    /// Send internal query command
    /// 送出要求狀態的指令
    fn send_internal_query_command(&mut self) -> usize {
        let len;
        let mut msg = String::new();
        {
            let mut init_queries = self.init_queries.lock().expect("");
            // 每次處理的命令數
            let query_count = *self.query_count.lock().expect("");
            // 如果有初始化命令就先處理
            if init_queries.len() > 0 {
                len = init_queries.len().min(query_count);
                for _i in 0..len {
                    // 初始化的命令，送出後就移除
                    msg.push_str(&init_queries.remove(0));
                }
            } else {
                let queries = self.cyclic_queries.lock().expect("");
                let start = *self.last_query.lock().expect("");
                let end = (start + query_count).min(queries.len());
                // 固定要循環送出的命令，
                for i in start..end {
                    msg.push_str(&queries[i]);
                }

                // 紀錄下次執時要從哪一個開始送
                *self.last_query.lock().expect("") = if end == queries.len() { 0 } else { end };
                len = end - start;
            }
        }
        if len > 0 {
            self.evaluate(&msg);
        }
        len
    }

    /// Handle message
    /// 處理 server 送過來的訊息
    fn handle_message(&mut self, message: &str) {
        if message.len() > 0 {
            if let Some(ref cb) = *self.on_message_cb.lock().unwrap() {
                let mut temp_msg = String::from(message).into_bytes();
                // 如果不是換行結束的,補上換行符號,如果沒有在 C 的輸出有問題
                if temp_msg[temp_msg.len() - 1] != 10 {
                    temp_msg.push(10);
                }
                temp_msg.push(0);
                let msg = CStr::from_bytes_with_nul(temp_msg.as_slice())
                    .expect("toCstr")
                    .as_ptr();
                (cb.callback)(cb.pointer, msg);
            }
        }
        {
            let lines: Vec<&str> = message.split("\n").collect();
            let mut handlers = self.handlers.lock().expect("self.handlers.lock()");
            let internal_handlers = self
                .internal_handlers
                .lock()
                .expect("self.internal_handles.lock()");

            for line in lines {
                // 用 `|` 解析訊息
                let r: Vec<&str> = line.split("|").collect();
                let mut next_tag = true;
                let mut event = "";
                for e in r {
                    if next_tag {
                        event = e.trim_start();
                        next_tag = false;
                    } else {
                        // 處理內部要求的訊息
                        // 先將 event 解析成 tag_name.x.x
                        let tag: Vec<&str> = event.split(".").collect();
                        // internal_handlers
                        if let Some(handler) = internal_handlers.get(tag[0]) {
                            let mut data_pool =
                                self.data_pool.lock().expect("self.internal_handles.lock()");
                            let mut tag_index: [usize; 2] = [0; 2];
                            for i in 1..tag.len().min(3) {
                                if let Ok(x) = tag[i].parse::<usize>() {
                                    tag_index[2 - i] = x;
                                }
                            }
                            handler(&mut data_pool, tag_index[0], tag_index[1], e);
                        }

                        let mut remove_event = false;
                        if let Some(handler) = handlers.get_mut(tag[0]) {
                            // tag_index[0]: position
                            // tag_index[1]: channel
                            let mut tag_index: [u32; 2] = [0; 2];
                            for i in 1..tag.len().min(3) {
                                if let Ok(x) = tag[i].parse::<u32>() {
                                    tag_index[2 - i] = x;
                                }
                            }
                            // 轉換字串型態
                            let mut msg = String::from(e).into_bytes();
                            msg.push(0);
                            let msg = CStr::from_bytes_with_nul(msg.as_slice())
                                .expect("toCstr")
                                .as_ptr();
                            // 執行對應的 callback function
                            // 使用 rev() 是為了 handler.remove，從後面刪除才不會影響 i 對應 vec 內的成員
                            for i in (0..handler.len()).rev() {
                                (handler[i].callback)(
                                    handler[i].pointer,
                                    tag_index[0],
                                    tag_index[1],
                                    msg,
                                );

                                if handler[i].count > 0 {
                                    handler[i].count -= 1;
                                    if handler[i].count == 0 {
                                        handler.remove(i);
                                    }
                                }
                            }
                            remove_event = handler.len() == 0;
                        }
                        // 假如都沒有 handle 就將此事件刪除
                        if remove_event {
                            handlers.remove(event);
                        }
                        next_tag = true;
                    }
                }
            }
        }
        self.data_pool_forth();
    }

    /// times
    /// `event` is event name
    /// `count` is handler called times
    /// `handler` is user function
    pub fn times<F>(&mut self, event: &'static str, count: u32, pointer: *mut c_void, cb: F)
    where
        F: Fn(*mut c_void, u32, u32, *const c_char) + Send + 'static,
    {
        let mut handlers = self.handlers.lock().unwrap();
        let handler = handlers.entry(event.to_owned()).or_insert(Vec::new());
        handler.push(TagCallbackHandler {
            count: count,
            pointer: pointer,
            callback: Box::new(cb),
        });
    }

    /// Has WS sender ?
    fn has_ws_sender(&self) -> bool {
        self.ws_out.lock().expect("has_ws_sender").is_some()
    }

    /// Execute on_error callback
    fn execute_on_error_cb(&mut self, msg: &str) {
        *self.is_connecting.lock().expect("execute_on_error_cb") = false;
        *self.is_connected.lock().expect("execute_on_error_cb") = false;
        *self.user_sender.lock().expect("execute_on_error_cb") = None;
        *self.ws_out.lock().expect("execute_on_error_cb") = None;
        self.scripts_buffer
            .lock()
            .expect("execute_on_error_cb")
            .clear();
        if let Some(ref cb) = *self.on_error_cb.lock().expect("execute_on_error_cb") {
            let mut temp_msg = String::from(msg).into_bytes();
            temp_msg.push(0);
            let msg = CStr::from_bytes_with_nul(temp_msg.as_slice())
                .expect("toCstr")
                .as_ptr();
            (cb.callback)(cb.pointer, msg);
        }
    }

    /// Execute on_open callback
    fn execute_on_open_cb(&self) {
        if let Some(ref cb) = *self.on_open_cb.lock().expect("execute_on_open_cb") {
            let mut temp_msg = String::from("Connect to ".to_owned() + &self.url()).into_bytes();
            temp_msg.push(0);
            let msg = CStr::from_bytes_with_nul(temp_msg.as_slice())
                .expect("toCstr")
                .as_ptr();
            (cb.callback)(cb.pointer, msg);
        }
    }

    /// Execute on_send callback
    fn execute_on_send_cb(&mut self, msg: &str) {
        if let Some(ref cb) = *self.on_send_cb.lock().expect("execute_on_send_cb") {
            let mut temp_msg = String::from(msg).into_bytes();
            temp_msg.push(0);
            let msg = CStr::from_bytes_with_nul(temp_msg.as_slice())
                .expect("toCstr")
                .as_ptr();
            (cb.callback)(cb.pointer, msg);
        }
    }

    pub fn set_on_send_cb<F>(&mut self, pointer: *mut c_void, cb: F)
    where
        F: Fn(*mut c_void, *const c_char) + Send + 'static,
    {
        *self.on_send_cb.lock().unwrap() = Some(CallbackHandler {
            pointer: pointer,
            callback: Box::new(cb),
        });
    }

    pub fn set_on_message_cb<F>(&mut self, pointer: *mut c_void, cb: F)
    where
        F: Fn(*mut c_void, *const c_char) + Send + 'static,
    {
        *self.on_message_cb.lock().unwrap() = Some(CallbackHandler {
            pointer: pointer,
            callback: Box::new(cb),
        });
    }

    /// Abort porgram
    pub fn abort_program(&mut self) {
        self.evaluate(r#"abort-program"#);
    }

    /// Deploy porgram
    pub fn program_deploy(&mut self, program: &mut Program) {
        program.push_line("end-of-program ;");
        let lines = program.lines.clone();
        let msg = "deploy ".to_owned()
            + &lines.lock().unwrap()
            + "\n 10 emit .( deployed|ok) 10 emit cr ;deploy";
        self.evaluate(&msg.to_owned());
    }

    /// Run porgram
    pub fn program_run(&mut self, program: &Program) {
        let name = program.name.clone();
        let msg = "deploy user$".to_owned() + &name + " ;deploy";
        self.evaluate(&msg)
    }

    /// Version
    pub fn version() -> &'static str {
        VERSION
    }
}

/// WebSocket Client
struct Client {
    ws_out: ws::Sender,
    sender: mpsc::Sender<String>,
    thread_tx: mpsc::Sender<ws::Sender>,
    on_error_cb: Arc<Mutex<Option<CallbackHandler>>>,
    is_watchdog_refreshed: bool,
}

impl Client {
    /// Execute on_error callback
    fn execute_on_error_cb(&self, msg: &str) {
        if let Some(ref cb) = *self.on_error_cb.lock().expect("execute_on_error_cb") {
            let mut temp_msg = String::from(msg).into_bytes();
            temp_msg.push(0);
            let msg = CStr::from_bytes_with_nul(temp_msg.as_slice())
                .expect("toCstr")
                .as_ptr();
            (cb.callback)(cb.pointer, msg);
        }
    }
}

impl Handler for Client {
    /// on_open
    fn on_open(&mut self, _: Handshake) -> Result<()> {
        self.ws_out
            .timeout(WS_WATCHDOG_PERIOD_MS, WS_TIMEOUT_TOKEN)?;
        self.thread_tx.send(self.ws_out.clone()).map_err(|err| {
            Error::new(
                ErrorKind::Internal,
                format!("Unable to communicate between threads: {:?}.", err),
            )
        })
    }

    /// on_message
    fn on_message(&mut self, msg: Message) -> Result<()> {
        self.is_watchdog_refreshed = true;
        if let Message::Text(m) = msg {
            // 資料長度 > 0 送進 mpsc::channel
            if m.len() > 0 {
                self.sender.send(m).expect("Client::on_message");
            }
        } else {
            unreachable!();
        }
        Ok(())
    }

    /// on error
    fn on_error(&mut self, err: Error) {
        self.execute_on_error_cb(&format!("{:?}\n", err));
    }

    /// on close
    fn on_close(&mut self, code: CloseCode, reason: &str) {
        self.execute_on_error_cb(&format!(
            "WS Client close code = {:?}, reason = {}\n",
            code, reason
        ));
    }

    /// Called when a timeout is triggered.
    fn on_timeout(&mut self, _event: Token) -> Result<()> {
        if !self.is_watchdog_refreshed {
            self.execute_on_error_cb(&format!("WS Client timeout!\n"));
            // 連線斷掉時，要用 shutdown，才能關掉。
            self.ws_out.shutdown()
        } else {
            self.is_watchdog_refreshed = false;
            self.ws_out.timeout(WS_WATCHDOG_PERIOD_MS, WS_TIMEOUT_TOKEN)
        }
    }
}
