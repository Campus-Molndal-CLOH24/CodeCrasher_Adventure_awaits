using System; // För att använda konsolutskrifter och andra systemfunktioner
using Adventure_await.Character; // Antag att Player-klassen finns i det här namnområdet
using Adventure_await.Utils; // Antag att InputHandler-klassen finns här

namespace Adventure_await.shop
{
    public class Shop
    {
        // Konstanta variabler som representerar kostnader i butiken
        private const int WeaponUpgradeCost = 75;  // Kostnad för att uppgradera vapen
        private const int HpRestoreCost = 100;     // Kostnad för att återställa hälsa

        // Privat metod som visar butiksmenyn för spelaren
        private static void Ui(Player player)
        {
            Console.WriteLine("The Shopper"); // Skriver ut butikens namn
            Console.WriteLine($"{player.Name} have: {player.GoldAmount} gold."); // Visar spelarens namn och guldbelopp
            Console.WriteLine();
            Console.WriteLine($"[1] - Weapon +10% Damage | -{WeaponUpgradeCost} gold");
            Console.WriteLine($"[2] - Restore 100% Health | -{HpRestoreCost} gold");
            Console.WriteLine();
            Console.WriteLine("[0] - Return to main menu"); // Alternativ för att återgå till huvudmenyn
        }

        // Publik metod som hanterar butiksmenyn och spelarens val
        public static void Menu(Player player)
        {
            int choice = -1; // Variabel för spelarens val, initialt satt till -1
            while (choice != 0) // Loopen fortsätter tills spelaren väljer att återgå till huvudmenyn (val 0)
            {
                Ui(player); // Anropa metoden som visar butiksmenyn
                choice = InputHandler.GetInt(0, 2); // Få spelarens val med InputHandler
                switch (choice)
                {
                    case 1:
                        UpgradeWeapon(player); // Uppgradera vapnet om spelaren väljer 1
                        break;
                    case 2:
                        RestoreHp(player); // Återställ hälsa om spelaren väljer 2
                        break;
                    case 0:
                        Console.WriteLine("Returning to main menu"); // Meddelande om återgång till huvudmenyn
                        break;
                    default:
                        Console.WriteLine("Wrong input!"); // Felmeddelande om spelaren skriver något ogiltigt
                        break;
                }
            }
        }

        // Privat metod för att uppgradera spelarens vapen
        private static void UpgradeWeapon(Player player)
        {
            if (player.GoldAmount < WeaponUpgradeCost) // Kontrollera om spelaren har tillräckligt med guld
            {
                Console.WriteLine("Gold amount too low!"); // Meddelande om otillräckligt guld
                return; // Avsluta metoden om spelaren inte har tillräckligt med guld
            }
            player.UpgradeWeaponInShop(WeaponUpgradeCost); // Anropa metod på Player-objektet för att uppgradera vapnet
            Console.WriteLine("Weapon upgraded!"); // Meddelande om att vapnet har uppgraderats
        }

        // Privat metod för att återställa spelarens hälsa
        private static void RestoreHp(Player player)
        {
            if (player.GoldAmount < HpRestoreCost) // Kontrollera om spelaren har tillräckligt med guld
            {
                Console.WriteLine("Gold amount too low!"); // Meddelande om otillräckligt guld
                return; // Avsluta metoden om spelaren inte har tillräckligt med guld
            }
            player.RestoreHpInShop(HpRestoreCost); // Anropa metod på Player-objektet för att återställa hälsa
            Console.WriteLine("Health restored!"); // Meddelande om att hälsan har återställts
        }
    }
}
