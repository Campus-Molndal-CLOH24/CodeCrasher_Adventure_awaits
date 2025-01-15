using Adventure_await.monster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Adventure_await.character;
using Adventure_await.Utils;

namespace Adventure_await.game
{
    public class GameEngine
    {
        private int startHP  = 100;
        private int levelXp  = 50;
        private int weaponDamage  = 10;
        private bool _isGameStarted  = false;

        //methods
        public void setDifficulty(int startHP, int levelXp, int weaponDamage)
        {
            if (_isGameStarted)
            {
                Console.WriteLine("Cannot change difficulty while game is running!");
            }
            else
            {
                this.startHP = startHP;
                this.levelXp = levelXp;
                this.weaponDamage = weaponDamage;
            }
        }

        public void startGame()
        {
            _isGameStarted = true;
        }

        public void endGame()
        {
            _isGameStarted = false;
        }
        public void gameLoop(Player player)
        {
            startGame();
            while (_isGameStarted)
            {
                Console.WriteLine("[1] - Go to Forrest");
                Console.WriteLine("[2] - Go to Cave");
                Console.WriteLine("[3] - Go to Castle");
                Console.WriteLine("[4] - Go to burning Hell");
                Console.WriteLine("[0] - Return to main menu");
                int choice = InputHandler.GetInt(0, 4);
                switch (choice)
                {
                    case 1:
                        forrest(player);
                        break;
                    case 2:
                        cave(player);
                        break;
                    case 3:
                        castle(player);
                        break;
                    case 4:
                        hell(player);
                        break;
                    case 0:
                        endGame();
                        Console.WriteLine("Exiting menu");
                        break;
                    default:
                        Console.WriteLine("Wrong input!");
                        break;
                }
                if (player.getLevel() >= 10)
                {
                    endGame();
                    Console.WriteLine("You have reached level 10! You win!");
                }
                if (player.getCurrentHp() <= 0)
                {
                    endGame();
                    Console.WriteLine("You have died! You lose!");
                }
            }
        }
        public int getStartHP()
        {
            return startHP;
        }

        public int getLevelXp()
        {
            return levelXp;
        }

        public int getWeaponDamage()
        {
            return weaponDamage;
        }

        public bool isGameStarted()
        {
            return _isGameStarted;
        }
        // TODO: Behövs player?
        public void increaseLevelXp(Player player)
        {
            this.levelXp = this.levelXp + 10;
        }

        public void forrest(Player player)
        {
            Console.WriteLine("You are in a forrest. It's dark and scary. You hear a sound from the bushes.");
            startAdventure(player);
        }

        public void cave(Player player)
        {
            Console.WriteLine("You enter a cave. You hear a sound from the darkness.");
            startAdventure(player);
        }

        public void castle(Player player)
        {
            Console.WriteLine("You are in front of a castle. You hear a sound from the other side of the gate.");
            startAdventure(player);
        }

        public void hell(Player player)
        {
            Console.WriteLine("You are in hell. It's burning everywhere and smells like death.  You hear a sound from the darkness.");
            startAdventure(player);
        }

        private void startAdventure(Player player)
        {
            if (meetMonster())
            {
                fight(player);
            }
            else
            {
                findTreasureWithoutMonster(player);
            }
        }

        private bool meetMonster()
        {
            /* 90% chance of meeting a monster */
            Random rnd = new Random();
            int number = rnd.Next(101);
            return number > 10;
        }

        private void fight(Player player)
        {
            // 10% chance of meeting a Boss
            if (meetBoss())
            {
                fightBoss(player);
            }
            else
            {
                fightMonster(player);
            }
        }
        private bool meetBoss()
        {
            /* 10% chance of meeting a Boss */
            return !meetMonster();
        }
        private void fightBoss(Player player)
        {
            Console.WriteLine("A boss appears!");
            Boss boss = createBoss();
            while (player.getCurrentHp() > 0 && boss.Hp > 0)
            
            Console.WriteLine(
                     "Boss: " + boss.Name + " HP: " + boss.Hp + " vs "
                    + player.getName() + " HP: " + player.getCurrentHp()
            );
       
            int round = 1;
            while (player.getCurrentHp() > 0 && boss.Hp > 0)
            {
                Console.WriteLine("Round " + round++);
                player.attack(boss);
                int monsterHealth = boss.Hp;
                if (monsterHealth <= 0)
                {
                    monsterHealth = 0;
                }
                Console.WriteLine(
                        "Boss: " + boss.Name + " HP: " + boss.Hp + " vs "
                                + player.getName() + " HP: " + player.getCurrentHp()
                );

                if (monsterHealth > 0)
                {
                    boss.SpecialAttack(player);
                }
                // delay 1000ms
                try
                {
                    Thread.Sleep(1000);
                }
                catch (ThreadInterruptedException e)
                {
                    // Hantera eventuellt avbrott här
                    Console.WriteLine("Thread was interrupted: " + e.Message);
                }
                // Loop until either player or monster is dead
            }
            if (player.getCurrentHp() > 0)
            {
                victoryEarnings(player, boss.XpReward, boss.GoldReward);
            }
            else
            {
                Console.WriteLine("You lost the fight and died!");
            }
        }

        private void fightMonster(Player player)
        {
            Console.WriteLine("A monster appears!");
            Monster monster = createMonster();
            Console.WriteLine(
                    "Monster: " + monster.Name + " HP: " + monster.Hp + " vs "
                            + player.getName() + " HP: " + player.getCurrentHp()
            );
            int round = 1;
            while (player.getCurrentHp() > 0 && monster.Hp > 0)
            {
                Console.WriteLine("Round " + round++);
                player.attack(monster);
                int monsterHealth = monster.Hp;
                if (monsterHealth <= 0)
                {
                    monsterHealth = 0;
                }
                Console.WriteLine(
                        "Monster: " + monster.Name + " HP: " + monster.Hp + " vs "
                                + player.getName() + " HP: " + player.getCurrentHp()
                );

                if (monsterHealth > 0)
                {
                    monster.Attack(player);
                }
                // delay 1000ms
                try
                {
                    Thread.Sleep(1000);
                }
                catch (ThreadInterruptedException e)
                {
                    // Hantera eventuellt avbrott här
                    Console.WriteLine("Thread was interrupted: " + e.Message);
                }
                // Loop until either player or monster is dead
            }

            if (player.getCurrentHp() > 0)
            {
                victoryEarnings(player, monster.XpReward, monster.GoldReward);
            }
            else
            {
                Console.WriteLine("You lost the fight and died!");
            }
        }
        private void victoryEarnings(Player player, int xpReward, int goldReward)
        {
            Console.WriteLine("You won the fight!");
            player.increaseXP(xpReward);
            player.levelUp(this);
            player.addGold(goldReward);
            Console.WriteLine("You have " + player.getCurrentHp() + " HP left and earned " + goldReward + " gold!");
            Console.WriteLine("You have " + player.getXp() + " XP and are at level " + player.getLevel() + "!");
        }
        private void findTreasureWithoutMonster(Player player)
        {
            Console.WriteLine("You found a treasure chest!");
            int goldAmount = getRandomValurFromTwentyToOneHundred();
            Console.WriteLine("You found " + goldAmount + " gold!");
            player.addGold(goldAmount);
        }
        private int getRandomValurFromTwentyToOneHundred()
        {
            return new Random().Next(81) + 20;
        }
        private Monster createMonster()
        {
            // Create 5 monsters and return one of them
            Monster[] monsters = new Monster[5];
            monsters[0] = new Monster("Goblin", 20, 5, 20, 20);
            monsters[1] = new Monster("Orc", 25, 8, 30, 30);
            monsters[2] = new Monster("Troll", 35, 11, 40, 40);
            monsters[3] = new Monster("Baby Giant", 40, 14, 50, 50);
            monsters[4] = new Monster("Baby Dragon", 45, 17, 60, 60);

            int randomIndex = getRandomIndex(monsters);
            return monsters[randomIndex];
        }

        private Boss createBoss()
        {
            // Create 3 Bosses and return one of them
            Boss[] bosses = new Boss[3];
            bosses[0] = new Boss("Giant Dragon", 50, 20, 60, 70);
            bosses[1] = new Boss("Giant Troll", 60, 25, 70, 80);
            bosses[2] = new Boss("Giant Giant", 70, 30, 80, 90);

            int randomIndex = getRandomIndex(bosses);
            return bosses[randomIndex];
        }

        private int getRandomIndex(Monster[] monsters)
        {
            return new Random().Next(monsters.Length);
        }
    }
}
