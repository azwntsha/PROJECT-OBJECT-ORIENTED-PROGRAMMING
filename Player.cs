using System;
namespace PrinceAdventureGame
{
      public class Player : Character, IAttackable
     {
        //Private fields
        private int Mana;
        private bool shieldActive = false;
    
    
        //Public Properties
        public int mana
        {
            get { return Mana; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Mana Status is invalid");
                } //Attack can't go negative
                Mana = value;
            }
        }
        //constructor
        public Player(string _name, int _health, int _attack, int _mana)
            : base(_name, _health, _attack)
        {
            Mana = _mana;
        }
    
        
        public void DefaultAttack(Character enemy)
        {
            Console.WriteLine($"You strike the enemy for {attack} damage!");
            GiveDamage(enemy, attack);
    
    
        }
    
        public void GiveDamage(Character enemy, int damage)
        {
            enemy.health -= damage;
            if (!enemy.IsAlive())
            {
                Console.WriteLine($"\n{enemy.name} has been defeated!");
            }
        }
    
        public void MagicBlade(Enemy enemy)
        {
            if (mana >= 20)
            {
                int damage = attack + 40;
                Console.WriteLine($"MAGIC BLADE! You deal {damage} damage!");
                GiveDamage(enemy, damage);
                mana -= 20;
            }
            else
                Console.WriteLine("Not enough mana!");
        }
    
        public void MagicArrow(Enemy enemy)
        {
            if (mana >= 15)
            {
                int damage = attack + 30;
                Console.WriteLine($"MAGIC ARROW! You deal {damage} damage!");
                GiveDamage(enemy, damage);
                mana -= 15;
            }
            else
                Console.WriteLine("Not enough mana!");
        }
    
        public void EnhancePotion()
        {
            if (mana >= 10)
            {
                Console.WriteLine("Attack enhanced! +20 attack this round!");
                attack += 20;
                mana -= 10;
            }
            else
                Console.WriteLine("Not enough mana!");
        }
    
        public void DivineHeal()
        {
            if (mana >= 25)
            {
                Console.WriteLine("DIVINE HEAL ACTIVATED!");
                Console.WriteLine("You recover 40 HP and gain full immunity this round!");
                health += 40;
                shieldActive = true;
                mana -= 25;
            }
            else
                Console.WriteLine("Not enough mana!");
        }
    
        public bool IsShieldActive()
        {
            return shieldActive;
        }
    
        public void ResetShield()
        {
            shieldActive = false;
        }
    
        public void RegenerateMana()
        {
            mana+= 5;
            if(mana > 100) mana = 100;
        }
    
         
        public override void DisplayStatus()
        {
            Console.WriteLine($"Your HP: {health}");
            Console.WriteLine($"Your Mana: {mana}");
        }
  
      }
}
