// Excerpt of romSetup.cpp from PD777.
#include "PD777.h"

#if defined(_WIN32)
#include <string>
#endif // defined(_WIN32)

namespace {

constexpr u32 ID1  = 0x003f17fa;
constexpr u32 ID2  = 0x00440844;
constexpr u32 ID3  = 0x003ea572;
constexpr u32 ID4  = 0x0041b25a;
constexpr u32 ID5  = 0x003decdb;
constexpr u32 ID6  = 0x003d26f4;
constexpr u32 ID7  = 0x0046e633;
constexpr u32 ID8  = 0x003c3405;
constexpr u32 ID9  = 0x003e4bf7;
constexpr u32 ID11 = 0x003b935b;
constexpr u32 ID12 = 0x003ecdfb;

}

bool
PD777::setupCodeRawAddress(const void* data, size_t dataSize)
{
    for(auto& r : rom) { r = ~0; }

    for(size_t i = 0; i < sizeof(rawRom) / sizeof(rawRom[0]); i += 2) {
        const auto address = rawRom[i + 0]; // アドレス
        rom[address]       = rawRom[i + 1]; // コード
    }
    for(auto& r : this->keyMapping.keyMap) { r = 0; }
    return true;
}

bool
PD777::determineKeyMapping(const void* data, size_t dataSize)
{
    u8 cassetteNumber = 0;

    for(auto& r : rom) { r = ~0; }
    keyMapping.bitMap[0] = 0x40;            // B09 => K7
    keyMapping.bitMap[1] = 0x20;            // B10 => K6
    keyMapping.bitMap[2] = 0x10;            // B11 => K5
    keyMapping.bitMap[3] = 0x08;            // B12 => K4
    keyMapping.bitMap[4] = 0x04;            // B13 => K3
    keyMapping.bitMap[5] = 0x02;            // B14 => K2
    keyMapping.bitMap[6] = 0x01;            // B15 => K1

    // アドレス、コードの順に並んだコード
    if(!setupCodeRawAddress(data, dataSize)) [[unlikely]] { return false; }

    // 適当計算
    // @todo かっこいいのに変更すること！
    {
        u32 checkSum = 0;
        for(const auto& r : rom) { checkSum += r; }
        cassetteNumber = 0;
        switch(checkSum) {
            case ID1:
                cassetteNumber = 1;
                keyMapping.keyMap[0] = 1;
                keyMapping.keyMap[1] = 1;
                keyMapping.keyMap[2] = 2;
                keyMapping.keyMap[3] = 2;
                keyMapping.keyMap[4] = 0;
                break;
            case ID2:
                cassetteNumber = 2;
                keyMapping.keyMap[0] = 1;
                keyMapping.keyMap[1] = 0;
                keyMapping.keyMap[2] = 2;
                keyMapping.keyMap[3] = 0;
                keyMapping.keyMap[4] = 2;
                break;
            case ID3:
                cassetteNumber = 3;
                keyMapping.keyMap[0] = 1;
                keyMapping.keyMap[1] = 1;
                keyMapping.keyMap[2] = 1;
                keyMapping.keyMap[3] = 1;
                keyMapping.keyMap[4] = 0;
                keyMapping.bitMap[0] = 0x20;           // B09 => K6
                keyMapping.bitMap[1] = 0x20;           // B10 => K6
                keyMapping.bitMap[2] = 0x10;           // B11 => K5
                keyMapping.bitMap[3] = 0x08;           // B12 => K4
                keyMapping.bitMap[4] = 0x04;           // B13 => K3
                keyMapping.bitMap[5] = 0x02;           // B14 => K2
                keyMapping.bitMap[6] = 0x01;           // B15 => K1
                break;
            case ID4:
                cassetteNumber = 4;
                keyMapping.keyMap[0] = 1;
                keyMapping.keyMap[1] = 0;
                keyMapping.keyMap[2] = 0;
                keyMapping.keyMap[3] = 0;
                keyMapping.keyMap[4] = 0;
                keyMapping.bitMap[0] = 0x00;           // B09
                keyMapping.bitMap[1] = 0x00;           // B10
                keyMapping.bitMap[2] = 0x00;           // B11
                keyMapping.bitMap[3] = 0x08;           // B12 => K4
                keyMapping.bitMap[4] = 0x00;           // B13
                keyMapping.bitMap[5] = 0x00;           // B14
                keyMapping.bitMap[6] = 0x01;           // B15 => K1
                break;
            case ID5:
                cassetteNumber = 5;
                keyMapping.keyMap[ 0] = 1;
                keyMapping.keyMap[ 1] = 1;
                keyMapping.keyMap[ 2] = 2;
                keyMapping.keyMap[ 3] = 2;
                keyMapping.keyMap[ 4] = 0;
                break;
            case ID6:
                cassetteNumber = 6;
                keyMapping.keyMap[0] = 1;
                keyMapping.keyMap[1] = 0;
                keyMapping.keyMap[2] = 2 | (1 << 2);   // A10  UPをPUSH3、DOWNをPUSH4に割り当てる
                keyMapping.keyMap[3] = 1 | (2 << 2);   // A11  レバー1左右をPUSH1、PUSH2に割り当てる
                keyMapping.keyMap[4] = 0;
                keyMapping.bitMap[0] = 0x02;           // B09 => K2
                keyMapping.bitMap[1] = 0x04;           // B10 => K3
                keyMapping.bitMap[2] = 0x00;           // B11
                keyMapping.bitMap[3] = 0x08;           // B12 => K4
                keyMapping.bitMap[4] = 0x00;           // B13
                keyMapping.bitMap[5] = 0x00;           // B14
                keyMapping.bitMap[6] = 0x01;           // B15 => K1
                break;
            case ID7:
                cassetteNumber = 7;
                keyMapping.keyMap[0] = 1;
                keyMapping.keyMap[1] = 0;
                keyMapping.keyMap[2] = 2;
                keyMapping.keyMap[3] = 0;
                keyMapping.keyMap[4] = 2;
                break;

            case ID8:
                cassetteNumber = 8;
                keyMapping.keyMap[0] = 1;
                keyMapping.keyMap[1] = 1;
                keyMapping.keyMap[2] = 2 | (1 << 2);    // UPをPUSH3、DOWNをPUSH4に割り当てる
                keyMapping.keyMap[3] = 2 | (1 << 2);    // UPをPUSH1、DOWNをPUSH2に割り当てる
                keyMapping.keyMap[4] = 0;
                break;
            case ID9:
                cassetteNumber = 9;
                keyMapping.keyMap[ 0] = 1;
                keyMapping.keyMap[ 1] = 1;
                keyMapping.keyMap[ 2] = 2;
                keyMapping.keyMap[ 3] = 2;
                keyMapping.keyMap[ 4] = 0;
                break;
            case ID11:
                cassetteNumber = 11;
                keyMapping.keyMap[0] = 1;
                keyMapping.keyMap[1] = 0;
                keyMapping.keyMap[2] = 2 | (1 << 2);   // A10  UPをPUSH3、DOWNをPUSH4に割り当てる
                keyMapping.keyMap[3] = 1 | (2 << 2);   // A11  レバー1左右をPUSH1、PUSH2に割り当てる
                keyMapping.keyMap[4] = 0;
                keyMapping.bitMap[0] = 0x02;           // B09 => K2
                keyMapping.bitMap[1] = 0x04;           // B10 => K3
                keyMapping.bitMap[2] = 0x00;           // B11
                keyMapping.bitMap[3] = 0x08;           // B12 => K4
                keyMapping.bitMap[4] = 0x00;           // B13
                keyMapping.bitMap[5] = 0x00;           // B14
                keyMapping.bitMap[6] = 0x01;           // B15 => K1
                break;
            case ID12:
                cassetteNumber = 12;
                keyMapping.keyMap[0] = 1;
                keyMapping.keyMap[1] = 1;
                keyMapping.keyMap[2] = 2 | (1 << 2);    // UPをPUSH3、DOWNをPUSH4に割り当てる
                keyMapping.keyMap[3] = 2 | (1 << 2);    // UPをPUSH1、DOWNをPUSH2に割り当てる
                keyMapping.keyMap[4] = 0;
                break;
        }
    }
    return true;
}

