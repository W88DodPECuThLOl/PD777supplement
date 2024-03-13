using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace asm777
{
	internal class Token
	{
        public TokenType tokenType { get; }
        public string literal { get; }

        public CodeInfo? codeInfo;
        public GroupCollection groups;

        public int codeBase;

        public Token(TokenType InType, string InLiteral, CodeInfo codeInfo, GroupCollection groups) {
            tokenType = InType;
            literal = InLiteral;
            this.codeInfo = codeInfo;
            this.groups = groups;
            codeBase = 0;
        }

        public Token(TokenType InType, string InLiteral = "")
        {
            tokenType = InType;
            literal = InLiteral;
            this.codeInfo = null;
            codeBase = 0;
        }
	}
}
