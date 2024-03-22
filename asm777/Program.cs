namespace asm777
{
	internal class Program
    {
        private static void doOneLine(int pass, LabelManager labelManager, CodeManager codeManager, int lineNo, ref int address, string text) {
            var lex = new LineLexer(text);
            // ラベルを処理
            Token token = lex.parse();
            while(token.tokenType == TokenType.LABEL) {
                labelManager.register(address, token.literal);
                token = lex.parse();
            }

            while(true) {
                if(token.tokenType == TokenType.ERROR) {
                    throw new Exception("LineNo." + (lineNo + 1) + ":" + text);
                }
                if(token.tokenType == TokenType.EOF) {
                    return;
                }

                // 命令を処理
                if(token.codeInfo != null) {
                    var c = token.codeInfo;
                    switch(c.codePattern) {
                        case CodePattern.NO_OPERAND:
                            codeManager.write(lineNo, address, c.codeBase);
                            break;
                        case CodePattern.OP_LABEL: // xxx LABEL
                            {
                                int code = c.codeBase;
                                for(int index = 0; index < c.param.Count; ++index) {
                                    string labelName = token.groups[index + 1].Value;
                                    int param = labelManager.searchLabelAddress(labelName);
                                    code |= (param & c.param[index].mask) << c.param[index].shift;
                                }
                                codeManager.write(lineNo, address, code);
                            }
                            break;

                        case CodePattern.OP_VALUE:
                            {
                                int code = c.codeBase;
                                for(int index = 0; index < c.param.Count; ++index) {
                                    string paramString = token.groups[index + 1].Value;
                                    int param = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);
                                    code |= (param & c.param[index].mask) << c.param[index].shift;
                                }
                                codeManager.write(lineNo, address, code);
                            }
                            break;

                        case CodePattern.OP_LH:
                            {
                                int code = c.codeBase;
                                string paramString = token.groups[1].Value;
                                int param = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);
                                code |= (param & c.param[0].mask) << c.param[0].shift;
                                code |= (param & c.param[1].mask) << c.param[1].shift;
                                codeManager.write(lineNo, address, code);
                            }
                            break;
                        case CodePattern.ASM_ORG:
                            {
                                string paramString = token.groups[1].Value;
                                address = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);
                            }
                            return;
                        case CodePattern.ASM_TITLE:
                            {
                                string paramString = token.groups[1].Value;
                                codeManager.title = paramString;
                            }
                            return;
                        case CodePattern.ASM_KEY_MAP_A:
                            {
                                string paramString = token.groups[1].Value;
                                int pin = Int32.Parse(paramString);
                                paramString = token.groups[2].Value;
                                int s = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);
                                if(pin < 8 || pin > 12) {
                                    throw new Exception("LineNo." + (lineNo + 1) + " out of range for pin number:" + token.tokenType);
                                }
                                codeManager.keyMapA[pin-8] = (byte)s;
                            }
                            return;
                        case CodePattern.ASM_KEY_MAP_B:
                            {
                                string paramString = token.groups[1].Value;
                                int pin = Int32.Parse(paramString);
                                paramString = token.groups[2].Value;
                                int s = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);
                                if(s > 0x7F) {
                                    throw new Exception("LineNo." + (lineNo + 1) + " out of range assignment value:" + token.tokenType);
                                }
                                if(pin < 9 || pin > 15) {
                                    throw new Exception("LineNo." + (lineNo + 1) + " out of range for pin number:" + token.tokenType);
                                }
                                codeManager.keyMapB[pin-9] = (byte)s;
                            }
                            return;
                        case CodePattern.ASM_INDEX_A1:
                            {
                                string paramString = token.groups[1].Value;
                                int offset = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);
                                paramString = token.groups[2].Value;
                                int l = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);

                                codeManager.write(lineNo, address, 0x580 | offset); //     offset => L,H
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x3E4 | l);      // CAJ H+A1=>H, l=>L
                            }
                            break;
                        case CodePattern.ASM_INDEX_A2:
                            {
                                string paramString = token.groups[1].Value;
                                int offset = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);
                                paramString = token.groups[2].Value;
                                int l = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);

                                codeManager.write(lineNo, address, 0x580 | offset); //     offset => L,H
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x3F4 | l);      // CAJ H+A2=>H, l=>L
                            }
                            break;
                        
                        case CodePattern.ASM_MEM_INDEX_A1_A1:
                            {
                                string paramString = token.groups[1].Value;
                                int offset = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);
                                paramString = token.groups[2].Value;
                                int l = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);

                                codeManager.write(lineNo, address, 0x580 | offset); //     offset => L,H
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x3E4 | l);      // CAJ H+A1=>H, l=>L
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x38C | l);      //     M=>A1, l=>L
                            }
                            break;
                        case CodePattern.ASM_MEM_INDEX_A1_A2:
                            {
                                string paramString = token.groups[1].Value;
                                int offset = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);
                                paramString = token.groups[2].Value;
                                int l = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);

                                codeManager.write(lineNo, address, 0x580 | offset); //     offset => L,H
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x3E4 | l);      // CAJ H+A1=>H, l=>L
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x39C | l);      //     M=>A2, l=>L
                            }
                            break;
                        case CodePattern.ASM_MEM_INDEX_A2_A1:
                            {
                                string paramString = token.groups[1].Value;
                                int offset = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);
                                paramString = token.groups[2].Value;
                                int l = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);

                                codeManager.write(lineNo, address, 0x580 | offset); //     offset => L,H
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x3F4 | l);      // CAJ H+A2=>H, l=>L
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x38C | l);      //     M=>A1, l=>L
                            }
                            break;
                        case CodePattern.ASM_MEM_INDEX_A2_A2:
                            {
                                string paramString = token.groups[1].Value;
                                int offset = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);
                                paramString = token.groups[2].Value;
                                int l = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);

                                codeManager.write(lineNo, address, 0x580 | offset); //     offset => L,H
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x3F4 | l);      // CAJ H+A2=>H, l=>L
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x39C | l);      //     M=>A2, l=>L
                            }
                            break;

                        case CodePattern.ASM_MEM_W_INDEX_A1_A2:
                            {
                                string paramString = token.groups[1].Value;
                                int offset = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);
                                paramString = token.groups[2].Value;
                                int l = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);

                                codeManager.write(lineNo, address, 0x580 | offset); //     offset => L,H
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x3E4 | l);      // CAJ H+A1=>H, l=>L
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x390 | l);      //     A2=>M, l=>L
                            }
                            break;
                        case CodePattern.ASM_MEM_W_INDEX_A2_A1:
                            {
                                string paramString = token.groups[1].Value;
                                int offset = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);
                                paramString = token.groups[2].Value;
                                int l = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);

                                codeManager.write(lineNo, address, 0x580 | offset); //     offset => L,H
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x3F4 | l);      // CAJ H+A2=>H, l=>L
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x380 | l);      //     A1=>M, l=>L
                            }
                            break;
                        case CodePattern.ASM_MEM_W_INDEX_A1_K:
                            {
                                string paramString = token.groups[1].Value;
                                int K = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);
                                paramString = token.groups[2].Value;
                                int offset = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);
                                paramString = token.groups[3].Value;
                                int l = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);

                                codeManager.write(lineNo, address, 0x580 | offset); //     offset => L,H
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x3E4 | l);      // CAJ H+A1=>H, l=>L
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x500 | K);      //     K=>M, l=>L
                            }
                            break;
                        case CodePattern.ASM_MEM_W_INDEX_A2_K:
                            {
                                string paramString = token.groups[1].Value;
                                int K = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);
                                paramString = token.groups[2].Value;
                                int offset = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);
                                paramString = token.groups[3].Value;
                                int l = Int32.Parse(paramString, System.Globalization.NumberStyles.HexNumber);

                                codeManager.write(lineNo, address, 0x580 | offset); //     offset => L,H
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x3F4 | l);      // CAJ H+A2=>H, l=>L
                                address = nextPCAddress((ushort)address);
                                codeManager.write(lineNo, address, 0x500 | K);      //     K=>M, l=>L
                            }
                            break;

                    }
                } else {
                    switch(token.tokenType) {
                        case TokenType.LITERAL:
                            {
                                int directCode = Int32.Parse(token.literal, System.Globalization.NumberStyles.HexNumber);
                                codeManager.write(lineNo, address, directCode);
                            }
                            break;
                        default:
                            throw new Exception("LineNo." + (lineNo + 1) + " unknown token type:" + token.tokenType);
                    }
                }

                // PAGEチェック
                if((address & 0x7F) == 0x40) {
                    throw new Exception("LineNo." + (lineNo + 1) + " page size over:" + address.ToString("X3") + " >= 127byte");
                }
                address = nextPCAddress((ushort)address);

                token = lex.parse();
            }
        }
        private static  void doPass(int pass, LabelManager labelManager, CodeManager codeManager, string[] text)
        {
            int address = 0;
            for(int lineNo = 0; lineNo < text.Length; ++lineNo) {
                doOneLine(pass, labelManager, codeManager, lineNo, ref address, text[lineNo]);
            }
        }

        private static ushort nextPCAddress(ushort address)
        {
            ushort a6 = (ushort)((address >> 5) & 1);
            ushort a7 = (ushort)((address >> 6) & 1);
            ushort a1 = (ushort)(1 ^ (a6 ^ a7));
            return (ushort)((((address << 1) | a1) & 0x7F) | (address & 0x780));
        }

        private static void WriteList(LabelManager labelManager, CodeManager codeManager)
        {
            Console.WriteLine("// Labels");
            labelManager.WriteList();
            Console.WriteLine();
            Console.WriteLine("// Page Info.");
            codeManager.WriteList();
        }

        private static void Help() {
            Console.WriteLine("microAssembler777 Version 00.03.22");
            Console.WriteLine(System.AppDomain.CurrentDomain.FriendlyName + " <INPUT> <OUTPUT>");
        }

        static void Main(string[] args)
        {
            if(args.Length < 2) {
                Help();
                Environment.Exit(-1);
                return;
            }

            string input = args[0];
            string outputFilename = args[1];
            string[] text = File.ReadAllLines(input);

            var labelManager = new LabelManager();
            var codeManager = new CodeManager();
            for(int pass = 1; pass <= 2; ++pass) {
                labelManager.pass = pass;
                codeManager.pass = pass;
                doPass(pass, labelManager, codeManager, text);
            }
            codeManager.WriteFile(outputFilename);
            WriteList(labelManager, codeManager);
        }
    }
}
