using System.Text.RegularExpressions;

namespace asm777
{
	internal class LineLexer
	{
        private string text;
        private int current;
        private int size;

        public LineLexer(string InLine) {
            // コメント削除
            InLine = removeComment(InLine);

            text = InLine;
            current = 0;
            size = text.Length;
        }

        private int getChar()
        {
            if(current >= size) {
                current++;
                return -1;
            }
            return text[current++];
        }
        private void ungetChar()
        {
            if(current > 0) {
                current--;
            }
        }

        /// <summary>
        /// 空白文字かどうか
        /// </summary>
        /// <param name="ch">文字</param>
        /// <returns>空白文字ならtrue</returns>
        private bool isSpace(int ch)
        {
            return (ch == ' ') || (ch == '\t');
        }

        private bool isAlpha(int ch)
        {
            return ('a' <= ch && ch <= 'z')
                || ('A' <= ch && ch <= 'Z');
        }

        private bool isNumber(int ch)
        {
            return ('0' <= ch && ch <= '9');
        }

        private bool isHexNumber(int ch)
        {
            return ('0' <= ch && ch <= '9')
                || ('a' <= ch && ch <= 'f')
                || ('A' <= ch && ch <= 'F');
        }

        /// <summary>
        /// 空白文字をスキップする
        /// </summary>
        private void skipSpace()
        {
            while(isSpace(getChar())) {}
            ungetChar();
        }
        private void skip(int size)
        {
            current += size;
        }

        private string getRemain()
        {
            return text.Substring(current);
        }


        public Token parse()
        {
            skipSpace();

            // Matching
            {
                string mnemonic = getRemain().Trim();
                if(mnemonic == "") {
                    return new Token(TokenType.EOF);
                }

                // for DEBUG
                /*
                {
                    int count = 0;
                    foreach(var c in CodeInfo.codesInfoList) {
                        var match = Regex.Match(mnemonic, c.mnemonic);
                        if (match.Success)
                        {
                            // Console.Out.WriteLine(c.mnemonic);
                            count++;
                        }
                    }
                    if(count >= 2) {
                        throw new Exception("重複している:" + mnemonic);
                    }
                }
                */

                foreach(var c in CodeInfo.codesInfoList) {
                    var match = Regex.Match(mnemonic, c.mnemonic);
                    if (match.Success) {
                        skip(match.Value.Length);
                        return new Token(c.tokenType, match.Value, c, match.Groups);
                    }
                }
            }



            int ch = getChar();
            if (isAlpha(ch)) {
                string value = "";
                while(isAlpha(ch) || isNumber(ch) || (ch == '_')) {
                    value += (char)ch;
                    ch = getChar();
                }
                if(ch == ':') {
                    // ラベル
                    // [a-zA-Z][a-zA-Z0-9_]*:
                    return new Token(TokenType.LABEL, value);
                } else {
                    return new Token(TokenType.ERROR);
                }
            } else if(ch == '$') {
                // 16進の数値
                // $[0-9a-fA-F]{1,3}
                ch = getChar();
                string value = "";
                if(!isHexNumber(ch)) {
                    // 「$」の後に数値が無い
                    return new Token(TokenType.ERROR);
                }
                value += (char)ch;

                ch = getChar();
                if(!isHexNumber(ch)) {
                    // $0
                    ungetChar();
                    return new Token(TokenType.LITERAL, value);
                }
                value += (char)ch;

                ch = getChar();
                if(!isHexNumber(ch)) {
                    // $00
                    ungetChar();
                    return new Token(TokenType.LITERAL, value);
                }
                value += (char)ch;

                // $000
                return new Token(TokenType.LITERAL, value);
            } else if(ch <= 0) {
                return new Token(TokenType.EOF);
            } else {
                return new Token(TokenType.ERROR);
            }
        }

        private string removeComment(string InLine) {
            var pos = InLine.IndexOf(';');
            if (pos >= 0) {
                InLine = InLine.Substring(0,pos);
            }
            return InLine.Trim();
        }
	}
}
