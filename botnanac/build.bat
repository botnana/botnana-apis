echo "build botnana.lib"
rd /s /q lib
md lib
cd lib
md i686-pc-windows-msvc
md x86_64-pc-windows-msvc
cd ..
cargo build --release --target=i686-pc-windows-msvc
copy target\i686-pc-windows-msvc\release\botnana.lib lib\i686-pc-windows-msvc\botnana.lib
cargo build --release --target=x86_64-pc-windows-msvc
copy target\x86_64-pc-windows-msvc\release\botnana.lib lib\x86_64-pc-windows-msvc\botnana.lib
