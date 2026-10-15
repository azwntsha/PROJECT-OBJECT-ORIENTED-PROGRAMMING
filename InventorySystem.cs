using System;
namespace PrinceAdventureGame
{
      class InventorySystem
  {
      public static void OpenInventory(Player prince, List<Item> inventory)
      {
          Console.WriteLine("\n=================================");
          Console.WriteLine("\t   INVENTORY\t");
          Console.WriteLine("=================================");
  
          int ChooseItem = 0;
          bool alreadyHasWeapon = false;
          do
          {
              //Display all items in inventory and choices of action to be taken
              for (int i = 0; i < inventory.Count; i++)
              {
                  Console.Write($"{i + 1}. ");
                  Console.WriteLine(inventory[i].GiveDescription());
              }
              Console.Write("\nWhat to do next?\n1. Use Item \n2. Unequip Weapon\n3. Remove Item\n4. Close Inventory\nChoose between 1 - 4: ");
              
              try
              {
                  ChooseItem = int.Parse(Console.ReadLine());
              }
              catch
              {
                  Console.WriteLine("Invalid input! Please enter a number.");
                  continue;
              }
              //Selection for User interaction in the inventory 
              if (ChooseItem == 1)
              {
                  Console.Write("Equip:\nEnter Item number to use:");
                  int ChooseWeapon;
                  try
                  {
                      ChooseWeapon = int.Parse(Console.ReadLine());
                  }
                  catch
                  {
                      Console.WriteLine("Invalid input!");
                      continue;
                  }
                  int index = ChooseWeapon - 1;
                  Item select = inventory[index];
                  if (select is WeaponItem w)
                  {
                      alreadyHasWeapon = inventory.Any(i => i is WeaponItem w && w.IsEquipped);
                      if (alreadyHasWeapon)
                      {
                          Console.WriteLine("Unequip current weapon first!\n");
                          continue;
                      }
                      else
                      {
                          //condition if the chosen item is already been equipped
                          if (w.IsEquipped)
                              Console.WriteLine("Already equipped!\n");
                          else
                          {
                              w.UseItem(prince);
                              w.IsEquipped = true;
                              Console.WriteLine($"{w.ItemName} is equipped!\n");
                          }
                      }
                  }
                  else//implementation when w is not a Weapon but General Item
                  {
                      select.UseItem(prince);
                      inventory.Remove(select);
                      Console.WriteLine($"{select.ItemName} has been consumed!\n");
                  }
              }
              else if (ChooseItem == 2)
              {
                  //to find if there any weapon is in used, if found, InUsed object will be assigned with the founded object 
                  Item? InUsed = inventory.Find(i => i is WeaponItem w && w.IsEquipped);
                  if (InUsed == null)
                  {
                      Console.WriteLine("No weapon currently equipped!\n");
                      continue;
                  }
  
                  Console.WriteLine($"\nUnequip:\nEnter OK to unequip {InUsed.ItemName}");
                  string? agree = Console.ReadLine();
                  if (agree == "OK")
                  {
                      if (InUsed is WeaponItem w)
                      {
                          prince.attack -= w.AttackBonus;
                          prince.mana -= w.ManaBonus;
                          w.IsEquipped = false;
                          alreadyHasWeapon = false;
                          Console.WriteLine($"{w.ItemName} unequipped!\n");
                      }
                  }
                  continue;
  
              }
              else if (ChooseItem == 3)
              {
                  Console.Write("\nRemove Item:\nChoose item to be remove: ");
                  int ItemRemove;
                  try
                  {
                      ItemRemove = int.Parse(Console.ReadLine()) - 1;
                  }
                  catch
                  {
                      Console.WriteLine("Invalid input!\n");
                      continue;
                  }
                  Item toRemove = inventory[ItemRemove];
                  inventory.Remove(toRemove);
                  Console.WriteLine($"{toRemove.ItemName} has been discarded!\n");
  
              }
  
          } while (ChooseItem != 4);
  
      }
  }
}
