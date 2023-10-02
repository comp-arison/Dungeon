using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    public class Player : Character
    {
        public int Level { get; set; }
        public int XP { get; set; }
        public int XPNext { get; set; }
        public int Score { get; set; }
        public Player(string name, int maxHealth, int health, int damage, int defence, int hitChance, int room, int x, int y, int level, int xp, int xpNext, int score) : base(name, maxHealth, health, damage, defence, hitChance, room, x, y)
        {
            Level = level;
            XP = xp;
            XPNext = xpNext;
            Score = score;
        }

        public Player(string name) : base(name, 20, 20, 4, 0, 70, 0, 1, 3)
        {
            Level = 1;
            XP = 0;
            XPNext = 25;
            Score = 0;
        }
        public bool AddXP(int newXP)
        {
            XP += newXP;
            while(XP >= XPNext)
            {
                XP -= XPNext;
                Level++;
                XPNext = 25 * Level;
                MaxHealth = 15 + Level * 5;
                Health = MaxHealth;
                Defence = (Level - 1) * 2;
                HitChance = 80 + 2 * Level;
                return true;
            }
            return false;
        }
    }
}
