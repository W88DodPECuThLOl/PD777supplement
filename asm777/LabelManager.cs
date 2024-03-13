using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asm777
{
	internal class LabelManager
	{
        Dictionary<string,LabelInfo> labels = new Dictionary<string, LabelInfo>();
        public int pass;

        public LabelManager() {}
        public void register(int address, string InName)
        {
            if(pass == 1) {
                labels.Add(InName, new LabelInfo(address, InName));
            }
        }
        public int searchLabelAddress(string InName)
        {
            if(pass == 1) {
                return 0;
            } else {
                if(!labels.ContainsKey(InName)) {
                    Console.Error.WriteLine("Undefined label:" + InName);
                }

                return labels[InName].address;
            }
        }

        public void WriteList() {
            foreach(var (key, value) in labels) {
                Console.WriteLine( "// $" + value.address.ToString("X3") + " " + key);
            }
        }
	}
}
