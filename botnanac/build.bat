echo "build botnana.lib"
cargo clean
cargo update
cargo build --release --target=i686-pc-windows-msvc
copy target\i686-pc-windows-msvc\release\botnana.lib lib\i686-pc-windows-msvc\botnana.lib
copy target\i686-pc-windows-msvc\release\botnana.dll lib\i686-pc-windows-msvc\botnana.dll
cargo build --release --target=x86_64-pc-windows-msvc
copy target\x86_64-pc-windows-msvc\release\botnana.lib lib\x86_64-pc-windows-msvc\botnana.lib
copy target\x86_64-pc-windows-msvc\release\botnana.dll lib\x86_64-pc-windows-msvc\botnana.dll
