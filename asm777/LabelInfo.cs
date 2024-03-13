using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asm777
{
	internal class LabelInfo
	{
        public LabelInfo(int address, string name) { this.address = address; this.name = name; }
        public int address;
        public string name;
	}
}
