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
            if (Poisons.Count > 0)                  //if you have any healing poisons you can choose: use it or use weapons
            {

                Console.WriteLine("[1] - HEAL       [2] - ATTACK");
                Console.WriteLine();
                ConsoleKeyInfo whatDo;
                int testing;
                do
                {
                    whatDo = Console.ReadKey(true);
                    int.TryParse(whatDo.KeyChar.ToString(), out testing);

                } while (!Char.IsNumber(whatDo.KeyChar) || testing < 1 || testing > 2);

                if (testing == 1)
                {
                    Console.WriteLine("Choose the poison you want to use now:");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    for (int i = 0, num = 1; i < Poisons.Count; i++, num++)
                    {
                        Console.WriteLine($"[{num}] {Poisons[i].Name} - {Poisons[i].Healing} heal points");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
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

                    int p;
                    for ( p = 0; p < Inventory.Count; p++)
                    {
                        if (Inventory[p].Name.Equals(Poisons[correctPoisonNum].Name)){
                            break;
                        }
                    }
                    Inventory.Remove(Inventory[p]);

                    Poisons.Remove(Poisons[correctPoisonNum]);
                    return 0;
                }
            }

            double damage = 0;

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
            if(Poisons.Count == 0)
            {
                Console.WriteLine("No healing poisons at the moment");
                Console.WriteLine();
            }
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
        public void HealLine(int poisonNum)
        {
            string poison = Poisons[poisonNum].Name;
            string phrase = "";
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (Poisons[poisonNum].Name.Equals(ItemStyles.weak))
            {
                phrase = "healLine";
            }
            else if (Poisons[poisonNum].Name.Equals(ItemStyles.normal))
            {
                phrase = "healLine";
            }
            else if (Poisons[poisonNum].Name.Equals(ItemStyles.strong))
            {
                phrase = "healLine";
            }
            else if (Poisons[poisonNum].Name.Equals(ItemStyles.swordHP))
            {
                phrase = "healLine";
            }
            else if (Poisons[poisonNum].Name.Equals(ItemStyles.eye))
            {
                phrase = "healLine";
            }
            else if (Poisons[poisonNum].Name.Equals(ItemStyles.ball))
            {
                phrase = "healLine";
            }
            Console.WriteLine($"You grab {poison} {phrase}");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;

        }         //add healing lines
        public void WeaponAttackLine(int weaponNum)
        {
            string weapon = Weapons[weaponNum].Name;
            string randomPhrase = "";

            Console.ForegroundColor = ConsoleColor.DarkGreen;
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
            "then trying to stick it in his butt but he instantly jumps away and you just beat his head with it",     //3
            "start screaming out loud and running to that peace of shit but slipping on the ground and your mace" +
            " flying out of your hands and landing right on the head of your opponent...\n" +
            "While he shocked you quickly take your weapon back and getting ready for the next punch",     //4
            "coming close to your opponent, raising your hand and landing your heavy mace on his head" +
            "...then on his back and continue beating the shit out of him"      //5
        };
        public string[] SwordPhrases = new string[5]
        {
            "and start chopping this bitch on peaces with your sword",        //1
            "stick it in his ass and cranking it till the blood will not stop dripping of this bastard",   //2
            "gently touches his stomach with the tip of your strong sword...you feel that you want to " +
            "penetrate him deeply...and do that right now...with your sword...right in his stomach",     //3
            "throw your sword in the sky and start running to him, trying to beat him with hand " +
            "but he moves too fast for you...few seconds later sword falling right on his head " +
            "and getting stuck in it\n" +
            "you come closer, get your bloody sword back and getting ready for the next punch",    //4
            "squeezing it in your hands hard and while he scream \"!!!SUKA BLYAT!!!\" trying to " +
            "stick it in his mouth but he turns his head and you cut his cheeks"     //5
        };
        public string[] BowPhrases = new string[5]
        {
            "bowline",
            "bowline",
            "bowline",
            "bowline",
            "bowline"
        };     //TODO: add bow lines

        public bool ArmsFree { get; set; } = true;
        public bool LegsFree { get; set; } = true;
        public bool BodyFree { get; set; } = true;
        public bool HeadFree { get; set; } = true;

        public bool SwordFree { get; set; } = true;
        public bool MaceFree { get; set; } = true;
        public bool BowFree { get; set; } = true;
    }
}
