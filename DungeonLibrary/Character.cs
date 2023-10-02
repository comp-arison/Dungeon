namespace DungeonLibrary
{
    public abstract class Character
    {

        //private string _name;
        //private int _maxHealth, _health, _damage, _Defence, _hitChance;

        public string Name { get; set; }
        public int MaxHealth { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Defence { get; set; }
        public int HitChance { get; set; }
        public int Room { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Character(string name, int maxHealth, int health, int damage, int defence, int hitChance, int room, int x, int y)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = Math.Min(health, maxHealth);
            Damage = damage;
            Defence = defence;
            HitChance = hitChance;
            Room = room;
            X = x;
            Y = y;
        }

        public Character(string name, int maxHealth, int health, int damage, int defence, int hitChance, int room)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = Math.Min(health, maxHealth);
            Damage = damage;
            Defence = defence;
            HitChance = hitChance;
            Room = room;
            X = 0;
            Y = 0;
        }
        public Character(string name, int maxHealth, int damage, int defence, int hitChance, int room)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = maxHealth;
            Damage = damage;
            Defence = defence;
            HitChance = hitChance;
            Room = room;
            X = 0;
            Y = 0;
        }
    }
}