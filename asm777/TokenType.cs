namespace asm777
{
	internal enum TokenType
	{
        // 不正なトークン, 終端
        ERROR,
        EOF,
        //

        /// <summary>
        /// 定数値
        /// </summary>
        LITERAL,
        /// <summary>
        /// 文字列定数
        /// </summary>
        STRING_LITERAL,
        /// <summary>
        /// ラベル
        /// </summary>
        LABEL,


        // ----------------------------------------------------
        // ----------------------------------------------------

        /// <summary>
        /// NOP
        /// </summary>
        NOP,

        /// <summary>
        /// GPL
        /// </summary>
        GPL,

        /// <summary>
        /// H=>NRM
        /// </summary>
        MOVE_H_TO_NRM,

        /// <summary>
        /// H<=>X
        /// </summary>
        EXCHANGE_H_X,

        /// <summary>
        /// SRE
        /// </summary>
        SRE,

        /// <summary>
        /// N=>STB
        /// </summary>
        SHIFT_STB,

        // ----------------------------

        /// <summary>
        /// J 4H BLK
        /// </summary>
        _4H_BLK,

        /// <summary>
        /// J VBLK
        /// </summary>
        VBLK,

        /// <summary>
        /// J GPSW/
        /// </summary>
        GPSW_,

        // ----------------------------

        /// <summary>
        /// A=>MA
        /// </summary>
        MOVE_A_TO_MA,

        /// <summary>
        /// MA=>A
        /// </summary>
        MOVE_MA_TO_A,

        /// <summary>
        /// MA<=>A
        /// </summary>
        EXCHANGE_MA_TO_A,

        // ----------------------------

        /// <summary>
        /// SRE+1
        /// </summary>
        SRE_1,

        // ----------------------------

        /// <summary>
        /// J PD1
        /// </summary>
        PD1_J,

        /// <summary>
        /// J PD2
        /// </summary>
        PD2_J,

        /// <summary>
        /// J PD3
        /// </summary>
        PD3_J,

        /// <summary>
        /// J PD4
        /// </summary>
        PD4_J,

        /// <summary>
        /// J/ PD1
        /// </summary>
        PD1_NJ,

        /// <summary>
        /// J/ PD2
        /// </summary>
        PD2_NJ,

        /// <summary>
        /// J/ PD3
        /// </summary>
        PD3_NJ,

        /// <summary>
        /// J/ PD4
        /// </summary>
        PD4_NJ,

        // ----------------------------

        /// <summary>
        /// BOJ M-K
        /// </summary>
        TestSubMK,

        // ----------------------------

        /// <summary>
        /// CAJ M+K=>M
        /// </summary>
        AddMKM,

        /// <summary>
        /// BOJ M-K=>M
        /// </summary>
        SubMKM,

        // ----------------------------

        SubHKtoH,
        AddHKtoH,

        // ----------------------------

        MoveKtoM,
        MoveKtoLH,
        MoveKtoA1,
        MoveKtoA2,
        MoveKtoA3,
        MoveKtoA4,

        // ----------------------------

        /// <summary>
        /// JP LABEL
        /// JP VALUE12
        /// </summary>
        JP,

        /// <summary>
        /// JS LABEL
        /// JS VALUE12
        /// </summary>
        JS,


        ASM_ORG,
        ASM_TITLE,
        ASM_KEY_MAP_A,
        ASM_KEY_MAP_B,
        ASM_INDEX_A1,
        ASM_INDEX_A2,
        ASM_MEM_INDEX_A1_A1,
        ASM_MEM_INDEX_A1_A2,
        ASM_MEM_INDEX_A2_A1,
        ASM_MEM_INDEX_A2_A2,
        ASM_MEM_W_INDEX_A1_A2,
        ASM_MEM_W_INDEX_A2_A1,
        ASM_MEM_W_INDEX_A1_K,
        ASM_MEM_W_INDEX_A2_K,
	}
}
