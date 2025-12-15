Extract ROM data from rom.cpp and store in a name.bin777 / name.ptn777 file pair.

Mostly useful if you've typed a nice ROM into rom.cpp and now want it on disk.

Put files in cppfiles/ with an appropriate name (like Galaxian.cpp) and
$ ./run.sh

This will produce binfiles/Galaxian.bin777 and binfiles/Galaxian.ptn777 using
the typed-in data plus keymaps from this extract of romSetup.cpp.

TODO (mittonk): Windows support.
