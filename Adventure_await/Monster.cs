using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Adventure_await
{
    public class Monster
    {
        private string _name;
        private int _hp;
        private int _damage;
        private int _goldReward;
        private int _xpReward;

        public Monster(string name, int hp, int damage, int goldReward, int xpReward)
        {
            if (goldReward < 0 || xpReward < 0 || hp < 0 || damage < 0)
            {
                throw new ArgumentException("Values cannot be negative.");
            }

            _name = name;
            _hp = hp;
            _damage = damage;
            _goldReward = goldReward;
            _xpReward = xpReward;
        }

        public void Attack(Player player)
        {
            Console.WriteLine($"The {_name} attacked {player.Name}");
            int dealtDamage = _damage;
            dealtDamage = GetDamageMultiplier(player, dealtDamage);
            player.DamageTaken(dealtDamage);
            Console.WriteLine($"{player.Name} took {dealtDamage} damage!");
        }

        protected int GetDamageMultiplier(Player player, int dealtDamage)
        {
            if (player.Level > 1)
            {
                int levelMultiplier = (int)Math.Round(player.Level * 0.1f);
                dealtDamage += levelMultiplier;
            }
            return dealtDamage;
        }

        public void DamageTaken(string playerName, int damage)
        {
            if (_hp <= 0)
            {
                return;
            }

            Console.WriteLine($"{playerName} attacked the {_name}!");
            _hp -= damage;
            Console.WriteLine($"The {_name} took {damage} damage!");

            if (_hp <= 0)
            {
                _hp = 0;
            }
        }

        public string Name => _name; // Getter
        public int Hp => _hp; // Getter
        public int Damage => _damage; // Getter

        public int GoldReward
        {
            get => _goldReward; // Getter
            set => _goldReward = value; // Setter
        }

        public int XpReward
        {
            get => _xpReward; // Getter
            set => _xpReward = value; // Setter
        }
    }
}
