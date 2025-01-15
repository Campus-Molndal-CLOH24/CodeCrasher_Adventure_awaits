using System;
using System.Numerics;
using Se.Dsve.Character;
using Se.Dsve.Game;
using Se.Dsve.Shop;
using Se.Dsve.Utils;

namespace Se.Dsve.Menu
{
    public class Menu
    {
        private const int MAX_LEVEL = 10;

        public void Start(Player player, GameEngine gameEngine)
        {
            int choice = 0;

            while (choice != 9)
            {
                MainChoices();
                choice = InputHandler.GetInt(0, 10);
                ChoiceHandler(choice, player, gameEngine);

                if (IsDead(player))
                {
                    Console.WriteLine("Do you want to play again? [y/n]");
                    string playAgain = InputHandler.GetString();
                    if (playAgain.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        ResetPlayerStats(player, gameEngine);
                        Console.WriteLine("You're back in the game!");
                        choice = 0;
                    }
                    else
                    {
                        Console.WriteLine("Thanks for playing, sucker!");
                        break;
                    }
                }

                if (player.Level >= MAX_LEVEL)
                {
                    Console.WriteLine("Congratulations! You have reached level 10 and have won the game!");
                    Console.WriteLine("Do you want to play again? [y/n]");
                    string playAgain = InputHandler.GetString();
                    if (playAgain.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        ResetPlayerStats(player, gameEngine);
                        Console.WriteLine("You're back in the game!");
                        choice = 0;
                    }
                    else
                    {
                        Console.WriteLine("Thanks for playing, Champ!");
                        break;
                    }
                }
            }
        }

        private static bool IsDead(Player player)
        {
            return player.CurrentHp <= 0;
        }

        private void MainChoices()
        {
            Console.WriteLine("[1] - Go on adventure");
            Console.WriteLine("[2] - Show info about player");
            Console.WriteLine("[3] - Go to the Shop");
            Console.WriteLine("[8] - Change difficulty");
            Console.WriteLine("[9] - EXIT GAME");
        }

        public void ChoiceHandler(int choice, Player player, GameEngine gameEngine)
        {
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Going on an adventure");
                    gameEngine.GameLoop(player);
                    break;
                case 2:
                    Console.WriteLine("Show player info");
                    ShowPlayerInfo(player);
                    break;
                case 3:
                    Console.WriteLine("Go to the Shop");
                    Shop.Menu(player);
                    break;
                case 8:
                    Console.WriteLine("Change difficulty");
                    SetDifficulty(gameEngine, player);
                    break;
                case 9:
                    Console.WriteLine("EXIT GAME");
                    break;
                default:
                    Console.WriteLine("Wrong input!");
                    break;
            }
        }

        public void ShowPlayerInfo(Player player)
        {
            Console.WriteLine($"Name: {player.Name}");
            Console.WriteLine($"Level: {player.Level}");
            Console.WriteLine($"XP: {player.Xp}");
            Console.WriteLine($"Current/Total HP: {player.CurrentHp}/{player.TotalHp}");
            Console.WriteLine($"Gold: {player.GoldAmount}");
            Console.WriteLine($"Weapon: {player.Weapon.Name}");
            Console.WriteLine($"Weapon Damage: {player.Weapon.Damage}");
        }

        public void SetDifficulty(GameEngine gameEngine, Player player)
        {
            Console.WriteLine("Set difficulty");
            Console.WriteLine("[1] - Easy");
            Console.WriteLine("[2] - Medium");
            Console.WriteLine("[3] - Hard");
            int choice = InputHandler.GetInt(1, 3);

            switch (choice)
            {
                case 1:
                    gameEngine.SetDifficulty(gameEngine.StartHP, gameEngine.LevelXp, gameEngine.WeaponDamage);
                    player.TotalHp = gameEngine.StartHP;
                    player.CurrentHp = gameEngine.StartHP;
                    player.Weapon.Damage = gameEngine.WeaponDamage;
                    break;
                case 2:
                    gameEngine.SetDifficulty(50, 25, 5);
                    player.TotalHp = 50;
                    player.CurrentHp = 50;
                    player.Weapon.Damage = 5;
                    break;
                case 3:
                    gameEngine.SetDifficulty(25, 10, 2);
                    player.TotalHp = 25;
                    player.CurrentHp = 25;
                    player.Weapon.Damage = 2;
                    break;
                default:
                    Console.WriteLine("Wrong input!");
                    break;
            }
        }

        private void ResetPlayerStats(Player player, GameEngine gameEngine)
        {
            player.Level = 1;
            player.Xp = 0;
            player.TotalHp = gameEngine.StartHP;
            player.CurrentHp = gameEngine.StartHP;
            player.GoldAmount = 0;
            string weaponName = player.Weapon.Name;
            player.Weapon = new Weapon(weaponName, gameEngine.WeaponDamage);
        }
    }
}
