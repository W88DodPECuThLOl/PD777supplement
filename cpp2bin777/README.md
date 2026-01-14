Extract ROM data from rom.cpp and store in a name.bin777 / name.ptn777 file pair,
as well as a ZIP-file containing those two.

Mostly useful if you've typed a nice ROM into rom.cpp and now want it on disk.

Put files in cppfiles/ with an appropriate name (like Galaxian.cpp) and
$ ./run.sh

This will produce
- binfiles/Galaxian.bin777
- binfiles/Galaxian.ptn777
- binfiles/Galaxian.zip

using the typed-in data plus keymaps from this extract of romSetup.cpp.

These utilites can run on Linux and macOS, or on Windows using the msys2 environment
used to build libretro cores:
https://docs.libretro.com/development/retroarch/compilation/windows/

TODO (mittonk): Direct Windows support using Visual Studio and batch files.
