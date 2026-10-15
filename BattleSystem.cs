using System;
namespace PrinceAdventureGame
{
  class BattleSystem
  {
      public static void StartBattle(Player player, Enemy enemy, List<Item> inventory)
      {
          Console.WriteLine($"\nBATTLE START: {enemy.name} appears!");
  
          while (player.IsAlive() && enemy.IsAlive())
          {
              Console.WriteLine("\n=================================");
              Console.WriteLine($"Your HP: {player.health}");
              Console.WriteLine($"Your Mana: {player.mana}");
              enemy.DisplayStatus();
              Console.WriteLine("=================================");
  
              Console.WriteLine("Action:");
              Console.WriteLine("1. Basic Attack");
              Console.WriteLine("2. MAGIC BLADE (20 Mana)");
              Console.WriteLine("3. MAGIC ARROW (15 Mana)");
              Console.WriteLine("4. Enhance Potion (10 Mana)");
              Console.WriteLine("5. Divine Heal (Heal + Immunity) (25 Mana)");
              Console.WriteLine("6. Open Inventory");
              Console.Write("Choose Action(1-6): ");
  
              //To get user interaction choice during combat 
              string? choice = Console.ReadLine();
  
              switch (choice)
              {
                  case "1":
                      player.DefaultAttack(enemy);
                      break;
                  case "2":
                      player.MagicBlade(enemy);
                      break;
                  case "3":
                      player.MagicArrow(enemy);
                      break;
                  case "4":
                      player.EnhancePotion();
                      break;
                  case "5":
                      player.DivineHeal();
                      break;
                  case "6":
                      InventorySystem.OpenInventory(player, inventory);
                      continue;
                  default:
                      Console.WriteLine("Invalid choice!");
                      continue;
              }
  
              
              if (enemy.IsAlive())
              {
                  enemy.DefaultAttack(player);
              }
  
              player.RegenerateMana(); //+5 mana every round
          }
      }
  }
}
