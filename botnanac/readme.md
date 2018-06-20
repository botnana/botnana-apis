#### 前言

提供 C 函式庫與 C 語言的範例  

函式庫下載：

* 32 位元 Linux: [按此下載](https://drive.google.com/drive/u/0/folders/1GhOABXGKu7SZCmRa1uPpun9YILrbSblQ)
* 64 位元 Linux: [按此下載](https://drive.google.com/drive/u/0/folders/10pD_OAfJw971P7gL64MwPdWdgk6dkYWd)
* 32 位元 Windows: [按此下載](https://drive.google.com/drive/u/0/folders/1Vmy9aWYeTMhvJDM3W7UwKuqG4SfyA_n7)
* 64 位元 Windows: [按此下載](https://drive.google.com/drive/u/0/folders/1sGibKjsuhkt0SMJ1w7id1XlOnoYKyD_W)


#### C ++ 使用者
 
如果是以 C++ 呼叫 C 語言的函式庫，因為 C++ 有名稱修飾(Name Mangling) 而 C 沒有，在函式庫已經有處理，所以可以直接引用。

##### VC++
 
如果是使用靜態函式庫，還要連結以下函式庫：
 
 * Ws2_32.lib
 * Userenv.lib
 * Crypt32.lib
 * Secur32.lib
 * NCrypt.lib
 

#### 如何產出函式庫

如要產出 Linux 的函式庫請使用 LXC container，因為 libssl-dev 的 32 與 64 位元函式庫無法同時安裝，所以必須在不同的平台上編譯。

* 32 位元 Linux: `botnanaImage_u14x86` [按此下載](https://drive.google.com/drive/u/0/folders/1p8csI5O7eufJpJF-PHfWurac_aFQxGHk)
* 64 位元 Linux: `botnanaImage_u16x86_64` [按此下載](https://drive.google.com/drive/u/0/folders/1p8csI5O7eufJpJF-PHfWurac_aFQxGHk)

如要產出 Windows 的函式庫，必須在 Windows 上平台安裝  Rust，可以在同一 Windows 平台編出 32 與 64 位元的函式庫。

產出函式庫的指令為：

* 32 位元 Linux: `cargo build --release --target=i686-pc-windows-gnu`
* 64 位元 Linux: `cargo build --release --target=x86_64-unknown-linux-gnu`
* 32 位元 Windows: `cargo build --release --target=i686-pc-windows-msvc`
* 64 位元 Windows: `cargo build --release --target=x86_64-pc-windows-msvc`