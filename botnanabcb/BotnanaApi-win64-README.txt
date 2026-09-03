BotnanaApi Win64 package for C++Builder
==========================================

This package contains the native x86-64 Botnana API DLL and its C-compatible
header:

- BotnanaApi_x86_64.dll
- BotnanaApi.h

Use with C++Builder
-------------------

1. Put BotnanaApi_x86_64.dll beside your application's .exe file.
2. Generate a C++Builder import library:

   implib -a BotnanaApiBCB_x86_64.lib BotnanaApi_x86_64.dll

3. Add BotnanaApiBCB_x86_64.lib to your C++Builder project and include
   BotnanaApi.h.

The DLL is built with the Microsoft Visual C++ 2015-2022 runtime. Install the
x64 Microsoft Visual C++ Redistributable on the target computer if it is not
already installed.
