using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    public class Monster : Character
    {
        public string Art { get; set; }
        public string Description { get; set; }
        public int XPReward { get; set; }

        public Monster(string name, int maxHealth, int health, int damage, int defence, int hitChance, int room, int x, int y, string art, string description, int xpReward) : base(name, maxHealth, health, damage, defence, hitChance, room, x, y)
        {
            Art = art;
            Description = description;
            XPReward = xpReward;
        }
        public Monster(string name, string description, string art, int xpReward, int maxHealth, int damage, int defence, int hitChance, int room, int x, int y) : base(name, maxHealth, maxHealth, damage, defence, hitChance, room, x, y)
        {
            Art = art;
            Description = description;
            XPReward = xpReward;
        }
        public Monster(string name, int room) : base(name, (room + 2) * 5, (room + 1) * 2, room, 60 + room * 2, room)
        {
            switch (name)
            {
                case "Bad Guy":
                    Description = "That guy's pretty bad.";
                    Art = "\\_'O'_/";
                    break;
                case "Mean Man":
                    Description = "Woah. This guy's really mean.";
                    Art = "\\_*O*_/";
                    break;
                case "Happy Fella":
                    Description = "Don't let him fool you. He's happy because he wants to kill you.";
                    Art = "\\_^.^_/";
                    break;
            }
            XPReward = (room + 1) * 9;
        }
    }
}
