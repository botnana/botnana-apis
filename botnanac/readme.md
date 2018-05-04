

如要產出 Linux 的函式庫請使用 LXC container，因為 libssl-dev 的 32 與 64 位元函式庫無法同時安裝，所以必須在不同的平台上編譯。

* 32 位元 Linux: `botnanaImage_u14x86` [按此下載](https://drive.google.com/drive/u/0/folders/1p8csI5O7eufJpJF-PHfWurac_aFQxGHk)
* 64 位元 Linux: `botnanaImage_u16x86_64` [按此下載](https://drive.google.com/drive/u/0/folders/1p8csI5O7eufJpJF-PHfWurac_aFQxGHk)

如要產出 Windows 的函式庫，必須在 Windows 上平台安裝  Rust，可以在同一 Windows 平台編出 32 與 64 位元的函式庫。

產出函式庫的指令為：

* 32 位元 Linux: `cargo build --release --target=i686-pc-windows-gnu`
* 64 位元 Linux: `cargo build --release --target=x86_64-unknown-linux-gnu`
* 32 位元 Windows: `cargo build --release --target=i686-pc-windows-msvc`
* 32 位元 Windows: `cargo build --release --target=x86_64-pc-windows-msvc`