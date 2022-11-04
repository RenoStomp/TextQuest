using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace TextQuest;
public class Program
{
    public static void Main(string[] args)
    {
        //Console.SetWindowSize()
        Introduction();

        Console.CursorVisible = true;
        Console.WriteLine();
        Console.Write("Input the name of your HERO and press ENTER: ");
        string heroName = Console.ReadLine().Trim();
        while (string.IsNullOrWhiteSpace(heroName) || !Regex.IsMatch(heroName, "^[a-zA-Z0-9 ]*$"))
        {
            Console.Write("Impossible name, try again: ");
            heroName = Console.ReadLine().Trim();
        }
        Hero hero = new(heroName, 150);

        Dragon dragon = new("Elden Dragon", 500, 100);

        //LowLVLmobs spider1 = new("Spider", 3);
        //LowLVLmobs spider2 = new("Spider", 3);
        //LowLVLmobs spider3 = new("Spider", 2);
        //LowLVLmobs spider4 = new("Spider", 2);

        //HighLVLmobs mobOne = new();
        //HighLVLmobs mobTwo = new();
        //HighLVLmobs mobThree = new();
        //HighLVLmobs mobFour = new();
        //HighLVLmobs mobFive = new();
        //HighLVLmobs mobSix = new();
        //HighLVLmobs mobSeven = new();
        //HighLVLmobs mobEight = new();
        //HighLVLmobs mobNine = new();

        #region Armor

        Items steelHelmet = new("Steel Helmet", 0.08, ItemStyles.head);
        Items steelBraces = new("Steel Braces", 0.05, ItemStyles.arms);
        Items steelBoots = new("Steel Boots", 0.07, ItemStyles.legs);
        Items steelArmor = new("Steel Armor", 0.12, ItemStyles.body);

        Items obsHelmet = new("Obsidian Helmet", 0.13, ItemStyles.head);
        Items obsBraces = new("Obsidian Braces", 0.1, ItemStyles.arms);
        Items obsBoots = new("Obsidian Boots", 0.12, ItemStyles.legs);
        Items obsArmor = new("Obsidian Armor", 0.2, ItemStyles.body);

        #endregion

        #region Weapons

        Items steelSword = new("Steel Sword", ItemStyles.sword, 20);
        Items steelMace = new("Steel Mace", ItemStyles.mace, 25);
        Items steelBow = new("Steel Bow", ItemStyles.bow, 18);

        Items obsSword = new("Obsidian Sword", ItemStyles.sword, 35);
        Items obsMace = new("Obsidian Mace", ItemStyles.mace, 50);
        Items obsBow = new("Obsidian Bow", ItemStyles.bow, 33);

        #endregion

        #region Heal Poisons

        Items weakHealPoison = new(ItemStyles.weak, 25);
        Items healPoison = new(ItemStyles.normal, 50);
        Items strongHealPoison = new(ItemStyles.strong, 75);
        Items healSword = new(ItemStyles.swordHP, 100);
        Items healBall = new(ItemStyles.ball, 200);
        Items healEye = new(ItemStyles.eye, 150);

        #endregion

        Console.CursorVisible = false;
        Console.WriteLine("Press ENTER to continue");
        Console.ReadKey(true);
        Console.Clear();

        StoryBegins(hero);

        //начиная отсюда идёт просто тест работы...вроде как, ошибок больше неть


        AddItem(steelSword, hero);
        AddItem(steelMace, hero);
        AddItem(steelBow, hero);
        Console.ReadKey(true);
        AddItem(obsSword, hero);
        AddItem(obsBow, hero);
        AddItem(obsMace, hero);


        AddItem(steelHelmet, hero);
        AddItem(steelBraces, hero);
        AddItem(steelBoots, hero);
        AddItem(steelArmor, hero);
        Console.ReadKey(true);
        AddItem(obsHelmet, hero);
        AddItem(obsBraces, hero);


        AddItem(healEye, hero);
        AddItem(healSword, hero);
        AddItem(healBall, hero);



        Console.ReadKey(true);

        //override ToString()

        //Тут должно быть продолжение, мобы и прочая хрень, но оно будет чуть позже...
        //зато теперь на дракона можно идти с оружием и 1000 хп....просто ради теста кнеш
        //ещё полностью интерфейс боя показывает всё, что нужно


        DragonBattle(hero, dragon);

        DeadCheck(hero);

        DragonDeadCheck(dragon);

        Console.ReadKey();

    }
    #region Adding Items
    public static void AddItem(Items item, Hero hero)
    {
        if (IsPoison(item))
        {
            Console.WriteLine($"You have added {item.Name} to your inventory");

            hero.Inventory.Add(item);
            hero.Poisons.Add(item);
        }
        else
        {
            if (IsArmor(item))
            {
                AddArmor(item, hero);
            }
            else
            {
                AddWeapon(item, hero);
            }
        }
    }      //main method to add anything you want
    public static bool IsArmor(Items item)
    {
        if (item.PhysycalDefence > 0)
        {
            return true;
        }
        return false;
    }
    public static bool IsPoison(Items item)
    {
        if (item.PhysycalDefence == 0 && item.Damage == 0)
        {
            return true;
        }
        return false;
    }
    public static void AddArmor(Items item, Hero hero)
    {
        switch(item.Style)
        {
            case ItemStyles.head:
                if (hero.HeadFree)
                {
                    Console.WriteLine($"You have added {item.Name} to your armor set");
                    hero.Inventory.Add(item);

                    hero.Armor.Add(item);

                    ShowArmor(hero);
                    hero.HeadFree = false;
                }
                else
                {
                    CompareItems(hero, item);
                }
                break;
            case ItemStyles.arms:
                if (hero.ArmsFree)
                {
                    Console.WriteLine($"You have added {item.Name} to your armor set");
                    hero.Inventory.Add(item);

                    hero.Armor.Add(item);

                    ShowArmor(hero);
                    hero.ArmsFree = false;
                }
                else
                {
                    CompareItems(hero, item);
                }
                break;
            case ItemStyles.legs:
                if (hero.LegsFree)
                {
                    Console.WriteLine($"You have added {item.Name} to your armor set");
                    hero.Inventory.Add(item);

                    hero.Armor.Add(item);

                    ShowArmor(hero);
                    hero.LegsFree = false;
                }
                else
                {
                    CompareItems(hero, item);
                }
                break;
            case ItemStyles.body:
                if (hero.BodyFree)
                {
                    Console.WriteLine($"You have added {item.Name} to your armor set");
                    hero.Inventory.Add(item);

                    hero.Armor.Add(item);

                    ShowArmor(hero);
                    hero.BodyFree = false;
                }
                else
                {
                    CompareItems(hero, item);
                }
                break;
        }
    }
    public static void AddWeapon(Items item, Hero hero)
    {
        switch (item.Style)
        {
            case ItemStyles.sword:
                if (hero.SwordFree)
                {
                    Console.WriteLine($"You have added {item.Name} to your inventory");
                    hero.Inventory.Add(item);

                    hero.Weapons.Add(item);

                    ShowWeapons(hero);
                    hero.SwordFree = false;
                }
                else
                {
                    CompareItems(hero, item);
                }
                break;
            case ItemStyles.mace:
                if (hero.MaceFree)
                {
                    Console.WriteLine($"You have added {item.Name} to your inventory");
                    hero.Inventory.Add(item);

                    hero.Weapons.Add(item);

                    ShowWeapons(hero);
                    hero.MaceFree = false;
                }
                else
                {
                    CompareItems(hero, item);
                }
                break;
            case ItemStyles.bow:
                if (hero.BowFree)
                {
                    Console.WriteLine($"You have added {item.Name} to your inventory");
                    hero.Inventory.Add(item);

                    hero.Weapons.Add(item);

                    ShowWeapons(hero);
                    hero.BowFree = false;
                }
                else
                {
                    CompareItems(hero, item);
                }
                break;
        }
    }
    public static void CompareItems(Hero hero, Items item)
    {
        string name = "";
        double points = 0;
        int i = 0;
        for (; i < hero.Inventory.Count; i++)
        {
            if (hero.Inventory[i].Style == item.Style)
            {
                name = hero.Inventory[i].Name;
                points = IsArmor(hero.Inventory[i]) ? hero.Inventory[i].PhysycalDefence : hero.Inventory[i].Damage;
                break;
            }
        }

        double comparePoints = IsArmor(item) ? item.PhysycalDefence : item.Damage;
        SwitchLine(name, points, item.Name, comparePoints);

        Console.WriteLine("[Y] - YES     [N] - NO");

        var choice = Console.ReadKey(true);
        while (choice.Key != ConsoleKey.Y && choice.Key != ConsoleKey.N)
        {
            choice = Console.ReadKey(true);
        }

        if (choice.Key == ConsoleKey.Y)
        {
            Console.WriteLine($"You have switched your {hero.Inventory[i].Name} to {item.Name}!");
            hero.Inventory[i] = item;
            //IsArmor(item) ? ShowArmor(hero) : ShowWeapons(hero);

            int j = 0;
            if (IsArmor(item))
            {
                for(; j < hero.Armor.Count; j++)
                {
                    if(hero.Armor[j].Style == item.Style)
                    {
                        hero.Armor[j] = item;
                    }
                }
                ShowArmor(hero);
            }
            else
            {
                for (; j < hero.Weapons.Count; j++)
                {
                    if (hero.Weapons[j].Style == item.Style)
                    {
                        hero.Weapons[j] = item;
                    }
                }
                ShowWeapons(hero);
            }
        }
        else
        {
            Console.WriteLine($"You have decided to stay with your oldie and continue your journey");

            if (IsArmor(item))
            {
                ShowArmor(hero);
            }
            else
            {
                ShowWeapons(hero);
            }
        }
    }
    public static void SwitchLine(string firstName, double firstPoints, string secondName, double secondPoints)
    {
        Console.Write($"Do you want to switch your ");  //do you want to switch you armor - defense to new armor - new defense?
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{firstName}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(" - ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{firstPoints}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(" to ");
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write($"{secondName}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(" - ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{secondPoints}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("?");

    }
    #endregion

    #region Interface

    public static void ShowWeapons(Hero hero)
    {
        Console.WriteLine("\nYour weapons at the moment:");
        for (int i = 0; i < hero.Weapons.Count; i++)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{hero.Weapons[i].Name}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" - ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{hero.Weapons[i].Damage} damage points");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }    //Printing all weapons you have atm
    public static void ShowArmor(Hero hero)
    {
        Console.WriteLine("\nYour armor at the moment:");
        for (int i = 0; i < hero.Armor.Count; i++)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{hero.Armor[i].Name}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" - ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Math.Round(hero.Armor[i].PhysycalDefence * 100)} defense points");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }     //Printing all armor you have atm
    public static void ShowInterface(Hero hero, Dragon dragon)    //Showing whole armor set and poisons in the bottom of console
    {
        if (dragon.HP <= 0)
        {
            dragon.HP = 0;
        }
        if (hero.HP <= 0)
        {
            hero.HP = 0;
        }
        string yourHealth = $"{hero.Name} health: {Math.Round(hero.HP, 2)}  ";
        string dragonHealth = $"{dragon.Name} health: {Math.Round(dragon.HP, 2)}  ";
        
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        for(int i = 0, j = 1; i < hero.Armor.Count ; i++, j++)
        {
            string armorLine = $"{hero.Armor[^j].Name} - {Math.Round(hero.Armor[^j].PhysycalDefence * 100)} points  ";
            Console.SetCursorPosition(Console.WindowWidth - armorLine.Length, Console.WindowHeight - j);
            Console.WriteLine(armorLine);
        }   //showing armor in bottom right corner

        Console.ForegroundColor = ConsoleColor.Green;
        for(int i = 0, j = 1; i < hero.Poisons.Count ; i++, j++)
        {
            string poisonLine = $"{hero.Poisons[^j].Name} - {hero.Poisons[^j].Healing} heal points";
            Console.SetCursorPosition(1, Console.WindowHeight - j);
            Console.WriteLine(poisonLine);
        }   //showing poisons in bottom left corner

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.SetCursorPosition(Console.WindowWidth - yourHealth.Length, 1);
        Console.WriteLine(yourHealth);
        Console.SetCursorPosition(Console.WindowWidth - dragonHealth.Length, 2);
        Console.WriteLine(dragonHealth);
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
    }

    #endregion

    public static void DeadCheck(Hero hero)
    {
        if (hero.HP <= 0)
        {
            DeadLine();
            Console.ReadKey(true);

            Environment.Exit(0);

        }
    }
    public static void DeadLine()
    {
        string deadLine = "YOU ARE DEAD";
        Console.BackgroundColor = ConsoleColor.DarkRed;
        Console.Clear();
        Console.SetCursorPosition(Console.WindowWidth / 2 - deadLine.Length / 2, Console.WindowHeight / 2 - 2);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(deadLine);
    }
    public static void DragonDeadCheck(Dragon dragon)
    {
        if (dragon.HP <= 0)
        {
            string winLine1 = "CONGRATULATIONS";
            string winLine2 = "YOU WON!";
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(Console.WindowWidth / 2 - winLine1.Length / 2, Console.WindowHeight / 2 - 2);
            Console.WriteLine(winLine1);
            Console.SetCursorPosition(Console.WindowWidth / 2 - winLine2.Length / 2, Console.WindowHeight / 2 - 1);
            Console.WriteLine(winLine2);
        }
    }
    public static void PressAnyButton()
    {
        Console.WriteLine("\n Press any key\n");
        Console.ReadKey(true);
        Console.Clear();
    }
    public static void Introduction()    //finish everything before player need name his hero....or leave it like that
    {
        ShowFirstPage();

        Console.WriteLine();
        Thread.Sleep(1000);
        Console.WriteLine(" This game was made just to know my potency in creating ... anything lol");
        Console.WriteLine();
        Thread.Sleep(1000);
        Console.WriteLine(" If you have any advices or complaints, text me on telegram @twentyonepovars");
        Console.WriteLine();
        Thread.Sleep(1000);
        Console.WriteLine(" But for now");
        Console.WriteLine();
        Thread.Sleep(1000);
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(" ENJOY MY FIRST GAME :)");
        Console.ForegroundColor = ConsoleColor.White;
        PressAnyButton();

    }
    public static void ShowFirstPage()
    {
        Console.CursorVisible = false;

        string mainLine = "DRAGON QUEST";
        string secondLine = "by Emil";
        string lastLine = "press any key to continue";

        Console.BackgroundColor = ConsoleColor.White;
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.SetCursorPosition(Console.WindowWidth / 2 - mainLine.Length / 2, Console.WindowHeight / 2 - 2);
        Console.WriteLine(mainLine);

        Thread.Sleep(2000);

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.SetCursorPosition(Console.WindowWidth / 2 - secondLine.Length / 2, Console.WindowHeight / 2);
        Console.WriteLine(secondLine);

        Thread.Sleep(1000);

        Console.ForegroundColor = ConsoleColor.Gray;
        Console.SetCursorPosition(Console.WindowWidth / 2 - lastLine.Length / 2, Console.WindowHeight / 2 + 3);
        Console.WriteLine(lastLine);
        Console.ReadKey(true);
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Clear();
    }
    public static void StoryBegins(Hero hero)
    {
        Console.WriteLine();
        Console.WriteLine($"{hero.Name} out of his house and see that everything around him is burned to the ashes...\n");
        Console.ReadKey(true);
        Console.WriteLine($"It is not a soul in his village .... {hero.Name} falls on his knees and falls into despair\n");
        Console.ReadKey(true);
        Console.WriteLine($"He trying to hold his tears but starts crying anyway\n");
        Console.ReadKey(true);
        Console.WriteLine($"One moment {hero.Name} realize that it is only two ways for him now...\n");
        Console.ReadKey(true);
        Console.WriteLine($"First - end it right here and right now by suicide\n");
        Console.ReadKey(true);
        Console.WriteLine($"And second...\n");
        Console.ReadKey(true);
        Console.WriteLine($"Grab his coward ass and find that bastard who did that to his village!\n");
        Console.ReadKey(true);

        HardChoice(hero);
    }
    public static void HardChoice(Hero hero)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"What will {hero.Name} do now?");
        Console.WriteLine();
        Console.WriteLine("[S] - suicide     [C] - continue");
        Console.WriteLine();

        var choice = Console.ReadKey(true);
        while (choice.Key != ConsoleKey.C && choice.Key != ConsoleKey.S)
        {
            choice = Console.ReadKey(true);
        }

        Console.ForegroundColor = ConsoleColor.White;

        if (choice.Key == ConsoleKey.S)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"{hero.Name} instantly stands up, turns around and goes back to his house...\n");
            Console.ReadKey(true);
            Console.WriteLine($"He trying to find something that he can use for his plan\n");
            Console.ReadKey(true);
            Console.WriteLine($"He finds a knife with a slightly dull blade\n");
            Console.ReadKey(true);
            Console.WriteLine($"That is the only thing left to do for {hero.Name}\n");
            Console.ReadKey(true);
            Console.WriteLine($"He knees down, brings the knife to his heart and ... freezes\n");
            Console.ReadKey(true);

            Console.Write("...");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("&%$#&!&%@@#$@#%!$@&!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("...\n");
            Console.ReadKey(true);

            Console.WriteLine($"Something loud happened outside!!!!\n");
            Console.ReadKey(true);
            Console.WriteLine($"{hero.Name} jumps up in fright!\n");
            Console.ReadKey(true);
            Console.WriteLine($"But something is under his foot and he stumbles...\n");
            Console.ReadKey(true);
            Console.WriteLine($"His head falls with his temple right on the corner of a chair standing next to him...\n");
            Console.ReadKey(true);
            Console.WriteLine($"{hero.Name} takes his last breath and leaves this cruel world...\n");
            Console.ReadKey(true);

            DeadLine();
            Thread.Sleep(2000);
            Console.ReadKey(true);

            Environment.Exit(0);
        }
    }
    public static void DragonBattle(Hero hero, Dragon dragon)
    {
        while (hero.HP > 0 && dragon.HP > 0)
        {
            Console.Clear();

            ShowInterface(hero, dragon);

            double heroDamage = hero.Attack(dragon.Name);
            dragon.TakesDamage(heroDamage);

            PressAnyButton();

            if (dragon.HP <= 0)
            {
                break;
            }

            ShowInterface(hero, dragon);

            double dragonDamage = dragon.Attack();
            hero.TakesDamage(dragonDamage);

            PressAnyButton();

            if (hero.HP <= 0)
            {
                break;
            }

            ShowInterface(hero, dragon);

        }
    }
}