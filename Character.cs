
using System;

namespace PrinceAdventureGame
{
    public class Character
    {
        private string Name;
        private int Health;
        private int Attack;

        public string name
        {
            get { return Name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Name cannot be empty.");
                Name = value;
            }
        }

        public int health { get; set; }
        public int attack
        {
            get { return Attack; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Attack Status is invalid");
                Attack = value;
            }
        }

        public Character(string _name, int _health, int _attack)
        {
            Name = _name;
            Health = _health;
            Attack = _attack;
        }

        public bool IsAlive() => health > 0;

        public virtual void DisplayStatus()
        {
            Console.WriteLine($"{name} HP: {health}");
        }
    }
}
