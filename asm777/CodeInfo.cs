namespace asm777
{
	internal class CodeInfo
	{
        /// <summary>
        /// 
        /// </summary>
        public string mnemonic;
        public string judge;
        public TokenType tokenType;
        public CodePattern codePattern;
        public int codeBase;
        public List<ParamInfo> param;

        public CodeInfo(string mnemonic, string judge, TokenType tokenType, CodePattern codePattern, int codeBase)
        {
            this.mnemonic = mnemonic;
            this.judge = judge;
            this.tokenType = tokenType;
            this.codePattern = codePattern;
            this.codeBase = codeBase;
            this.param = new List<ParamInfo>();
        }
        public CodeInfo(string mnemonic, string judge, TokenType tokenType, CodePattern codePattern, int codeBase, int param1mask, int param1shift)
        {
            this.mnemonic = mnemonic;
            this.judge = judge;
            this.tokenType = tokenType;
            this.codePattern = codePattern;
            this.codeBase = codeBase;
            this.param = new List<ParamInfo>();
            this.param.Add(new ParamInfo(param1mask, param1shift));
        }
        public CodeInfo(string mnemonic, string judge, TokenType tokenType, CodePattern codePattern, int codeBase, int param1mask, int param1shift, int param2mask, int param2shift)
        {
            this.mnemonic = mnemonic;
            this.judge = judge;
            this.tokenType = tokenType;
            this.codePattern = codePattern;
            this.codeBase = codeBase;
            this.param = new List<ParamInfo>();
            this.param.Add(new ParamInfo(param1mask, param1shift));
            this.param.Add(new ParamInfo(param2mask, param2shift));
        }
        public CodeInfo(string mnemonic, string judge, TokenType tokenType, CodePattern codePattern, int codeBase,
            int param1mask, int param1shift,
            int param2mask, int param2shift,
            int param3mask, int param3shift,
            int param4mask, int param4shift,
            int param5mask, int param5shift
        )
        {
            this.mnemonic = mnemonic;
            this.judge = judge;
            this.tokenType = tokenType;
            this.codePattern = codePattern;
            this.codeBase = codeBase;
            this.param = new List<ParamInfo>();
            this.param.Add(new ParamInfo(param1mask, param1shift));
            this.param.Add(new ParamInfo(param2mask, param2shift));
            this.param.Add(new ParamInfo(param3mask, param3shift));
            this.param.Add(new ParamInfo(param4mask, param4shift));
            this.param.Add(new ParamInfo(param5mask, param5shift));
        }


        static public List<CodeInfo> codesInfoList = new List<CodeInfo>() {
            new CodeInfo(@"^NOP$",                          "",     TokenType.NOP,                  CodePattern.NO_OPERAND, 0x000),
            new CodeInfo(@"^J\s+GPL$",                      "J",    TokenType.GPL,                  CodePattern.NO_OPERAND, 0x004),
            new CodeInfo(@"^H\s*=>\s*NRM$",                 "",     TokenType.MOVE_H_TO_NRM,        CodePattern.NO_OPERAND, 0x008),
            new CodeInfo(@"^H\s*<=>\s*X$",                  "",     TokenType.EXCHANGE_H_X,         CodePattern.NO_OPERAND, 0x018),
            new CodeInfo(@"^SRE$",                          "",     TokenType.SRE,                  CodePattern.NO_OPERAND, 0x020),
            new CodeInfo(@"^\$([0-1])\s*=>\s*STB$",         "",     TokenType.SHIFT_STB,            CodePattern.OP_VALUE,   0x028, 0x001, 0),
            new CodeInfo(@"^([0-1])\s*=>\s*STB$",           "",     TokenType.SHIFT_STB,            CodePattern.OP_VALUE,   0x028, 0x001, 0),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            new CodeInfo(@"^J\s+4H\s*BLK$",                 "J",    TokenType._4H_BLK,              CodePattern.NO_OPERAND, 0x049),
            new CodeInfo(@"^J\s+VBLK$",                     "J",    TokenType.VBLK,                 CodePattern.NO_OPERAND, 0x04A),
            new CodeInfo(@"^J\s+GPSW/$",                    "J",    TokenType.GPSW_,                CodePattern.NO_OPERAND, 0x04C),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            new CodeInfo(@"^A\s*=>\s*MA$",                  "",     TokenType.MOVE_A_TO_MA,         CodePattern.NO_OPERAND, 0x054),
            new CodeInfo(@"^MA\s*=>\s*A$",                  "",     TokenType.MOVE_MA_TO_A,         CodePattern.NO_OPERAND, 0x058),
            new CodeInfo(@"^MA\s*<=>\s*A$",                 "",     TokenType.EXCHANGE_MA_TO_A,     CodePattern.NO_OPERAND, 0x05C),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // SRE+1
            new CodeInfo(@"^SRE\s*\+\s*1$",                 "",     TokenType.SRE_1,                CodePattern.NO_OPERAND, 0x060),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // J PD1
            new CodeInfo(@"^J\s+PD1$",                      "J",    TokenType.PD1_J,                CodePattern.NO_OPERAND, 0x030),
            // J PD2
            new CodeInfo(@"^J\s+PD2$",                      "J",    TokenType.PD2_J,                CodePattern.NO_OPERAND, 0x034),
            // J PD3
            new CodeInfo(@"^J\s+PD3$",                      "J",    TokenType.PD3_J,                CodePattern.NO_OPERAND, 0x038),
            // J PD4
            new CodeInfo(@"^J\s+PD4$",                      "J",    TokenType.PD4_J,                CodePattern.NO_OPERAND, 0x03C),
            // J/ PD1
            new CodeInfo(@"^J/\s+PD1$",                     "J/",   TokenType.PD1_NJ,               CodePattern.NO_OPERAND, 0x070),
            // J/ PD2
            new CodeInfo(@"^J/\s+PD2$",                     "J/",   TokenType.PD2_NJ,               CodePattern.NO_OPERAND, 0x074),
            // J/ PD3
            new CodeInfo(@"^J/\s+PD3$",                     "J/",   TokenType.PD3_NJ,               CodePattern.NO_OPERAND, 0x078),
            // J/ PD4
            new CodeInfo(@"^J/\s+PD4$",                     "J/",   TokenType.PD4_NJ,               CodePattern.NO_OPERAND, 0x07C),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // BOJ M-K
            new CodeInfo(@"^BOJ\s+M\s*-\s*\$([a-fA-F0-9]{1,2})$",                                 "BOJ",  TokenType.TestSubMK,         CodePattern.OP_VALUE,   0x080, 0x07F, 0),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // CAJ M+K=>M, N=>L
            new CodeInfo(@"^CAJ\s+M\s*\+\s*\$([a-fA-F0-9]{1,2})\s*=>\s*M,\s*\$([0-3])\s*=>\s*L$", "CAJ",  TokenType.AddMKM,            CodePattern.OP_VALUE,   0x100, 0x01F, 0, 0x003, 5),
            // BOJ M-K=>M, N=>L
            new CodeInfo(@"^BOJ\s+M\s*-\s*\$([a-fA-F0-9]{1,2})\s*=>\s*M,\s*\$([0-3])\s*=>\s*L$",  "BOJ",  TokenType.SubMKM,            CodePattern.OP_VALUE,   0x180, 0x01F, 0, 0x003, 5),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // EQJ A1&A1, N=>L
            new CodeInfo(@"^EQJ\s+A1\s*&\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                           "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x200, 0x003, 0),
            // EQJ/ A1&A1, N=>L
            new CodeInfo(@"^EQJ/\s+A1\s*&\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                          "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x220, 0x003, 0),
            // EQJ A1=A1, N=>L
            new CodeInfo(@"^EQJ\s+A1\s*=\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                           "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x208, 0x003, 0),
            // EQJ/ A1=A1, N=>L
            new CodeInfo(@"^EQJ/\s+A1\s*=\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                          "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x228, 0x003, 0),
            // BOJ A1-A1, N=>L
            new CodeInfo(@"^BOJ\s+A1\s*-\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                           "BOJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x20C, 0x003, 0),
            // BOJ/ A1-A1, N=>L
            new CodeInfo(@"^BOJ/\s+A1\s*-\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                          "BOJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x22C, 0x003, 0),

            // EQJ A1&A2, N=>L
            new CodeInfo(@"^EQJ\s+A1\s*&\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                           "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x210, 0x003, 0),
            // EQJ/ A1&A2, N=>L
            new CodeInfo(@"^EQJ/\s+A1\s*&\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                          "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x230, 0x003, 0),
            // EQJ A1=A2, N=>L
            new CodeInfo(@"^EQJ\s+A1\s*=\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                           "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x218, 0x003, 0),
            // EQJ/ A1=A2, N=>L
            new CodeInfo(@"^EQJ/\s+A1\s*=\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                          "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x238, 0x003, 0),
            // BOJ A1-A2, N=>L
            new CodeInfo(@"^BOJ\s+A1\s*-\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                           "BOJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x21C, 0x003, 0),
            // BOJ/ A1-A2, N=>L
            new CodeInfo(@"^BOJ/\s+A1\s*-\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                          "BOJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x23C, 0x003, 0),

            // EQJ A2&A1, N=>L
            new CodeInfo(@"^EQJ\s+A2\s*&\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                           "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x240, 0x003, 0),
            // EQJ/ A2&A1, N=>L
            new CodeInfo(@"^EQJ/\s+A2\s*&\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                          "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x260, 0x003, 0),
            // EQJ A2=A1, N=>L
            new CodeInfo(@"^EQJ\s+A2\s*=\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                           "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x248, 0x003, 0),
            // EQJ/ A2=A1, N=>L
            new CodeInfo(@"^EQJ/\s+A2\s*=\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                          "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x268, 0x003, 0),
            // BOJ A2-A1, N=>L
            new CodeInfo(@"^BOJ\s+A2\s*-\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                           "BOJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x24C, 0x003, 0),
            // BOJ/ A2-A1, N=>L
            new CodeInfo(@"^BOJ/\s+A2\s*-\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                          "BOJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x26C, 0x003, 0),

            // EQJ A2&A2, N=>L
            new CodeInfo(@"^EQJ\s+A2\s*&\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                           "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x250, 0x003, 0),
            // EQJ/ A2&A2, N=>L
            new CodeInfo(@"^EQJ/\s+A2\s*&\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                          "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x270, 0x003, 0),
            // EQJ A2=A2, N=>L
            new CodeInfo(@"^EQJ\s+A2\s*=\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                           "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x258, 0x003, 0),
            // EQJ/ A2=A2, N=>L
            new CodeInfo(@"^EQJ/\s+A2\s*=\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                          "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x278, 0x003, 0),
            // BOJ A2-A2, N=>L
            new CodeInfo(@"^BOJ\s+A2\s*-\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                           "BOJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x25C, 0x003, 0),
            // BOJ/ A2-A2, N=>L
            new CodeInfo(@"^BOJ/\s+A2\s*-\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                          "BOJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x27C, 0x003, 0),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // EQJ M&A1, N=>L
            new CodeInfo(@"^EQJ\s+M\s*&\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                          "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x280, 0x003, 0),
            // EQJ/ M&A1, N=>L
            new CodeInfo(@"^EQJ/\s+M\s*&\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                         "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x2A0, 0x003, 0),

            // EQJ M=A1, N=>L
            new CodeInfo(@"^EQJ\s+M\s*=\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                          "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x288, 0x003, 0),
            // EQJ/ M=A1, N=>L
            new CodeInfo(@"^EQJ/\s+M\s*=\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                         "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x2A8, 0x003, 0),

            new CodeInfo(@"^BOJ\s+M\s*-\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                          "BOJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x28C, 0x003, 0),
            new CodeInfo(@"^BOJ/\s+M\s*-\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                         "BOJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x2AC, 0x003, 0),

            // EQJ M&A2, N=>L
            new CodeInfo(@"^EQJ\s+M\s*&\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                          "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x290, 0x003, 0),
            // EQJ/ M&A2, N=>L
            new CodeInfo(@"^EQJ/\s+M\s*&\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                         "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x2B0, 0x003, 0),

            new CodeInfo(@"^EQJ\s+M\s*=\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                          "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x298, 0x003, 0),
            new CodeInfo(@"^EQJ/\s+M\s*=\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                         "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x2B8, 0x003, 0),

            new CodeInfo(@"^BOJ\s+M\s*-\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                          "BOJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x29C, 0x003, 0),
            new CodeInfo(@"^BOJ/\s+M\s*-\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                         "BOJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x2BC, 0x003, 0),

            new CodeInfo(@"^EQJ\s+H\s*&\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                          "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x2C0, 0x003, 0),
            new CodeInfo(@"^EQJ/\s+H\s*&\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                         "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x2E0, 0x003, 0),

            // EQJ H=A1, N=>L
            new CodeInfo(@"^EQJ\s+H\s*=\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                          "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x2C8, 0x003, 0),
            // EQJ/ H=A1, N=>L
            new CodeInfo(@"^EQJ/\s+H\s*=\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                         "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x2E8, 0x003, 0),

            new CodeInfo(@"^BOJ\s+H\s*-\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                          "BOJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x2CC, 0x003, 0),
            new CodeInfo(@"^BOJ/\s+H\s*-\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                         "BOJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x2EC, 0x003, 0),

            new CodeInfo(@"^EQJ\s+H\s*&\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                          "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x2D0, 0x003, 0),
            new CodeInfo(@"^EQJ/\s+H\s*&\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                         "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x2F0, 0x003, 0),
            // EQJ H=A2, N=>L
            new CodeInfo(@"^EQJ\s+H\s*=\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                          "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x2D8, 0x003, 0),
            // EQJ/ H=A2, N=>L
            new CodeInfo(@"^EQJ/\s+H\s*=\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                         "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x2F8, 0x003, 0),

            new CodeInfo(@"^BOJ\s+H\s*-\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                          "BOJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x2DC, 0x003, 0),
            new CodeInfo(@"^BOJ/\s+H\s*-\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                         "BOJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x2FC, 0x003, 0),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // N=>L
            new CodeInfo(@"^\$([0-3])\s*=>\s*L$",                                               "",     TokenType.NOP,              CodePattern.OP_VALUE,   0x300, 0x003, 0),
            // A2=>A1, N=>L
            new CodeInfo(@"^A2\s*=>\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                              "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x310, 0x003, 0),
            // A1-A2=>A2, N=>L
            new CodeInfo(@"^BOJ\s+A1\s*-\s*A2\s*=>\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",               "BOJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x31C, 0x003, 0),
            // A1=>FLS, 0=>L
            new CodeInfo(@"^A1\s*=>\s*FLS\s*,\s*\$0\s*=>\s*L$",                                   "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x308),
            // A1=>FRS, 1=>L
            new CodeInfo(@"^A1\s*=>\s*FRS\s*,\s*\$1\s*=>\s*L$",                                   "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x309),
            // A1=>MODE, 1N=>L
            new CodeInfo(@"^A1\s*=>\s*MODE\s*,\s*\$([2-3])\s*=>\s*L$",                            "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x30A, 0x001, 0),
            // A1=>RS, N=>L
            new CodeInfo(@"^A1\s*=>\s*RS\s*,\s*\$([0-3])\s*=>\s*L$",                              "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x318, 0x003, 0),
            // A1=>A2, N=>L
            new CodeInfo(@"^A1\s*=>\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                              "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x340, 0x003, 0),
            new CodeInfo(@"^A2\s*=>\s*FLS\s*,\s*\$0\s*=>\s*L$",                                   "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x348),
            new CodeInfo(@"^A2\s*=>\s*FRS\s*,\s*\$1\s*=>\s*L$",                                   "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x349),
            new CodeInfo(@"^A2\s*=>\s*MODE\s*,\s*\$([2-3])\s*=>\s*L$",                            "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x34A, 0x001, 0),
            new CodeInfo(@"^BOJ\s+A2\s*-\s*A1\s*=>\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",               "BOJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x34C, 0x003, 0),
            new CodeInfo(@"^A2\s*=>\s*RS\s*,\s*\$([0-3])\s*=>\s*L$",                              "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x358, 0x003, 0),
            // M=>FLS, 0=>L
            new CodeInfo(@"^M\s*=>\s*FLS\s*,\s*\$0\s*=>\s*L$",                                  "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x388),
            new CodeInfo(@"^M\s*=>\s*FRS\s*,\s*\$1\s*=>\s*L$",                                  "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x389),
            new CodeInfo(@"^M\s*=>\s*MODE\s*,\s*\$([2-3])\s*=>\s*L$",                           "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x38A, 0x001, 0),
            new CodeInfo(@"^M\s*=>\s*RS\s*,\s*\$([0-3])\s*=>\s*L$",                             "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x398, 0x003, 0),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // A1&A1=>A1, N=>L
            new CodeInfo(@"^A1\s*&\s*A1\s*=>\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                   "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x320, 0x003, 0),
            // CAJ A1+A1=>A1, N=>L
            new CodeInfo(@"^CAJ\s+A1\s*\+\s*A1\s*=>\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",            "CAJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x324, 0x003, 0),
            // A1|A1=>A1, N=>L
            new CodeInfo(@"^A1\s*\|\s*A1\s*=>\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                  "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x328, 0x003, 0),
            // BOJ A1-A1=>A1, N=>L
            new CodeInfo(@"^BOJ\s+A1\s*-\s*A1\s*=>\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",             "BOJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x32C, 0x003, 0),
            // A1&A2=>A1, N=>L
            new CodeInfo(@"^A1\s*&\s*A2\s*=>\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                   "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x330, 0x003, 0),
            // CAJ A1+A2=>A1, N=>L
            new CodeInfo(@"^CAJ\s+A1\s*\+\s*A2\s*=>\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",            "CAJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x334, 0x003, 0),
            // A1|A2=>A1, N=>L
            new CodeInfo(@"^A1\s*\|\s*A2\s*=>\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                  "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x338, 0x003, 0),
            // BOJ A1-A2=>A1, N=>L
            new CodeInfo(@"^BOJ\s+A1\s*-\s*A2\s*=>\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",             "BOJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x33C, 0x003, 0),
            // A2&A1=>A2, N=>L
            new CodeInfo(@"^A2\s*&\s*A1\s*=>\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                   "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x360, 0x003, 0),
            // CAJ A2+A1=>A2, N=>L
            new CodeInfo(@"^CAJ\s+A2\s*\+\s*A1\s*=>\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",            "CAJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x364, 0x003, 0),
            // A2|A1=>A2, N=>L
            new CodeInfo(@"^A2\s*\|\s*A1\s*=>\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                  "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x368, 0x003, 0),
            // BOJ A2-A1=>A2, N=>L
            new CodeInfo(@"^BOJ\s+A2\s*-\s*A1\s*=>\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",             "BOJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x36C, 0x003, 0),
            // A2&A2=>A2, N=>L
            new CodeInfo(@"^A2\s*&\s*A2\s*=>\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                   "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x370, 0x003, 0),
            // CAJ A2+A2=>A2, N=>L
            new CodeInfo(@"^CAJ\s+A2\s*\+\s*A2\s*=>\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",            "CAJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x374, 0x003, 0),
            // A2|A2=>A2, N=>L
            new CodeInfo(@"^A2\s*\|\s*A2\s*=>\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                  "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x378, 0x003, 0),
            // BOJ A2-A2=>A2, N=>L
            new CodeInfo(@"^BOJ\s+A2\s*-\s*A2\s*=>\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",             "BOJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x37C, 0x003, 0),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // A1=>M, N=>L
            new CodeInfo(@"^A1\s*=>\s*M\s*,\s*\$([0-3])\s*=>\s*L$",                             "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x380, 0x003, 0),
            // A2=>M, N=>L
            new CodeInfo(@"^A2\s*=>\s*M\s*,\s*\$([0-3])\s*=>\s*L$",                             "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x390, 0x003, 0),
            // M<=>A1, N=>L
            new CodeInfo(@"^M\s*<=>\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                            "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x384, 0x003, 0),
            // M<=>A2, N=>L
            new CodeInfo(@"^M\s*<=>\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                            "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x394, 0x003, 0),
            // M=>A1, N=>L
            new CodeInfo(@"^M\s*=>\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                             "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x38C, 0x003, 0),
            // M=>A2, N=>L
            new CodeInfo(@"^M\s*=>\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                             "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x39C, 0x003, 0),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // M&A2=>M, N=>L
            new CodeInfo(@"^M\s*&\s*A2\s*=>\s*M\s*,\s*\$([0-3])\s*=>\s*L$",                     "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x3B0, 0x003, 0),
            // CAJ M+A2=>M, N=>L
            new CodeInfo(@"^CAJ\s+M\s*\+\s*A2\s*=>\s*M\s*,\s*\$([0-3])\s*=>\s*L$",              "CAJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x3B4, 0x003, 0),
            // M|A2=>M, N=>L
            new CodeInfo(@"^M\s*\|\s*A2\s*=>\s*M\s*,\s*\$([0-3])\s*=>\s*L$",                    "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x3B8, 0x003, 0),
            // BOJ M-A2=>M, N=>L
            new CodeInfo(@"^BOJ\s+M\s*-\s*A2\s*=>\s*M\s*,\s*\$([0-3])\s*=>\s*L$",               "BOJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x3BC, 0x003, 0),
            // M&A1=>M, N=>L
            new CodeInfo(@"^M\s*&\s*A1\s*=>\s*M\s*,\s*\$([0-3])\s*=>\s*L$",                     "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x3A0, 0x003, 0),
            // CAJ M+A1=>M, N=>L
            new CodeInfo(@"^CAJ\s+M\s*\+\s*A1\s*=>\s*M\s*,\s*\$([0-3])\s*=>\s*L$",              "CAJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x3A4, 0x003, 0),
            // M|A1=>M, N=>L
            new CodeInfo(@"^M\s*\|\s*A1\s*=>\s*M\s*,\s*\$([0-3])\s*=>\s*L$",                    "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x3A8, 0x003, 0),
            // BOJ M-A1=>M, N=>L
            new CodeInfo(@"^BOJ\s+M\s*-\s*A1\s*=>\s*M\s*,\s*\$([0-3])\s*=>\s*L$",               "BOJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x3AC, 0x003, 0),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // A1=>H, N=>L
            new CodeInfo(@"^A1\s*=>\s*H\s*,\s*\$([0-3])\s*=>\s*L$",                             "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x3C0, 0x003, 0),
            // A2=>H, N=>L
            new CodeInfo(@"^A2\s*=>\s*H\s*,\s*\$([0-3])\s*=>\s*L$",                             "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x3D0, 0x003, 0),
            // H=>A1, N=>L
            new CodeInfo(@"^H\s*=>\s*A1\s*,\s*\$([0-3])\s*=>\s*L$",                             "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x3CC, 0x003, 0),
            // H=>A2, N=>L
            new CodeInfo(@"^H\s*=>\s*A2\s*,\s*\$([0-3])\s*=>\s*L$",                             "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x3DC, 0x003, 0),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // H&A1=>H, N=>L
            new CodeInfo(@"^H\s*&\s*A1\s*=>\s*H\s*,\s*\$([0-3])\s*=>\s*L$",                     "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x3E0, 0x003, 0),
            // CAJ H+A1=>H, N=>L
            new CodeInfo(@"^CAJ\s+H\s*\+\s*A1\s*=>\s*H\s*,\s*\$([0-3])\s*=>\s*L$",              "CAJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x3E4, 0x003, 0),
            // H|A1=>H, N=>L
            new CodeInfo(@"^H\s*\|\s*A1\s*=>\s*H\s*,\s*\$([0-3])\s*=>\s*L$",                    "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x3E8, 0x003, 0),
            // BOJ H-A1=>H, N=>L
            new CodeInfo(@"^BOJ\s+H\s*-\s*A1\s*=>\s*H\s*,\s*\$([0-3])\s*=>\s*L$",               "BOJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x3EC, 0x003, 0),
            // H&A2=>H, N=>L
            new CodeInfo(@"^H\s*&\s*A2\s*=>\s*H\s*,\s*\$([0-3])\s*=>\s*L$",                     "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x3F0, 0x003, 0),
            // CAJ H+A2=>H, N=>L
            new CodeInfo(@"^CAJ\s+H\s*\+\s*A2\s*=>\s*H\s*,\s*\$([0-3])\s*=>\s*L$",              "CAJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x3F4, 0x003, 0),
            // H|A2=>H, N=>L
            new CodeInfo(@"^H\s*\|\s*A2\s*=>\s*H\s*,\s*\$([0-3])\s*=>\s*L$",                    "",         TokenType.NOP,              CodePattern.OP_VALUE,   0x3F8, 0x003, 0),
            // BOJ H-A2=>H, N=>L
            new CodeInfo(@"^BOJ\s+H\s*-\s*A2\s*=>\s*H\s*,\s*\$([0-3])\s*=>\s*L$",               "BOJ",      TokenType.NOP,              CodePattern.OP_VALUE,   0x3FC, 0x003, 0),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // N=>A11
            new CodeInfo(@"^\$([0-1])\s*=>\s*A11$",                                             "",     TokenType.NOP,              CodePattern.OP_VALUE,   0x400, 0x001, 0),
            new CodeInfo(@"^JMP\s*,\s*\$0\s*=>\s*L\s*,\s*\$([0-1])\s*=>\s*A11$",                  "",     TokenType.NOP,              CodePattern.OP_VALUE,   0x402, 0x001, 0),
            new CodeInfo(@"^\$([0-1])\s*=>\s*D\s*,\s*\$([0-1])\s*=>\s*G\s*,\s*\$([0-1])\s*=>\s*K\s*,\s*\$([0-1])\s*=>\s*S\s*,\s*\$([0-1])\s*=>\s*A11$", "",   TokenType.NOP, CodePattern.OP_VALUE, 0x440,
                0x001, 5,   // D
                0x001, 4,   // G
                0x001, 3,   // K
                0x001, 2,   // S
                0x001, 0    // N
            ),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // BOJ H-K=>H
            new CodeInfo(@"^BOJ\s+H\s*-\s*\$([a-fA-F0-9]{1,2})\s*=>\s*H$",                        "BOJ",  TokenType.SubHKtoH,          CodePattern.OP_VALUE,   0x480, 0x01F, 0),
            // CAJ H+K=>H
            new CodeInfo(@"^CAJ\s+H\s*\+\s*\$([a-fA-F0-9]{1,2})\s*=>\s*H$",                       "CAJ",  TokenType.AddHKtoH,          CodePattern.OP_VALUE,   0x4C0, 0x01F, 0),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // K=>M
            new CodeInfo(@"^\$([a-fA-F0-9]{1,2})\s*=>\s*M$",                                    "",     TokenType.MoveKtoM,         CodePattern.OP_VALUE,   0x500, 0x07F, 0),
            // K=>L,H
            new CodeInfo(@"^\$([a-fA-F0-9]{1,2})\s*=>\s*L\s*,\s*H$",                            "",     TokenType.MoveKtoLH,        CodePattern.OP_VALUE,   0x580, 0x07F, 0),
            // K=>A1
            new CodeInfo(@"^\$([a-fA-F0-9]{1,2})\s*=>\s*A1$",                                   "",     TokenType.MoveKtoA1,        CodePattern.OP_VALUE,   0x600, 0x07F, 0),
            // K=>A2
            new CodeInfo(@"^\$([a-fA-F0-9]{1,2})\s*=>\s*A2$",                                   "",     TokenType.MoveKtoA2,        CodePattern.OP_VALUE,   0x680, 0x07F, 0),
            // K=>A3
            new CodeInfo(@"^\$([a-fA-F0-9]{1,2})\s*=>\s*A3$",                                   "",     TokenType.MoveKtoA3,        CodePattern.OP_VALUE,   0x700, 0x07F, 0),
            // K=>A4
            new CodeInfo(@"^\$([a-fA-F0-9]{1,2})\s*=>\s*A4$",                                   "",     TokenType.MoveKtoA4,        CodePattern.OP_VALUE,   0x780, 0x07F, 0),
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // JP label
            new CodeInfo(@"^JP\s+([a-zA-Z][a-zA-Z0-9_]*)$",                                     "",     TokenType.JP,               CodePattern.OP_LABEL,   0x800, 0x3FF, 0),
            // JP $000
            new CodeInfo(@"^JP\s+\$([a-fA-F0-9]{1,4})$",                                        "",     TokenType.JP,               CodePattern.OP_VALUE,   0x800, 0x3FF, 0),
            // JS label
            new CodeInfo(@"^JS\s+([a-zA-Z][a-zA-Z0-9_]*)$",                                     "",     TokenType.JS,               CodePattern.OP_LABEL,   0xC00, 0x3FF, 0),
            // JS $000
            new CodeInfo(@"^JS\s+\$([a-fA-F0-9]{1,4})$",                                        "",     TokenType.JS,               CodePattern.OP_VALUE,   0xC00, 0x3FF, 0),

            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // IF(M>=$00)   BOJ M-Kのエイリアス
            new CodeInfo(@"^IF\s*\(\s*M\s*>=\s*\$([a-fA-F0-9]{1,2})\s*\)\s*",                       "BOJ",  TokenType.TestSubMK,         CodePattern.OP_VALUE,   0x080, 0x07F, 0),

            // IF(A1&A1,$0=>L)          EQJ A1&A1, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A1\s*&\s*A1\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",                           "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x200, 0x003, 0),
            // IF(A1&A1!=0,$0=>L)          EQJ A1&A1, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A1\s*&\s*A1\s*\!=\s*0\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",                           "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x200, 0x003, 0),
            // IF!(A1&A1,$0=>L)         EQJ/ A1&A1, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\!\(\s*A1\s*&\s*A1\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",               "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x220, 0x003, 0),
            // IF(A1&A1==0,$0=>L)       EQJ/ A1&A1, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A1\s*&\s*A1\s*==\s*0\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",               "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x220, 0x003, 0),

            // IF(A1&A2,$0=>L)          EQJ A1&A2, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A1\s*&\s*A2\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",                "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x210, 0x003, 0),
            // IF!(A1&A2,$0=>L)         EQJ/ A1&A2, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\!\(\s*A1\s*&\s*A2\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",              "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x230, 0x003, 0),
            // IF!(A1=A2,$0=>L)         EQJ A1=A2, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\!\(\s*A1\s*=\s*A2\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",              "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x218, 0x003, 0),
            // IF(A1!=A2,$0=>L)         EQJ A1=A2, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A1\s*\!=\s*A2\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",              "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x218, 0x003, 0),
            // IF(A1=A2,$0=>L)          EQJ/ A1=A2, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A1\s*=\s*A2\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",                "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x238, 0x003, 0),
            // IF(A1==A2,$0=>L)         EQJ/ A1=A2, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A1\s*==\s*A2\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",               "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x238, 0x003, 0),

            // IF(A2&A1,$0=>L)          EQJ A2&A1, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A2\s*&\s*A1\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",                "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x240, 0x003, 0),
            // IF!(A2&A1,$0=>L)         EQJ/ A2&A1, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\!\(\s*A2\s*&\s*A1\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",              "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x260, 0x003, 0),
            // IF!(A2=A1,$0=>L)         EQJ A2=A1, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\!\(\s*A2\s*=\s*A1\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",              "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x248, 0x003, 0),
            // IF(A2!=A1,$0=>L)         EQJ A2=A1, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A2\s*\!=\s*A1\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",              "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x248, 0x003, 0),
            // IF(A2=A1,$0=>L)          EQJ/ A2=A1, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A2\s*=\s*A1\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",                "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x268, 0x003, 0),
            // IF(A2==A1,$0=>L)         EQJ/ A2=A1, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A2\s*==\s*A1\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",               "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x268, 0x003, 0),

            // IF(A1<A2,$0=>L)          BOJ/ A1-A2, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A1\s*<\s*A2\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",                "BOJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x23C, 0x003, 0),
            // IF(A1<=A2,$0=>L)         BOJ A2-A1, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A1\s*<=\s*A2\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",               "BOJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x24C, 0x003, 0),
            // IF(A1>A2,$0=>L)          BOJ/ A2-A1, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A1\s*>\s*A2\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",                "BOJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x26C, 0x003, 0),
            // IF(A1>=A2,$0=>L)         BOJ A1-A2, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A1\s*>=\s*A2\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",               "BOJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x21C, 0x003, 0),
            // IF(A2<A1,$0=>L)          BOJ/ A2-A1, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A2\s*<\s*A1\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",                "BOJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x26C, 0x003, 0),
            // IF(A2<=A1,$0=>L)         BOJ A1-A2, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A1\s*<=\s*A2\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",               "BOJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x21C, 0x003, 0),
            // IF(A2>A1,$0=>L)          BOJ/ A1-A2, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A2\s*>\s*A1\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",                "BOJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x23C, 0x003, 0),
            // IF(A2>=A1,$0=>L)         BOJ A2-A1, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*A2\s*>=\s*A1\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",               "BOJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x24C, 0x003, 0),

            // IF(M&A1,$0=>L)        EQJ M&A1, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*M\s*&\s*A1\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",    "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x280, 0x003, 0),
            // IF!(M&A1,$0=>L)       EQJ/ M&A1, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\!\(\s*M\s*&\s*A1\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",  "EQJ/",  TokenType.NOP,              CodePattern.OP_VALUE,   0x2A0, 0x003, 0),
            // IF(M&A1==0,$0=>L)       EQJ/ M&A1, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*M\s*&\s*A1\s*==\s*0\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",  "EQJ/",  TokenType.NOP,              CodePattern.OP_VALUE,   0x2A0, 0x003, 0),
            // IF(M&A2,$0=>L)        EQJ M&A2, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\(\s*M\s*&\s*A2\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",    "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x290, 0x003, 0),
            // IF!(M&A2,$0=>L)       EQJ/ M&A2, N=>Lのエイリアス
            new CodeInfo(@"^IF\s*\!\(\s*M\s*&\s*A2\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",  "EQJ/",  TokenType.NOP,              CodePattern.OP_VALUE,   0x2B0, 0x003, 0),

            // IF(H!=A1,$0=>L)      EQJ H=A1, N=>L
            new CodeInfo(@"^IF\s*\(\s*H\s*\!=\s*A1\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",    "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x2C8, 0x003, 0),
            // IF(H==A1,$0=>L)      EQJ/ H=A1, N=>L
            new CodeInfo(@"^IF\s*\(\s*H\s*==\s*A1\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",                         "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x2E8, 0x003, 0),
            // IF(H!=A2,$0=>L)      EQJ H=A2, N=>L
            new CodeInfo(@"^IF\s*\(\s*H\s*\!=\s*A2\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",                          "EQJ",  TokenType.NOP,              CodePattern.OP_VALUE,   0x2D8, 0x003, 0),
            // IF(H==A2,$0=>L)      EQJ/ H=A2, N=>L
            new CodeInfo(@"^IF\s*\(\s*H\s*==\s*A2\s*,\s*\$([0-3])\s*=>\s*L\s*\)\s*",                         "EQJ/", TokenType.NOP,              CodePattern.OP_VALUE,   0x2F8, 0x003, 0),



            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            // 疑似命令
            // ORG $000
            new CodeInfo(@"^ORG\s+\$([a-fA-F0-9]{1,3})$",                                       "",     TokenType.ASM_ORG,          CodePattern.ASM_ORG,     0x000),
            new CodeInfo("^TITLE\\s+\"(.*)\"$",                                                 "",     TokenType.ASM_TITLE,        CodePattern.ASM_TITLE,   0x000),
            new CodeInfo("^TITLE\\s+'(.*)'$",                                                   "",     TokenType.ASM_TITLE,        CodePattern.ASM_TITLE,   0x000),
            new CodeInfo(@"^KEY_MAP\s+A([0-9]{1,2})\s*=>\s*S([1-4])$",                          "",     TokenType.ASM_KEY_MAP,      CodePattern.ASM_KEY_MAP, 0x000),

            // 疑似命令 インデックスアクセスの準備
            // A1+offset,$L=>H,L            offset=>L,H : H+A1=>H, $L=>L
            new CodeInfo(@"^A1\s*\+\s*\$([a-fA-F0-9]{1,2})\s*,\s*\$([0-3])\s*=>\s*H\s*,\s*L$",  "", TokenType.ASM_INDEX_A1,    CodePattern.ASM_INDEX_A1, 0x000),
            // A2+offset,$L=>H,L            offset=>L,H : H+A2=>H, $L=>L
            new CodeInfo(@"^A2\s*\+\s*\$([a-fA-F0-9]{1,2})\s*,\s*\$([0-3])\s*=>\s*H\s*,\s*L$",  "", TokenType.ASM_INDEX_A2,    CodePattern.ASM_INDEX_A2, 0x000),

            // 疑似命令 メモリのインデックスアクセス
            // M[A1+$00,$0]=>A1
            new CodeInfo(@"^M\[\s*A1\s*\+\s*\$([a-fA-F0-9]{1,2})\s*,\s*\$([0-3])\]\s*=>\s*A1$", "", TokenType.ASM_MEM_INDEX_A1_A1,    CodePattern.ASM_MEM_INDEX_A1_A1, 0x000),
            // M[A1+$00,$0]=>A2
            new CodeInfo(@"^M\[\s*A1\s*\+\s*\$([a-fA-F0-9]{1,2})\s*,\s*\$([0-3])\]\s*=>\s*A2$", "", TokenType.ASM_MEM_INDEX_A1_A2,    CodePattern.ASM_MEM_INDEX_A1_A2, 0x000),
            // M[A2+$00,$0]=>A1
            new CodeInfo(@"^M\[\s*A2\s*\+\s*\$([a-fA-F0-9]{1,2})\s*,\s*\$([0-3])\]\s*=>\s*A1$", "", TokenType.ASM_MEM_INDEX_A2_A1,    CodePattern.ASM_MEM_INDEX_A2_A1, 0x000),
            // M[A2+$00,$0]=>A2
            new CodeInfo(@"^M\[\s*A2\s*\+\s*\$([a-fA-F0-9]{1,2})\s*,\s*\$([0-3])\]\s*=>\s*A2$", "", TokenType.ASM_MEM_INDEX_A2_A2,    CodePattern.ASM_MEM_INDEX_A2_A2, 0x000),

            // A2=>M[A1+$00,$0]
            new CodeInfo(@"^A2\s*=>\s*M\[\s*A1\s*\+\s*\$([a-fA-F0-9]{1,2})\s*,\s*\$([0-3])\]$", "", TokenType.ASM_MEM_W_INDEX_A1_A2,    CodePattern.ASM_MEM_W_INDEX_A1_A2, 0x000),
            // A1=>M[A2+$00,$0]
            new CodeInfo(@"^A1\s*=>\s*M\[\s*A2\s*\+\s*\$([a-fA-F0-9]{1,2})\s*,\s*\$([0-3])\]$", "", TokenType.ASM_MEM_W_INDEX_A2_A1,    CodePattern.ASM_MEM_W_INDEX_A2_A1, 0x000),
            // $00=>M[A1+$00,$0]
            new CodeInfo(@"^\$([a-fA-F0-9]{1,2})\s*=>\s*M\[\s*A1\s*\+\s*\$([a-fA-F0-9]{1,2})\s*,\s*\$([0-3])\]$", "", TokenType.ASM_MEM_W_INDEX_A1_K,    CodePattern.ASM_MEM_W_INDEX_A1_K, 0x000),
            // $00=>M[A2+$00,$0]
            new CodeInfo(@"^\$([a-fA-F0-9]{1,2})\s*=>\s*M\[\s*A2\s*\+\s*\$([a-fA-F0-9]{1,2})\s*,\s*\$([0-3])\]$", "", TokenType.ASM_MEM_W_INDEX_A2_K,    CodePattern.ASM_MEM_W_INDEX_A2_K, 0x000),
        };
	}
}
