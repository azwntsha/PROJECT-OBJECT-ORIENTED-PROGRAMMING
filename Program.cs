using System;

namespace PrinceAdventureGame
{
    // Class 1: character (Task 2)
    class Character
    {
        // Private fields
        private string _name;
        private int _health;
        private int _attack;

        // Public properties
        public string Name { get { return _name; } }
        public int Health
        {
            get { return _health; }
            set
            {
                if (value < 0) { _health = 0; }
                else { _health = value; }
            }
        }
        public int Attack { get { return _attack; } }

        // Constructor
        public Character(string name, int health, int attack)
        {
            _name = name;
            _health = health;
            _attack = attack;
        }

        public void DisplayStatus()
        {
            Console.WriteLine(_name + " HP: " + _health);
        }


        public bool IsAlive()
        {
            return Health > 0;
        }



    }

    // Class 2: player (Task 2)
    class Player : Character
    {
        private int _mana;

        public int Mana { get { return _mana; } }

        // Constructor
        public Player(string name, int health, int attack, int mana)
            : base(name, health, attack)
        {
            _mana = mana;
        }

        // Method
        public void BasicAttack(Enemy enemy)
        {
            int damage = Attack + 5;
            Console.WriteLine(Name + " uses Basic Attack on " + enemy.Name);
            Console.WriteLine($"You strike the enemy for {Attack} damage!");
            enemy.Health = enemy.Health - damage;
        }

        public void MagicBlade(Enemy enemy)
        {
            int damage = Attack + 40;
            Console.WriteLine(Name + " uses Magic Blade on " + enemy.Name);
            Console.WriteLine($"You strike the enemy for {Attack} damage!");
            enemy.Health = enemy.Health - damage;
        }

    }

    // Class 3: enemy (Task 2)
    class Enemy : Character
    {
        // Constructor
        public Enemy(string name, int health, int attack)
            : base(name, health, attack) { }

        // Method
        public void AttackPlayer(Player player)
        {
            Console.WriteLine(Name + " attacks " + player.Name);
            player.Health = player.Health - Attack;
        }

        public void ReflectDamage(Player player)
        {
            Console.WriteLine(Name + " reflect 5 damage on " + player.Name);
            player.Health -= 5;
        }

    }

    // (Task 3)
    class Program
    {
        // Method
        static void Main(string[] args)
        {
            // Constructor for Object Creation
            Player prince = new Player("Hidden Prince", 150, 30, 100);
            Enemy boss = new Enemy("Lord Varkath", 300, 40);

            Console.WriteLine("Assignment 2 Demonstration");

            Console.WriteLine("Before attack (interaction between classes): ");
            //Method calling
            prince.DisplayStatus();
            boss.DisplayStatus();

            Console.WriteLine("\nBattle started");
            prince.BasicAttack(boss);
            prince.MagicBlade(boss);
            boss.AttackPlayer(prince);
            boss.ReflectDamage(prince);

            // Public access to show result
            Console.WriteLine("\nResults:");
            prince.DisplayStatus();
            boss.DisplayStatus();

            Console.ReadKey();
        }
    }
}
