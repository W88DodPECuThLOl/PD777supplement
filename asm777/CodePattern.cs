namespace asm777
{
	internal enum CodePattern {
        NO_OPERAND,

        OP_LABEL,   // ラベル
        OP_VALUE,   // 値
        OP_LH,      // L,H

        // 疑似命令
        ASM_ORG,                // ORG $000
        ASM_TITLE,              // TITLE "hogehoge"
        ASM_KEY_MAP,            // KEY_MAP A08=>S1
        ASM_INDEX_A1,           // A1+$00,$0=>H,L
        ASM_INDEX_A2,           // A2+$00,$0=>H,L
        ASM_MEM_INDEX_A1_A1,    // M[A1+$00,$0]=>A1
        ASM_MEM_INDEX_A1_A2,    // M[A1+$00,$0]=>A2
        ASM_MEM_INDEX_A2_A1,    // M[A2+$00,$0]=>A1
        ASM_MEM_INDEX_A2_A2,    // M[A2+$00,$0]=>A2
        ASM_MEM_W_INDEX_A1_A2,  // A2=>M[A1+$00,$0]
        ASM_MEM_W_INDEX_A2_A1,  // A1=>M[A2+$00,$0]
        ASM_MEM_W_INDEX_A1_K,   // $00=>M[A1+$00,$0]
        ASM_MEM_W_INDEX_A2_K,   // $00=>M[A2+$00,$0]
    }
}
