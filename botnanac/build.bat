echo "build botnana.lib"
rd /s /q lib
md lib
cargo build --release --target=i686-pc-windows-msvc
copy target\i686-pc-windows-msvc\release\botnana.lib lib\botnana.lib
cargo build --release --target=x86_64-pc-windows-msvc
copy target\x86_64-pc-windows-msvc\release\botnana.lib lib\botnana_x86_64.lib
