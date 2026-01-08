#include <format>
#include <fstream>
#include <iostream>
#include <stdlib.h>

#include "PD777.h"

using std::ios;

PD777 cpu;

void write_code(PD777 cpu, std::string basename, std::string title) {
    // https://github.com/W88DodPECuThLOl/PD777supplement/tree/main/asm777
    std::ofstream outfile;
    std::string filename = std::format("{}.bin777", basename);
    outfile.open(filename, ios::binary | ios::out);
    // Header
    // Magic number
    outfile.write("_CassetteVision_", 16);
    // Format version
    outfile.write("0001", 4);
    // Padding
    u32 zero = 0;
    outfile.write(reinterpret_cast<const char*>(&zero), sizeof(u32));

    // Keymap
    const auto romSize = 4096;
    cpu.determineKeyMapping(cpu.rawRom, romSize * 2);

    outfile.write(reinterpret_cast<const char*>(&cpu.keyMapping.keyMap), 5*sizeof(u8));
    outfile.write(reinterpret_cast<const char*>(&zero), sizeof(u8));
    outfile.write(reinterpret_cast<const char*>(&zero), sizeof(u8));
    outfile.write(reinterpret_cast<const char*>(&zero), sizeof(u8));

    outfile.write(reinterpret_cast<const char*>(&cpu.keyMapping.bitMap), 7*sizeof(u8));
    outfile.write(reinterpret_cast<const char*>(&zero), sizeof(u8));

    // Title
    const int title_width = 216;
    outfile.write(title.c_str(), title.length());
    for (auto i=title.length(); i<title_width; i++) {
        outfile.write(reinterpret_cast<const char*>(&zero), sizeof(u8));
    }

    // Code
    //const auto rawRomSize = 4096;
    outfile.write(reinterpret_cast<const char*>(cpu.rawRom), romSize * 2);

    outfile.close();

};

void write_pattern(PD777 cpu, std::string basename) {
    // https://github.com/W88DodPECuThLOl/PD777supplement/blob/main/ptn777/README.md
    std::ofstream outfile;
    std::string filename = std::format("{}.ptn777", basename);
    outfile.open(filename, ios::binary | ios::out);
    // Header
    // Magic number
    outfile.write("*CassetteVision*", 16);
    // Format version
    outfile.write("0000", 4);
    // Padding
    u32 zero = 0;
    outfile.write(reinterpret_cast<const char*>(&zero), sizeof(u32));
    outfile.write(reinterpret_cast<const char*>(&zero), sizeof(u32));
    outfile.write(reinterpret_cast<const char*>(&zero), sizeof(u32));

    // Vent
    // cpp files are in "patternRaw" format; extract every 7th index and
    // discard the 7 data points and 6 following indices to get "patternFormatted" format.
    const auto vent_bytes = 16;
    u8 vent_scratch[vent_bytes] = {};
    for (auto i=0; i<vent_bytes; i++) {
        vent_scratch[i] = cpu.characterAttribute[i*7*2];
        if (cpu.characterAttribute[i] == 0xFF) break;
    }
    outfile.write(reinterpret_cast<const char*>(vent_scratch), vent_bytes);

    // 7x7 pattern
    const auto seven_size = 686;
    outfile.write(reinterpret_cast<const char*>(cpu.patternRom), seven_size);

    // 8x7 pattern
    const auto eight_size = 98;
    outfile.write(reinterpret_cast<const char*>(cpu.patternRom8), eight_size);

    outfile.close();

};

int main(int argc, char *argv[]) {
    if (argc != 3) return -1;
    std::string basename = argv[1];
    std::string title = argv[2];
    write_code(cpu, basename, title);
    write_pattern(cpu, basename);

    return 0;
}
