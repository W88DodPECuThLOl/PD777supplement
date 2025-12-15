#!/bin/bash

for sourcefile in cppfiles/*.cpp; do
    ./process.sh "$sourcefile"
done
