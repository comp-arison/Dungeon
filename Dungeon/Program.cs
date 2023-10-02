using DungeonLibrary;
namespace DungeonApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your name: ");
            Player player = new Player(Console.ReadLine());
            //Character[] entities = { player , new Monster(0), new Monster(0), new Monster(0) };
            //List<Character> entities = new List<Character> { player, new Monster(0), new Monster(0), new Monster(0) };
            Room[] rooms = {
                new(
                "###############\n" +
                "#             #\n" +
                "#     ###     #\n" +
                "#     ###     $\n" +
                "#     ###     #\n" +
                "#             #\n" +
                "###############\n",
                new List<Character> { player, new Monster("Bad Guy", 0), new Monster("Bad Guy", 0), new Monster("Happy Fella", 0) }, 3),
                new(
                "###############\n" +
                "#   #######   #\n" +
                "#   #######   #\n" +
                "              $\n" +
                "#   #######   #\n" +
                "#   #######   #\n" +
                "###############\n",
                new List<Character> { new Monster("Bad Guy", 1), new Monster("Happy Fella", 1), new Monster("Mean Man", 1), new Monster("Mean Man", 1), new Monster("Bad Guy", 1) }, 3),
                new(
                "###############\n" +
                "#    # #   #  #\n" +
                "# ## # # #   ##\n" +
                "  #    # #### $\n" +
                "# #### #    # #\n" +
                "# #      ##   #\n" +
                "###############\n",
                new List<Character> { new Monster("Bad Guy", 2), new Monster("Bad Guy", 2), new Monster("Mean Man", 2), new Monster("Mean Man", 2), new Monster("Happy Fella", 2), new Monster("Bad Guy", 2), new Monster("Mean Man", 2) }, 5)
            };

            bool quit = false;
            Monster? fight = null;
            bool showInfo = false;
            int[] prevPos = {player.X, player.Y};
            Random rand = new Random();
            string message = $"Welcome to the world, {player.Name}!\n";
            do
            {
                Console.Clear();
                if (fight == null)
                {

                    if (showInfo)
                    {
                        Console.WriteLine($"\n{player.Name}\nScore: {player.Score}\nHP: {player.Health}/{player.MaxHealth}\n{RenderBar(player.Health, player.MaxHealth)}\nLevel: {player.Level}\nXP: {player.XP}/{player.XPNext}\n{RenderBar(player.XP, player.XPNext)}\nAttack Damage: {player.Damage}\nDefence: {player.Defence}\n\nPress any key to return.");
                        ConsoleKeyInfo input = Console.ReadKey(true);
                        showInfo = false;
                    }
                    else
                    {
                        Console.WriteLine(message + "\n" + RenderScreen(rooms[player.Room]));
                        Console.WriteLine("WASD: Move\nI: Info\nQ: Quit Program");
                        ConsoleKeyInfo input = Console.ReadKey(true);
                        switch (input.KeyChar)
                        {
                            case 'q':
                                quit = true;
                                break;
                            case 'i':
                                showInfo = true;
                                break;
                            case 'w':
                                if (rooms[player.Room].Layout[(player.Y - 1) * 16 + player.X] == ' ' || (rooms[player.Room].Layout[(player.Y - 1) * 16 + player.X] == '$' && !rooms[player.Room].Locked))
                                {
                                    prevPos[0] = player.X;
                                    prevPos[1] = player.Y;
                                    player.Y -= 1;
                                }
                                break;
                            case 's':
                                if (rooms[player.Room].Layout[(player.Y + 1) * 16 + player.X] == ' ' || (rooms[player.Room].Layout[(player.Y + 1) * 16 + player.X] == '$' && !rooms[player.Room].Locked))
                                {
                                    prevPos[0] = player.X;
                                    prevPos[1] = player.Y;
                                    player.Y += 1;
                                }
                                break;
                            case 'a':
                                if (rooms[player.Room].Layout[player.Y * 16 + (player.X - 1)] == ' ' || (rooms[player.Room].Layout[player.Y * 16 + (player.X - 1)] == '$' && !rooms[player.Room].Locked))
                                {
                                    prevPos[0] = player.X;
                                    prevPos[1] = player.Y;
                                    player.X -= 1;
                                    if (player.X == 0)
                                    {
                                        rooms[player.Room].Entities.Remove(player);
                                        player.Room -= 1;
                                        player.X = 13;
                                        rooms[player.Room].Entities.Insert(0, player);
                                    }
                                }
                                break;
                            case 'd':
                                if (rooms[player.Room].Layout[player.Y * 16 + (player.X + 1)] == ' ' || (rooms[player.Room].Layout[player.Y * 16 + (player.X + 1)] == '$' && !rooms[player.Room].Locked))
                                {
                                    prevPos[0] = player.X;
                                    prevPos[1] = player.Y;
                                    player.X += 1;
                                    if (player.X == 14)
                                    {
                                        if (player.Room == rooms.Length - 1)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("You won!!!! :D\nScore: " + player.Score);
                                            quit = true;
                                        }
                                        else
                                        {
                                            rooms[player.Room].Entities.Remove(player);
                                            player.Room += 1;
                                            player.X = 1;
                                            rooms[player.Room].Entities.Insert(0, player);
                                        }
                                    }
                                }
                                break;
                        }
                        foreach (Character entity in rooms[player.Room].Entities)
                        {
                            if (entity is Monster && player.X == entity.X && player.Y == entity.Y)
                            {
                                fight = (Monster)entity;
                                message = "Battle!\n";
                            }
                        }
                    }
                }
                else
                {
                    if (showInfo)
                    {
                        Console.WriteLine($"\n{fight.Art}\n\n{fight.Name}\n{fight.Description}\nHP: {fight.Health}/{fight.MaxHealth}\n{RenderBar(fight.Health, fight.MaxHealth)}\nAttack Damage: {fight.Damage}\nDefence: {fight.Defence}\n\nPress any key to return.");
                        ConsoleKeyInfo input = Console.ReadKey(true);
                        showInfo = false;
                    }
                    else
                    {
                        Console.WriteLine($"{message}\n\n{fight.Art}\n\n{fight.Name}\nTheir HP: {fight.Health}/{fight.MaxHealth}\n{RenderBar(fight.Health, fight.MaxHealth)}\n Your HP: {player.Health}/{player.MaxHealth}\n{RenderBar(player.Health, player.MaxHealth)}\n\nF: Fight\nI: Info\nR: Run\nQ: Quit Program");
                        ConsoleKeyInfo input = Console.ReadKey(true);
                        switch (input.KeyChar)
                        {
                            case 'q':
                                quit = true;
                                break;
                            case 'i':
                                showInfo = true;
                                break;
                            case 'r':
                                fight = null;
                                player.X = prevPos[0];
                                player.Y = prevPos[1];
                                break;
                            case 'f':
                                if (rand.Next(100) < player.HitChance)
                                {
                                    fight.Health = Math.Max(0, fight.Health - Math.Max(1, player.Damage - fight.Defence));
                                    message = $"{player.Name} dealt {Math.Max(1, player.Damage - fight.Defence)} damage!\n";
                                }
                                else
                                {
                                    message = $"{player.Name}'s attack missed!\n";
                                }

                                if (fight.Health == 0)
                                {
                                    player.Score += 100;
                                    if (player.AddXP(fight.XPReward))
                                    {
                                        player.Score += 150;
                                        message = $"{player.Name} leveled up to level {player.Level}!\n";
                                    }
                                    else
                                    {
                                        message = $"{player.Name} earned {fight.XPReward} XP!\n";
                                    }
                                    rooms[player.Room].Entities.Remove(fight);
                                    fight = null;
                                    rooms[player.Room].Kills += 1;
                                    if (rooms[player.Room].Kills >= rooms[player.Room].KillsToUnlock)
                                    {
                                        player.Score += 125;
                                        rooms[player.Room].Locked = false;
                                        message += "The gates have lifted!";
                                    }
                                }
                                else
                                {
                                    if (rand.Next(100) < fight.HitChance)
                                    {
                                        player.Health = Math.Max(0, player.Health - Math.Max(1, fight.Damage - player.Defence));
                                        message += $"{fight.Name} dealt {Math.Max(1, fight.Damage - player.Defence)} damage!";
                                        if (player.Health == 0)
                                        {
                                            Console.Clear();
                                            Console.WriteLine("You died. :(\nYour score: " + player.Score);
                                            quit = true;
                                        }
                                    }
                                    else
                                    {
                                        message += $"{fight.Name}'s attack missed!";
                                    }
                                }
                                break;
                        }
                    }
                }
            } while (!quit);
        }

        static string RenderScreen(Room room)
        {
            char[] screen = room.Layout.ToCharArray();
            foreach (Character entity in room.Entities)
            {
                if (entity != null)
                {
                    if (entity is Player)
                    {
                        screen[entity.Y * 16 + entity.X] = '@';
                    }
                    else
                    {
                        screen[entity.Y * 16 + entity.X] = '&';
                    }
                }
            }
            if (!room.Locked)
            {
                return new string(screen).Replace('$', ' ');
            }
            return new string(screen);
        }
        
        static string RenderBar(int filled, int max)
        {
            string bar = "";
            for (int i = 0; i < max; i++)
            {
                if (i < filled)
                {
                    bar += "#";
                } else
                {
                    bar += "-";
                }
            }
            return bar;
        }
    }
}