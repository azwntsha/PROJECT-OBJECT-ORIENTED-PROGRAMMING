
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;


class Program
{
    static void Main(string[] args)
    {
        //Display Game Introduction
        Story.ShowIntro();

        //Initialization of objects:

        //1. Player object (Prince)
        Player prince = new Player("Hidden Prince", 150, 30, 100);

        //2. Item List (1 Inventory with default weapon)
        List<Item> inventory = new List<Item>()
        {
            new WeaponItem("Normal Sword", 0, 0)
        };

        //3. Item List contains battle rewards (loot) for each level completed
        List<Item> Loot1 = new List<Item>()
        {
            new WeaponItem("Cursed Bone Sword", 25, 10),
            new WeaponItem("Undead Shield Fragment", 15, 5),
            new GeneralItem("Undead Essence Potion", 15, 15),
            new GeneralItem("Rotten Flesh Bandage", 30, 0),
            new GeneralItem("Soul Shard", 15, 10)
        };

        List<Item> Loot2 = new List<Item>()
        {
            new WeaponItem("Dragon Fang Blade", 40, 15),
            new WeaponItem("Inferno Gauntlet", 30, 10),
            new GeneralItem("Fire Resistance Potion", 50, 0),
            new GeneralItem("Dragon Scale Shard", 25, 20),
            new GeneralItem("Molten Core Extract", 0, 50)
        };

        List<Item> Loot3 = new List<Item>()
        {
            new WeaponItem("Alucard's Shadow Blade", 50, 20),
            new WeaponItem("Crimson Dark Dagger", 35, 15),
            new GeneralItem("Shadow Mana Potion", 0, 50),
            new GeneralItem("Blood Veil", 30, 10),
            new GeneralItem("Dark Absorption Elixir", 45, 0)
        };

        //4. Enemy List contains each enemy on every level
        List<Enemy> AllEnemies = new List<Enemy>()
        {
            new Enemy("Undead General", 120, 20),
            new Enemy("Fire Dragon", 160, 25),
            new Enemy("Alucard", 200, 30),
            new Enemy("Lord Maldrath", 300, 40)
        };

        //Main Game loop  — iterates through 4 encounters (3 levels + final boss)
        for (int i = 0; i < 4; i++)
        {
            //Battle Initialization for current (i) enemy
            BattleSystem.StartBattle(prince, AllEnemies[i], inventory);

            //Specifically on Lvl 3, special quest offered to upgrade player before meet final boss
            if (i == 2) UpgradeSystem.OfferHelp(prince);

            //Loot Distribution System: Loot will appeared after a level completed except for Lvl 4
            if (i < 3)
            {
                int j = 1;
                bool donePickingLoot = false;

                List<Item> currentLoot = i == 0 ? Loot1 : i == 1 ? Loot2 : Loot3;
                Console.WriteLine("\n╔════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                                 BATTLE REWARDS                                 ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");

                foreach (Item item in currentLoot)
                {
                    Console.WriteLine($"{j}. {item.GiveDescription()}");
                    j++;
                }

                //Loot selection loop — continues until player stops or loot runs out
                while (!donePickingLoot)
                {
                    int weaponCount = inventory.Count(x => x is WeaponItem);
                    int generalCount = inventory.Count(x => x is GeneralItem);
                    Console.WriteLine("Available Slot:");
                    Console.WriteLine($"Weapon slots: {weaponCount}/3");
                    Console.WriteLine($"General slots: {generalCount}/5");
                    Console.Write("Choose item to add (0 to stop): ");

                    int pickIndex;
                    try
                    {
                        pickIndex = int.Parse(Console.ReadLine()) - 1;
                    }
                    catch
                    {
                        Console.WriteLine("Invalid input! Please enter a number.");
                        continue;
                    }

                    //Condition to skip picking Loot
                    if (pickIndex == -1)
                    {
                        donePickingLoot = true;
                        break;
                    }
                    
                    //Reject out-of-range choice
                    if (pickIndex >= currentLoot.Count || pickIndex < 0)
                    {
                        Console.WriteLine("Invalid choice!");
                        continue;
                    }
                    Item chosen = currentLoot[pickIndex];

                    //Filter selected item according to it category (weapon/general) 
                    //Note: Each category has fixed slot. Weapon: 3 slots General: 5 slots

                    
                    if (chosen is WeaponItem && weaponCount >= 3)
                    {
                        Console.WriteLine("Weapon slots full!\n");
                        Console.Write("1. Skip\n2. Open Inventory\nChoose 1 or 2: ");
                        int option = int.Parse(Console.ReadLine());
                        if (option == 1) continue;
                        if (option == 2) InventorySystem.OpenInventory(prince, inventory);
                    }
                    else if (chosen is GeneralItem && generalCount >= 5)
                    {
                        Console.WriteLine("General slots full!\n");
                        Console.WriteLine("Choose:\n1. Skip\n2. Open Inventory");
                        int option1 = int.Parse(Console.ReadLine());
                        if (option1 == 1) continue;
                        if (option1 == 2) InventorySystem.OpenInventory(prince, inventory);

                    }
                    else
                    {
                        inventory.Add(chosen);
                        currentLoot.Remove(chosen); //remove from loot so cant pick same item twice
                        Console.WriteLine($"{chosen.ItemName} added to inventory!");

                    }

                    if (currentLoot.Count == 0)
                    {
                        Console.WriteLine("No more loot!");
                        break;
                    }

                    //Re-display available item after oick item
                    Console.WriteLine("\nRemaining item: ");
                    int k = 1;
                    foreach (Item item in currentLoot)
                    {
                        Console.WriteLine($"{k}. {item.GiveDescription()}");
                        k++;
                    }
                }
            }

            //After Loot picking, game continue with next level intro (i+1)
            Story.AfterLevel(i);

        }

    }
}