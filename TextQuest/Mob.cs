using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextQuest
{
    public class HighLVLmobs
    {
        public string Name = names[new Random().Next(0, 5)];
        public double HP = new Random().Next(5, 21);
        public static int Randomaizer(int from, int to)
        {
            Random random = new();
            return random.Next(from, to);
        }

        #region Attacks

        public double Bite()
        {
            Console.WriteLine($"{Name} bites your ear...LV police prepares their handcuffs!");
            double damage = Randomaizer(4, 8);
            return damage;
        }
        public double Pinch()
        {
            Console.WriteLine($"{Name} comes really close and pinches you!");
            Console.WriteLine("You are hella angry now!");
            double damage = Randomaizer(1, 3);
            return damage /= 10;
        }
        public void AssSpank()
        {
            Console.WriteLine($"{Name} instantly appears right behind of you!&?");
            Console.WriteLine($"{Name} spank your ass hard...you are confused...but you kinda like it");
            Console.WriteLine($"You don't wanna show it so you continue the fight and " +
                $"trying to forget that ♥ strong ♥ arms ♥ squeezes ♥ your ♥ buttcheecks ♥");
        }
        public double HardPunch()
        {
            Console.WriteLine($"{Name} start beating the shit out of you and you get a lot of damage!");
            double damage = Randomaizer(7, 12);
            return damage;
        }
        public double SoftPunch()
        {
            Console.WriteLine($"{Name} trying to hit your face but miss and just scrapes you!");
            double damage = Randomaizer(3, 7);
            return damage;
        }
        public double Hit()
        {
            Console.WriteLine($"{Name} hits you!");
            double damage = Randomaizer(5, 8);
            return damage;
        }
        public double Attack()
        {
            double damage = 0;
            int chance = Randomaizer(1, 26);
            if (chance < 5)
            {
                damage = Bite();
            }
            if (chance < 10)
            {
                damage = Pinch();
            }
            if (chance < 12)
            {
                Console.WriteLine("\nLoses respect on the streets\n");
            }
            if (chance < 14)
            {
                damage = HardPunch();
            }
            if (chance < 20)
            {
                damage = SoftPunch();
            }
            if (chance < 25)
            {
                damage = Hit();
            }
            Console.WriteLine($"{damage} damage");
            return damage;
        }

        #endregion

        public void TakesDamage(double damage)
        {
            Console.WriteLine($"\n{Name} receives {Math.Round(damage, 2)} damage");
            HP -= damage;
        }

        public static string[] names = new string[5] { "Skeleton", "Dead man", "Swamp corpse", "Thief", "Midget warrior" };

    }
}
