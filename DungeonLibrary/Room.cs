using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonLibrary
{
    public class Room
    {
        public string Layout { get; set; }
        //public Character[] Entities { get; set; }
        //public ArrayList Entities { get; set; }
        public List<Character> Entities { get; set; }
        public int KillsToUnlock { get; set; }
        public bool Locked { get; set; }
        public int Kills { get; set; }

        public Room(string layout, List<Character> entities, int killsToUnlock)
        {
            Layout = layout;
            Entities = entities;
            KillsToUnlock = killsToUnlock;
            Locked = true;
            Kills = 0;
            Random rand = new Random();
            for(int i = 0; i < entities.Count; i++)
            {
                while (Layout[entities[i].Y * 16 + entities[i].X] != ' ')
                {
                    entities[i].X = rand.Next(1, 15);
                    entities[i].Y = rand.Next(5);
                }
                for (int a = 0; a < i; a++)
                {
                    while (Layout[entities[i].Y * 16 + entities[i].X] != ' ' || (entities[i].X == entities[a].X && entities[i].Y == entities[a].Y))
                    {
                        entities[i].X = rand.Next(15);
                        entities[i].Y = rand.Next(5);
                    }
                }
            }
        }
    }
}
