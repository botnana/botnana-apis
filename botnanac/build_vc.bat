cd

msbuild ..\botnanacs\BotnanaApi\BotnanaApi.sln /t:Clean,Build /p:Configuration=Release /p:Platform=x86
msbuild ..\botnanacs\BotnanaApi\BotnanaApi.sln /t:Clean,Build /p:Configuration=Release /p:Platform=x64

copy ..\botnanacs\BotnanaApi\Release\BotnanaApi.dll lib\BotnanaApi.dll
copy ..\botnanacs\BotnanaApi\x64\Release\BotnanaApi_x86_64.dll lib\BotnanaApi_x86_64.dll

implib -a lib\BotnanaApiBCB.lib lib\BotnanaApi.dll
implib -a lib\BotnanaApiBCB_x86_64.lib lib\BotnanaApi_x86_64.dll

copy lib\BotnanaApi.dll ..\botnanacs\AIO\AIO\BotnanaApi.dll
copy lib\BotnanaApi.dll ..\botnanacs\AxisGroup\AxisGroup\BotnanaApi.dll
copy lib\BotnanaApi.dll ..\botnanacs\DIO\DIO\BotnanaApi.dll
copy lib\BotnanaApi.dll ..\botnanacs\PositionComparsionPanaA6B\PositionComparsionPanaA6B\BotnanaApi.dll
copy lib\BotnanaApi.dll ..\botnanacs\SingleDrive\SingleDrive\BotnanaApi.dll
copy lib\BotnanaApi.dll ..\botnanacs\TorqueScope\TorqueScope\BotnanaApi.dll
copy lib\BotnanaApi.dll ..\botnanacs\TouchProbe\TouchProbe\BotnanaApi.dll

copy lib\BotnanaApiBCB.lib ..\botnanabcb\SingleDrive\BotnanaApiBCB.lib
copy lib\BotnanaApi.dll ..\botnanabcb\SingleDrive\Win32\Release\BotnanaApi.dll
