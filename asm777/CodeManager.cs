using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace asm777
{
	internal class CodeManager
	{
        public string title = "";
        public byte[] keyMap = new Byte[] {0,0,0,0,0,0,0,0};

        class Bin {
            public int lineNo;
            public int pc;
            public int code;

            public Bin(int lineNo, int pc, int code)
            {
                this.lineNo = lineNo;
                this.pc = pc;
                this.code = code;
            }
        }
        List<Bin> program = new List<Bin>();
        int[] paseSize = new int[16];
        public int pass;
        public CodeManager() {}
        public void write(int lineNo, int pc, int code)
        {
            if(pass == 1) {
            } else {
                if((pc & 0x7F) == 0) {
                    Console.Out.WriteLine("// ----------------------------");
                    Console.Out.WriteLine("// PAGE $" + (pc & ~0x7F).ToString("X3"));
                    Console.Out.WriteLine("// ----------------------------");
                }
//                Console.Out.WriteLine("0x" + pc.ToString("X03") + ", 0x" + code.ToString("X3") + ", // src line no." + (lineNo + 1));
                Console.Out.WriteLine("0x" + pc.ToString("X03") + ", 0x" + code.ToString("X3") + ",");

                program.Add(new Bin(lineNo, pc, code));
                paseSize[pc / 0x80]++;
            }
        }

        void WriteFileHeader(BinaryWriter writer, string Title, byte[] KeyMap) {
            //書き込む処理
            var MagicNumber = new Byte[] {0x5F, 0x43, 0x61, 0x73, 0x73, 0x65, 0x74, 0x74, 0x65, 0x56, 0x69, 0x73, 0x69, 0x6F, 0x6E, 0x5F};
            var FormatVersion = new Byte[] {0x30, 0x30, 0x30, 0x30};
            writer.Write(MagicNumber);
            writer.Write(FormatVersion);
            writer.Write((Int32)0); // Padding1
            writer.Write(KeyMap);
            byte[] TitleUTF8 = System.Text.Encoding.UTF8.GetBytes(Title);
            Array.Resize<byte>(ref TitleUTF8, 224);
            writer.Write(TitleUTF8);
        }

        public void WriteFile(string fileName) {
            using (var writer = new BinaryWriter(new FileStream(fileName, FileMode.Create), System.Text.Encoding.UTF8))
            {
                WriteFileHeader(writer, title, keyMap);
                foreach (var e in program) {
                    //書き込む処理
                    writer.Write((Int16)e.pc);
                    writer.Write((Int16)e.code);
                }
            }
        }

        public void WritePageInfo() {
            int i = 0;
            int codeSize = 0;
            foreach (var e in paseSize) {
                if(i == 8) {
                   Console.WriteLine("// ===  A11 Wall  ==================================");
                }
                Console.WriteLine("// Page $" + (i * 0x80).ToString("X3") + " Usage size:$" + e.ToString("X2") + " Unused size:$" + (0x7F - e).ToString("X2") + " " + (0x7F - e));
                ++i;
                codeSize += e;
            }
            Console.WriteLine("// Total code size $" + codeSize.ToString("X3") + " " + codeSize );
            Console.WriteLine("// Free  area size $" + (0x7F0 - codeSize).ToString("X3") + " " + (0x7F0 - codeSize) );
        }
        public void WriteList() {
            WritePageInfo();
        }
	}
}
