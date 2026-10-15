using System;
namespace PrinceAdventureGame
{
        public class Enemy : Character, IAttackable
      {
          public Enemy(string _name, int _health, int _attack)
              : base(_name, _health, _attack)
          {
      
          }
      
          public void DefaultAttack(Character player)//SINI
          {
              if (player is Player p && p.IsShieldActive())
              {
                  Console.WriteLine("Enemy attack blocked by Divine Shield!");
                  p.ResetShield();
              }
              else if (player is Player p2)
              {
                  Console.WriteLine($"Enemy attacks you for {attack} damage!");
                  GiveDamage(p2, attack);
              }
          }
      
         
          public void GiveDamage(Character player, int damage)
          {
              player.health -= damage;
      
              if (!player.IsAlive())
              {
                  Console.WriteLine("\nYOU HAVE BEEN DEFEATED...");
                  Environment.Exit(0);
              }
          }
      }
}
