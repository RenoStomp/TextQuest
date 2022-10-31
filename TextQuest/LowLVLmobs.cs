using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class LowLVLmobs
    {
        string Name { get; set; }
        double HP { get; set; }
        public LowLVLmobs(string name, double hp)
        {
            Name = name;
            HP = hp;
        }

    }
}
