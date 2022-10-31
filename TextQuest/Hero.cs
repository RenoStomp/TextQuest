using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class Hero
    {
        public double HP { get; set; }
        public double Mana { get; set; }
        public string Name { get; set; }

        //public Item CurrentItem { get; set; } /*= null;*/

        public List<Items> Inventory { get; set; } = new();
        public List<Items> Weapons { get; set; } = new();
        public List<Items> Armor { get; set; } = new();
        public List<Items> Poisons { get; set; } = new();

        public int Randomaizer(int from, int to)
        {
            Random random = new();
            return random.Next(from, to);
        }
        public Hero(string name, double hp)
        {
            HP = hp;
            Name = name;
        }
        public double Bite(string name)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{Name} filling his jaws with the power of all of his ancestors, " +
                $"move to the {name} and... \nBITES HIM AS HARD AS IT POSSIBLE!");
            Console.ForegroundColor = ConsoleColor.White;
            double damage = Randomaizer(1, 3);
            return damage;
        }
        public void TakesDamage(double damage)
        {
            if (damage > 0)
            {
                if (Armor.Count > 0)
                {
                    double defence = Armor.Sum(t => t.PhysycalDefence);
                    defence *= damage;
                    damage -= defence;
                    Console.WriteLine();
                    Console.WriteLine($"{Name} reflects {Math.Round(defence, 2)} but receives {Math.Round(damage, 2)} damage");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"{Name} receives {Math.Round(damage, 2)} damage");
                }
                HP -= damage;
            }
        }
        public double Attack(string name)
        {
            if (Poisons.Count > 0)
            {

                Console.WriteLine("[1] - HEAL       [2] - ATTACK");
                Console.WriteLine();
                ConsoleKeyInfo whatDo;
                int testing;
                do
                {
                    whatDo = Console.ReadKey(true);
                    _ = int.TryParse(whatDo.KeyChar.ToString(), out testing);

                } while (!Char.IsNumber(whatDo.KeyChar) || testing < 1 || testing > 2);

                if (testing == 1)
                {
                    Console.WriteLine("Choose the poison you want to use now:");

                    for (int i = 0, num = 1; i < Poisons.Count; i++, num++)
                    {
                        Console.WriteLine($"[{num}] {Poisons[i].Name} - {Poisons[i].Healing} heal points");
                    }

                    ConsoleKeyInfo poisonNum;
                    int poison;
                    do
                    {
                        poisonNum = Console.ReadKey(true);
                        int.TryParse(poisonNum.KeyChar.ToString(), out poison);
                    } while (!Char.IsNumber(poisonNum.KeyChar) || poison > Poisons.Count || poison < 1);

                    int correctPoisonNum = Convert.ToInt16(poisonNum.KeyChar.ToString()) - 1;
                    HealLine(correctPoisonNum);

                    double heal = Poisons[correctPoisonNum].Healing;
                    HP += heal;
                    Console.WriteLine($"Healing {heal} HP");

                    Poisons.Remove(Poisons[correctPoisonNum]);
                    return 0;
                }
            }

            double damage = 0;
            Console.WriteLine();

            if (Weapons.Count == 0)
            {
                int chance = Randomaizer(1, 21);
                if (chance < 21)
                {
                    damage = Bite(name);
                }
                Console.WriteLine($"{damage} damage");
                return damage;
            }

            Console.WriteLine("No healing poisons atm");
            Console.WriteLine();
            Console.WriteLine($"Choose which weapon will you use for your hit: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            int weaponsCount = 0;
            for (int i = 0; i < Weapons.Count; i++)
            {
                int displayNum = i + 1;
                Console.WriteLine($"[{displayNum}] {Weapons[i].Name} - {Weapons[i].Damage}");
                weaponsCount++;
            }
            Console.WriteLine();
            ConsoleKeyInfo weaponNum;
            int test;
            do
            {
                weaponNum = Console.ReadKey(true);
                int.TryParse(weaponNum.KeyChar.ToString(), out test);
            } while (!Char.IsNumber(weaponNum.KeyChar) || test > weaponsCount || test < 1);

            int correctWeaponNum = Convert.ToInt16(weaponNum.KeyChar.ToString()) - 1;
            WeaponAttackLine(correctWeaponNum);

            damage = Weapons[correctWeaponNum].Damage;
            int luckyExtra = Randomaizer(0, 100);
            if (luckyExtra < 34)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine();
                Console.WriteLine("CRITICAL HIT!");
                Console.WriteLine();
                Console.WriteLine("EXTRA DAMAGE!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"{damage} + 20 damage!!!");

                damage += 20;
            }
            else
            {
                Console.WriteLine($"{damage} damage");
            }
            return damage;
            
        }
        public void HealLine(int poisonNum)         //дописать хил лайны
        {
            string poison = Poisons[poisonNum].Name;
            string phrase = "";

            Console.ForegroundColor = ConsoleColor.Green;
            if (Poisons[poisonNum].Equals(ItemStyles.weak))
            {
                phrase = "healLine";
            }
            else if (Poisons[poisonNum].Equals(ItemStyles.normal))
            {
                phrase = "healLine";
            }
            else if (Poisons[poisonNum].Equals(ItemStyles.strong))
            {
                phrase = "healLine";
            }
            else if (Poisons[poisonNum].Equals(ItemStyles.swordHP))
            {
                phrase = "healLine";
            }
            else if (Poisons[poisonNum].Equals(ItemStyles.eye))
            {
                phrase = "healLine";
            }
            else if (Poisons[poisonNum].Equals(ItemStyles.ball))
            {
                phrase = "healLine";
            }
            Console.WriteLine($"You grab {poison} {phrase}");

            Console.ForegroundColor = ConsoleColor.White;

        }
        public void WeaponAttackLine(int weaponNum)
        {
            string weapon = Weapons[weaponNum].Name;
            string randomPhrase = "";

            Console.ForegroundColor = ConsoleColor.Green;
            if (Weapons[weaponNum].Style.Equals(ItemStyles.mace))
            {
                randomPhrase = MacePhrases[new Random().Next(0, 5)];
            }
            else if (Weapons[weaponNum].Style.Equals(ItemStyles.sword))
            {
                randomPhrase = SwordPhrases[new Random().Next(0, 5)];
            }
            else if (Weapons[weaponNum].Style.Equals(ItemStyles.bow))
            {
                randomPhrase = BowPhrases[new Random().Next(0, 5)];
            }

            Console.WriteLine($"You take your {weapon} {randomPhrase}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public string[] MacePhrases = new string[5]
        {
            "comes closer and start beating the shit out of that bastard",   //1
            "throw it to him as hard as you can and while he shaking his head, run to your weapon and take it back",        //2
            "trying to stick it in his butt but he instantly jumps away and you just beat his head with it",     //3
            "maceline",     //4
            "maceline"      //5
        };
        public string[] SwordPhrases = new string[5]
        {
            "and start chopping this bitch on peaces with your sword",
            "swordline",
            "swordline",
            "swordline",
            "swordline"
        };
        public string[] BowPhrases = new string[5]
        {
            "bowline",
            "bowline",
            "bowline",
            "bowline",
            "bowline"
        };
        
        public bool ArmsFree { get; set; } = true;
        public bool LegsFree { get; set; } = true;
        public bool BodyFree { get; set; } = true;
        public bool HeadFree { get; set; } = true;

        public bool SwordFree { get; set; } = true;
        public bool MaceFree { get; set; } = true;
        public bool BowFree { get; set; } = true;
    }

    //public enum Item
    //{
    //    Arms,
    //    Legs,
    //    Body,
    //    Head,

    //    Sword,
    //    Mace,
    //    Bow,
    //    Not
    //}
}
