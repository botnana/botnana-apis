# Botnana Control API for different languages

# 目錄結構

    botnana-api
    |-----> botnanabcb  C++ Builder 範例 
	|-----> botnanac    C 語言函式庫與範例
    |-----> botnanacs   C# 範例 
    |-----> botnanajs   JavaScript 函式庫與範例

範例內會常常看到一組 IP 與通訊埠為 `192.168.7.2:3012`，是因為 Botnana-Control 的主控合的網路 IP 位置會設定成 `192.168.7.2`，指定 3012 為 WebSocket 通訊埠。如果主控盒的 IP 有其它設定，就必須調整範例中的 IP 位置。

Botnana-Control Server 同時間只允許 2 個 WebSocket Client 連線，如果測試有收到 `Cannot aquire real-time task` 訊息，表示連線數已經多於 2 個了。

如果是要嘗試輸入 Forth 命令與觀察回傳的訊息格式，建議使用 `botnanajs/forth.js` 範例，使用方法參考 [botnanajs/readme.md](./botnanajs/readme.md)

C# 與 C++ Builder 範例皆使用 C 語言的函式庫。

