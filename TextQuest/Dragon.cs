using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TextQuest
{
    public class Dragon
    {
        public double HP { get; set; }
        public double Mana { get; set; }
        public double Armor { get; set; }
        public string Name { get; set; }
        public static double RandomaizerDouble()
        {
            Random random = new();
            return random.NextDouble();
        }
        public static int Randomaizer(int from, int to)
        {
            Random random = new();
            return random.Next(from, to);
        }
        public Dragon(string name, double hp, double mana)
        {
            HP = hp;
            Name = name;
            Mana = mana;
        }

        public void TakesDamage(double damage)
        {
            if (damage > 0)
            {
                Armor = RandomaizerDouble();
                double defence = damage * Math.Round(Armor, 2);
                damage -= defence;
                Console.WriteLine();
                Console.WriteLine($"{Name} skin reflects {Math.Round(defence, 2)} damage but receives {Math.Round(damage, 2)} damage");
                HP -= damage;
            }
        }
        public double Incinerate()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{Name} makes a big inhale and filling his lungs with oxygen for that strongest attack!");
            Console.WriteLine();

            Thread.Sleep(1500);
            Console.WriteLine("He is ready to burn you to the ashes...getting closer to you....");
            Console.WriteLine();

            Thread.Sleep(1500);
            Console.WriteLine("AND.....");
            Console.WriteLine();

            if (Mana > 100)
            {
                Thread.Sleep(1500);
                Console.WriteLine($"{Name} INCINERATE EVERYTHING AND EVERYONE AROUND HIM!");
                Console.ForegroundColor = ConsoleColor.White;

                Mana -= 100;
                double damage = Randomaizer(100, 301);
                return damage;
            }
            else
            {
                Thread.Sleep(1500);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Starts coughing hard and loses all the fuse");
                Console.WriteLine();

                Thread.Sleep(800);
                Console.WriteLine("You are safe for now...but be careful with him");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                return 0;
            }
        }
        public double TailPunch()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            string line = TailPunchPhrases[Randomaizer(0, 5)];
            Console.WriteLine($"{Name} {line}");
            Console.ForegroundColor = ConsoleColor.White;

            Mana += 20;
            double damage = Randomaizer(10, 31);
            return damage;
        }
        public double Bite()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            string line = BitePhrases[Randomaizer(0, 5)];
            Console.WriteLine($"{Name} {line}");
            Console.ForegroundColor = ConsoleColor.White;

            Mana += 20;
            double damage = Randomaizer(30, 61);
            return damage;
        }
        public double Attack()
        {
            double damage = 0;
            int chance = Randomaizer(1, 23);
            if (chance < 10)
            {
                damage = Bite();
            }
            else if (chance < 19)
            {
                damage = TailPunch();
            }
            else if (chance < 23)
            {
                damage = Incinerate();
            }
            Console.WriteLine($"{damage} damage");
            return damage;
        }

        public string[] BitePhrases = new string[5]
        {
            "think that you will be a great dinner and bites you!",            //1
            "wanna taste you a bit, comes closer and gently bites you",        //2
            "moving so fast...quiet surprising from that big lizard. You feel that his jaws closes right on your arm",    //3
            "see that you are shocked with his massiveness and while you are standing with open mouth bites you",         //4
            "bites your ass"       //5
        };
        public string[] TailPunchPhrases = new string[5]
        {
            "instantly turns around and gives you a sharp punch with his tail",     //1
            "slap your face with his tail and leave the red mark on your chick..." +
            "you are realizing that you are laying down next to the wall",          //2
            "wanna stick his tail right between your buttcheecks but miss and just punch you with it",    //3
            "does a double flip, somersault and gives you a slap on the back of your head with his tail",    //4
            "puts on a ballet tutu, begins to dance an excerpt from \"Swan Lake\" and " +
            "during one of the turns elegantly beats you with his tail"       //5
        };
    }
}
