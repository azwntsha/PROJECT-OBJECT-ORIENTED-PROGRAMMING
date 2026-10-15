using System;
namespace PrinceAdventureGame
{
    class UpgradeSystem
  {
      public static void OfferHelp(Player player)
      {
          Console.WriteLine("\nAn old merchant appears before you...");
          Console.WriteLine("'Prince... please help me save my family...a group of bandits kidnapped them from me'");
          Console.WriteLine("\nDo you want to help this man?");
          Console.WriteLine("1. Yes, I will help you.  (Note: Your Mana is at stake)");
          Console.WriteLine("2. No.. I have to continue my mission.");
  
          
          string? choice = Console.ReadLine();
  
          if (choice == "1")
          {
              Console.WriteLine("\nYour kindness awakens hidden power!");
  
              //restore stats
              player.health = 150;
              player.mana = 100;
  
              //upgrade attack
              player.attack += 20;
  
              //skill improvement message
              Console.WriteLine("Basic Attack Enhanced!");
              Console.WriteLine("Magic skills cost less mana!");
              Console.WriteLine("Your HP and Mana are fully restored!");
  
              //mana bonus so skills feel cheaper
              player.mana += 30;
          }
          else
          {
              Console.WriteLine("\nYou hesitantly walk away...");
              Console.WriteLine("But your determination sharpens your blade.");
  
              //only basic attack upgrade
              player.attack += 10;
  
              Console.WriteLine("Basic Attack slightly enhanced.");
          }
  
          Console.WriteLine("\nPress any key to continue your journey...");
          Console.ReadKey();
      }
  }
}
