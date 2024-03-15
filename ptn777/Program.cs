using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.ExceptionServices;
using static System.Net.Mime.MediaTypeNames;

namespace ptn777
{
	internal class Program
	{
        private static void WriteFileHeader(BinaryWriter writer, List<int> attribute) {
            //書き込む処理
            byte[] attr = new byte[16];
            {
                int i = 0;
                foreach(var e in attribute) {
                    attr[i++] = (byte)e;
                }
                attr[i++] = 0xFF;
            }

            var MagicNumber = new Byte[] {0x2A, 0x43, 0x61, 0x73, 0x73, 0x65, 0x74, 0x74, 0x65, 0x56, 0x69, 0x73, 0x69, 0x6F, 0x6E, 0x2A};
            var FormatVersion = new Byte[] {0x30, 0x30, 0x30, 0x30};
            writer.Write(MagicNumber);
            writer.Write(FormatVersion);
            writer.Write((Int32)0); // Padding1
            writer.Write((Int32)0); // Padding2
            writer.Write((Int32)0); // Padding3
            writer.Write(attr);     // 属性
        }

        private static void WriteBinary(string filename, int[] ptn7x7, int[] ptn8x7, List<int> attribute)
        {
            using (var writer = new BinaryWriter(new FileStream(filename, FileMode.Create), System.Text.Encoding.UTF8))
            {
                WriteFileHeader(writer, attribute);
                foreach (var e in ptn7x7) {
                    writer.Write((byte)e);
                }
                foreach (var e in ptn8x7) {
                    writer.Write((byte)e);
                }
            }
        }

        private static void WriteList(int[] ptn7x7, int[] ptn8x7, List<int> attribute)
        {
            // 7x7 書き出す
            Console.Out.WriteLine("const u8 PD777::patternRom[686] = {");
            int ptnNo = 0;
            for(int i = 0; i < ptn7x7.Length; ++i) {
                string text;
                if((i % 7) == 0) {
                    if((ptnNo % 8) == 7) {
                        ptnNo++;
                        Console.Out.WriteLine();
                    }
                    text = "    // 0x" + ptnNo.ToString("X2");
                    Console.Out.WriteLine(text);
                    ptnNo++;
                }

                text = "    0b";
                for(int j = 0; j < 7; ++j) {
                    if((ptn7x7[i] & (0x40 >> j)) != 0) {
                        text += "1";
					} else {
                        text += "0";
                    }
                }
                text += ",";
                Console.Out.WriteLine(text);
            }
            Console.Out.WriteLine("};");
            Console.Out.WriteLine();
            // 8x7 書き出す
            Console.Out.WriteLine("const u8 PD777::patternRom8[98] = {");
            ptnNo = 0x70;
            for(int i = 0; i < ptn8x7.Length; ++i) {
                string text;
                if((i % 7) == 0) {
                    if((ptnNo % 8) == 7) {
                        ptnNo++;
                        Console.Out.WriteLine();
                    }
                    text = "    // 0x" + ptnNo.ToString("X2");
                    Console.Out.WriteLine(text);
                    ptnNo++;
                }

                text = "    0b";
                for(int j = 0; j < 8; ++j) {
                    if((ptn8x7[i] & (0x80 >> j)) != 0) {
                        text += "1";
					} else {
                        text += "0";
                    }
                }
                text += ",";
                Console.Out.WriteLine(text);
            }
            Console.Out.WriteLine("};");
            Console.Out.WriteLine();
            // ベント情報
            Console.Out.WriteLine("const u8 PD777::characterAttribute[0x80*2] = {");
            foreach(int attr in attribute) {
                // ベント属性は、横一列（７個のパターン）単位
                for(int i = 0; i < 7; ++i) {
                    Console.Out.WriteLine("    0x" + (attr + i).ToString("X2") + ", 0x0C,");
                }
            }
            Console.Out.WriteLine("    0xFF, 0");
            Console.Out.WriteLine("};");
        }

        private static bool isPixelOne(Color color) {
            if(color.A == 0x00) {
                return false;   // 透明なら0
            }
            // 黒以外は1
            return (color.R != 0x00)
                || (color.G != 0x00)
                || (color.B != 0x00);
        }
        private static bool isPixelBent(Color color) {
            if(color.A == 0x00) {
                return false;   // 透明なら0
            }
            // 赤色があるパターンはBent
            return (color.R == 0xFF)
                && (color.G == 0x00)
                && (color.B == 0x00);
        }

        private static int[] ReadPatternFile(string patternFilename, ref List<int> attribute)
        {
            int width, height;
            using (Bitmap img = new Bitmap(patternFilename))
            {
                //画像サイズを取得
                width = img.Width;
                height = img.Height;
                if(width == 7*7 && height == 14*7) {
                    // 7*7パターン
                    int index = 0;
                    int[] ptn = new int[7 * 14 * 7];
                    int ptnNo = 0;
                    for(int offsetY = 0; offsetY < height; offsetY += 7) {
                        bool bentPattern = false;
                        for(int offsetX = 0; offsetX < width; offsetX += 7) {
                            for(int y = 0; y < 7; ++y) {
                                int value = 0;
                                for(int x = 0; x < 7; ++x) {
                                    Color pixel = img.GetPixel(offsetX + x, offsetY + y);
                                    if(isPixelOne(pixel)) {
                                        value |= 0x40 >> x;
                                    }
                                    if(isPixelBent(pixel)) {
                                        bentPattern = true;
                                    }
                                }
                                ptn[index + y] = value;
						    }
                            index += 7;
                        }
                        if(bentPattern) {
                            // ベント属性は、横一列（７個のパターン）単位
                            attribute.Add(ptnNo);
                        }
                        ptnNo += 8;
                    }
                    return ptn;
                } else if(width == 8*7 && height == 2*7) {
                    // 8*7パターン
                    int index = 0;
                    int[] ptn = new int[7 * 2 * 7];
                    for(int offsetY = 0; offsetY < height; offsetY += 7) {
                        for(int offsetX = 0; offsetX < width; offsetX += 8) {
                            for(int y = 0; y < 7; ++y) {
                                int value = 0;
                                for(int x = 0; x < 8; ++x) {
                                    Color pixel = img.GetPixel(offsetX + x, offsetY + y);
                                    if(isPixelOne(pixel)) {
                                        value |= 0x80 >> x;
                                    }
                                }
                                ptn[index + y] = value;
						    }
                            index += 7;
                        }
                    }
                    return ptn;
                } else {
                    throw new Exception("画像サイズが変です。49x98または56x14にしてください");
                }
            }
		}
        private static void Help() {
            Console.WriteLine("microPatternConverter777 Version 00.00.00");
            Console.WriteLine(System.AppDomain.CurrentDomain.FriendlyName + " <INPUT 7x7> <INPUT 8x7> <OUTPUT>");
        }

		static void Main(string[] args)
		{
            if(args.Length < 3) {
                Help();
                Environment.Exit(-1);
                return;
            }

            string inputFilename7x7 = args[0];
            string inputFilename8x7 = args[1];
            string outputFilename   = args[2];

            List<int> attribute = new List<int>();
            int[] ptn7x7 = ReadPatternFile(inputFilename7x7, ref attribute);
            int[] ptn8x7 = ReadPatternFile(inputFilename8x7, ref attribute);
            WriteList(ptn7x7, ptn8x7, attribute);
            WriteBinary(outputFilename, ptn7x7, ptn8x7, attribute);
        }
	}
}
