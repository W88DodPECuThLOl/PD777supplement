#!/bin/bash
sourcefile="$1"
filename=$(basename -- "$sourcefile")
shortname="${filename%.*}"
title=$shortname
echo "$sourcefile => $shortname"

cp "$sourcefile" rom.cpp
touch rom.cpp
make semiclean && make
./cpp2bin777 "$shortname" "$title"
mkdir -p binfiles
mv "$shortname.bin777" "$shortname.ptn777" binfiles/
