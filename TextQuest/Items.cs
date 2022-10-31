using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class Items
    {
        public string Name { get; set; }
        public double PhysycalDefence { get; set; } = 0;
        public double Damage { get; set; } = 0;
        public double Healing { get; set; } = 0;
        public string Style { get; set; } = string.Empty;
        public Items(string name, double physycalDefence, string style)
        {
            Name = name;
            PhysycalDefence = physycalDefence;
            Style = style;
        }
        public Items(string name, string style, double damage)
        {
            Name = name;
            Style = style;
            Damage = damage;
        }
        public Items(string name, double healing)
        {
            Healing = healing;
            Name = name;
        }
    }
}
