// Stub include for rom extraction only.
#pragma once

#include <cstdint>
#include <stdlib.h>

// 基本的な型
typedef std::int8_t   s8;
typedef std::uint8_t  u8;
typedef std::int16_t  s16;
typedef std::uint16_t u16;
typedef std::int32_t  s32;
typedef std::uint32_t u32;
typedef std::int64_t  s64;
typedef std::uint64_t u64;
typedef float         f32;
typedef double        f64;

/**
 * @brief カートリッジの結線
 * 
 * * カートリッジ内のCPUとの結線状態の定義
 * * CPUと本体の配線がどのようにつながっているかを定義するもの
 * * カートリッジ毎に違う
 */
struct KeyMapping {
    /**
     * @brief A8～A12の割り当て
     * 
     * keyMap[0]がA8でkeyMap[4]がA12に対応。<br>
     * 値はS1に割り当てる場合は1、S2は2、S3は3、S4は4を設定する。<br>
     * 未割り当ての場合は0を設定する。<br>
     * 
     * 全て0の場合は、デフォルトの変換規則になる。<br>
     * 
     * |     | B15   | B14 | B13 | B12    | B11 | B10   | B9    |
     * |-----|-------|-----|-----|--------|-----|-------|-------|
     * | A8  | START | L1L | L1R | SELECT | AUX |       |       |
     * | A9  |       | L2L | L2R |        |     |       |       |
     * | A10 |       |     |     |        |     | PUSH4 | PUSH3 |
     * | A11 |       |     |     |        |     | PUSH2 | PUSH1 |
     * | A12 | LL    | L   | C   | R      | RR  |       |       |
     * ※LL、L、C、R、RR: コーススイッチ
     */
    u8 keyMap[5] {};
    /**
     * @brief B9～B15の割り当て
     * 
     * レバーやボタンが押されたときのKの値を設定する。<br>
     *  bitMap[0] : B9がONのときにKに設定する値(0x40 B9はCPUのK7とつながっている)<br>
     *  bitMap[1] : B10がONのときにKに設定する値(0x20 B10はCPUのK6と繋がっている)<br>
     *  bitMap[2] : B11がONのときにKに設定する値(0x10 B11はCPUのK5と繋がっている)<br>
     *  bitMap[3] : B12がONのときにKに設定する値(0x08 B12はCPUのK4と繋がっている)<br>
     *  bitMap[4] : B13がONのときにKに設定する値(0x04 B13はCPUのK3と繋がっている)<br>
     *  bitMap[5] : B14がONのときにKに設定する値(0x02 B14はCPUのK2と繋がっている)<br>
     *  bitMap[6] : B15がONのときにKに設定する値(0x01 B15はCPUのK1と繋がっている)<br>
     *  ※括弧はデフォルト値
     */
    u8 bitMap[7] {};

    /**
     * @brief デフォルトの設定かどうかを判定
     * 
     * ::keyMap[]が全て0の場合デフォルトの設定とみなす。
     * 
     * @return デフォルトの設定かどうか
     * @retval  true:  デフォルトの設定
     * @retval  false: 何かしら設定されている
     */
    const bool isDefaultMapping() const noexcept {
        for(const auto& e : keyMap) {
            if(e) { return false; }
        }
        return true;
    }
};

class PD777 {
public:
    /**
     * @brief ROMデータの元
     * address, dataの組が続いている。
     */
    static const u16 rawRom[4096];
    /**
     * @brief 7x7パターン用のROM
     */
    static const u8 patternRom[686];
    /**
     * @brief 8x7パターン用のROM
     */
    static const u8 patternRom8[98];
    /**
     * @brief パターンの属性
     */
    static const u8 characterAttribute[0x80*2];
    u32 cassetteNumber = 0;
    KeyMapping keyMapping;

    /**
     * @brief   コードをセットアップする
     * 
     * @param[in]   codeData        コードデータ
     * @param[in]   codeDataSize    コードデータサイズ(バイト単位)
     * modifies     keyMapping      Look up key mapping from hardcoded list
     * @return  成功したらtrueを返す
     */
    bool determineKeyMapping(const void* codeData, size_t codeDataSize);

    bool isCodeRawOrder(const void* codeData,  const size_t codeDataSize) const;
    bool setupCodeRawAddress(const void* data, size_t dataSize);
    bool setupCodeRawOrder(const void* data, const size_t dataSize);

    u16 rom[0x800] {};
    u8 patternCGRom7x7[686] {};
    u8 patternCGRom8x7[98] {};
    u8 characterBent[0x80*2] {};
};


