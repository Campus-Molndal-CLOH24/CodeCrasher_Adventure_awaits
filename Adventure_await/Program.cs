
using System;
using Adventure_await.character;
using Adventure_await.game;
using Adventure_await.menu;
using Adventure_await.Utils;
using System.Numerics;

namespace Adventure_await
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Adventure Awaits");
            GameEngine gameEngine = new GameEngine();

            // Skapa ett vapen
            int weaponDamage = gameEngine.getWeaponDamage();
            Weapon weapon = createNewWeapon(weaponDamage);

            // Skapa en karaktär
            int startHp = gameEngine.getStartHP();
            Player player = createNewPlayer(startHp, weapon);

            // Starta spelet
            Menu menu = new Menu();
            menu.Start(player, gameEngine);
        }

        private static Weapon createNewWeapon(int weaponDamage)
        {
            Console.WriteLine("Give the weapon a suiting name: ");
            String weaponName = InputHandler.getString();

            return new Weapon(weaponName, weaponDamage);
        }

        private static Player createNewPlayer(int startHp, Weapon weapon)
        {
            Console.WriteLine("Give the hero a proper name: ");
            String heroName = InputHandler.getString();

            return new Player(heroName, startHp, weapon);
        }
    }
    }
}
