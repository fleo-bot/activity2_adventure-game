using System;

namespace activity_2
{
    class Program
    {
        static string playerName = "";
        static int health = 100;
        static int gold = 0;
        static bool hasSword = false;
        static bool hasShield = false;

        static void DisplayStats()
        {
            Console.WriteLine("\n--- PLAYER STATS ---");
            Console.WriteLine("Hero   : " + playerName);
            Console.WriteLine("Health : " + health);
            Console.WriteLine("Gold   : " + gold);
            Console.WriteLine("Sword  : " + (hasSword ? "Yes" : "No"));
            Console.WriteLine("Shield : " + (hasShield ? "Yes" : "No"));
            Console.WriteLine("--------------------");
        }

        static void Battle(string enemyName, int enemyHealth, int damage, int reward)
        {
            Console.WriteLine("\nA wild " + enemyName + " appears!");
            Console.WriteLine(enemyName + " HP: " + enemyHealth);
            Console.WriteLine("Press any key to fight...");
            Console.ReadKey();

            Random rng = new Random();
            int playerDamage = hasSword ? rng.Next(20, 40) : rng.Next(5, 15);
            int shieldBlock = hasShield ? rng.Next(5, 15) : 0;
            int actualDamage = Math.Max(0, damage - shieldBlock);

            Console.WriteLine("\nYou dealt " + playerDamage + " damage to " + enemyName + "!");
            Console.WriteLine(enemyName + " dealt " + actualDamage + " damage to you!");

            if (playerDamage >= enemyHealth)
            {
                health -= actualDamage;
                gold += reward;
                Console.WriteLine("\nYou defeated the " + enemyName + "!");
                Console.WriteLine("You earned " + reward + " gold!");
            }
            else
            {
                health -= actualDamage * 2;
                Console.WriteLine("\nThe " + enemyName + " overpowered you! You barely escaped!");
            }

            health = Math.Max(0, health);
        }

        static void VisitShop()
        {
            Console.Clear();
            Console.WriteLine("--- VILLAGE SHOP ---");
            Console.WriteLine("1. Buy Sword  (30 gold)");
            Console.WriteLine("2. Buy Shield (20 gold)");
            Console.WriteLine("3. Heal       (15 gold)");
            Console.WriteLine("4. Leave Shop");
            Console.WriteLine("--------------------");
            Console.Write("What will you buy? ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    if (gold >= 30)
                    {
                        hasSword = true;
                        gold -= 30;
                        Console.WriteLine("You bought a Sword! Attack power increased!");
                    }
                    else Console.WriteLine("Not enough gold!");
                    break;
                case "2":
                    if (gold >= 20)
                    {
                        hasShield = true;
                        gold -= 20;
                        Console.WriteLine("You bought a Shield! You can now block damage!");
                    }
                    else Console.WriteLine("Not enough gold!");
                    break;
                case "3":
                    if (gold >= 15)
                    {
                        health = Math.Min(100, health + 30);
                        gold -= 15;
                        Console.WriteLine("You healed 30 HP!");
                    }
                    else Console.WriteLine("Not enough gold!");
                    break;
                case "4":
                    Console.WriteLine("You leave the shop.");
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }

        static void ExploreForest()
        {
            Console.Clear();
            Console.WriteLine("You enter the Dark Forest...");
            Console.WriteLine("The trees are tall and eerie.");
            Battle("Goblin", 30, 20, 25);
        }

        static void ExploreCave()
        {
            Console.Clear();
            Console.WriteLine("You enter the Mysterious Cave...");
            Console.WriteLine("You hear growling echoing inside.");
            Battle("Cave Troll", 60, 35, 50);
        }

        static void ExploreCastle()
        {
            Console.Clear();
            Console.WriteLine("You approach the Dark Castle...");
            Console.WriteLine("Lightning strikes as you enter!");
            Battle("Dragon", 100, 50, 100);
        }

        static bool IsGameOver()
        {
            if (health <= 0)
            {
                Console.Clear();
                Console.WriteLine("--- GAME OVER ---");
                Console.WriteLine(playerName + " has fallen...");
                Console.WriteLine("Final Gold: " + gold);
                Console.WriteLine("-----------------");
                return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("=== LEGEND OF THE CONSOLE KNIGHT ===");
            Console.WriteLine("Welcome, adventurer!");
            Console.Write("Enter your knight's name: ");
            playerName = Console.ReadLine();

            Console.WriteLine("\nWelcome, " + playerName + "!");
            Console.WriteLine("Your quest: Defeat the Dragon and save the kingdom!");
            Console.WriteLine("Press any key to begin...");
            Console.ReadKey();

            bool playing = true;
            while (playing && !IsGameOver())
            {
                Console.Clear();
                DisplayStats();

                Console.WriteLine("\nWHERE WILL YOU GO?");
                Console.WriteLine("1. Dark Forest");
                Console.WriteLine("2. Mysterious Cave");
                Console.WriteLine("3. Dark Castle");
                Console.WriteLine("4. Village Shop");
                Console.WriteLine("5. Quit Game");
                Console.Write("\nChoose your destination: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ExploreForest();
                        break;
                    case "2":
                        ExploreCave();
                        break;
                    case "3":
                        ExploreCastle();
                        if (gold >= 100)
                        {
                            Console.WriteLine("\nYOU DEFEATED THE DRAGON!");
                            Console.WriteLine("THE KINGDOM IS SAVED!");
                            Console.WriteLine("Final Gold: " + gold);
                            playing = false;
                        }
                        break;
                    case "4":
                        VisitShop();
                        break;
                    case "5":
                        playing = false;
                        Console.WriteLine("Thanks for playing! Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Try again.");
                        break;
                }

                if (playing && input != "5")
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }
    }
}