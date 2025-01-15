using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Adventure_await
{
    public class Boss : Monster
    {
        public Boss(string name, int hp, int damage, int goldReward, int xpReward) : base(name, hp, damage, goldReward, xpReward)
        {
            if (GetRandomValueFromZeroToOneHundred() > 50)
            {
                MultiplyRewards();
            }
        }

        public void SpecialAttack(Player player)
        {
            int value = GetRandomValueFromZeroToOneHundred();
            int dealtDamage = this.Damage;
            dealtDamage = GetDamageMultiplier(player, dealtDamage);

            if (value < 31)
            {
                Console.WriteLine($"{Name} used special attack!");
                dealtDamage = (int)(dealtDamage * 1.5);
            }
            else
            {
                Console.WriteLine($"{Name} did not use special attack!");
            }

            player.DamageTaken(dealtDamage);
            Console.WriteLine($"{player.Name} took {dealtDamage} damage!");
        }

        public void SpecialDefence(Player player)
        {
            int value = GetRandomValueFromZeroToOneHundred();
            int damage = player.Weapon.Damage;

            if (value < 31)
            {
                Console.WriteLine($"{Name} used special defence!");
                damage = (int)(player.Weapon.Damage * 0.5);
            }
            else
            {
                Console.WriteLine($"{Name} did not use special defence!");
            }

            this.DamageTaken(player.Name, damage);
        }

        private void MultiplyRewards()
        {
            int xpReward = (int)(this.XpReward * 1.5);
            int goldReward = (int)(this.GoldReward * 1.5);
            this.XpReward = xpReward;
            this.GoldReward = goldReward;
        }

        private int GetRandomValueFromZeroToOneHundred()
        {
            Random random = new Random();
            return random.Next(0, 101);
        }
    }
}
