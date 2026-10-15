
using PrinceAdventureGame;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using static PrinceAdventureGame.Program;

namespace PrinceAdventureGame
{
    //Main Program Class
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

        // =========================================================================
        // BASE CLASS: Character
        // Serves as the parent class for Player and Enemy.
        // Demonstrates encapsulation through private fields with public properties and constructor
        // Encourage polymorphism via the virtual DisplayStatus() method.
        // =========================================================================


        public class Character
        {
            //Private fields
            private string Name;
            private int Health;
            private int Attack;

            //Public properties
            public string name
            {
                get{ return Name; }
                set{
                    if (string.IsNullOrEmpty(value))
                    {
                        throw new ArgumentException("Name cannot be empty.");
                    }
                    Name = value; }
            }

            public int health
            {
                get{ return Health; }
                set{Health = value; }
            }

            public int attack
            {
                get{ return Attack; }
                set
                {
                    if (value < 0)
                    {
                        throw new ArgumentException("Attack Status is invalid");
                    } //Attack can't go negative
                    Attack = value;
                }
                
            }

            //constructor
            public Character(string _name, int _health, int _attack)
            {
                Name = _name;
                Health = _health;
                Attack = _attack;
            }

            public bool IsAlive()
            {
                return health > 0;
            }

            
            public virtual void DisplayStatus()
            {
                Console.WriteLine($"{name} HP: {health}");
            }


        }

        // =========================================================================
        // INTERFACE: IAttackable (Abstraction)
        // Defines a contract to ensure any class that can attack will also be able to inflict damage
        // Player and Enemy classes are forced to implement it accordingly
        // =========================================================================

        public interface IAttackable //interface
        {
            public void DefaultAttack(Character target);
            public void GiveDamage(Character target, int damage);
        }


        // =========================================================================
        // DERIVED CLASS: Player (inherits Character, implements IAttackable)
        // Represents the playable character
        // Add on Mana management, skill abilities, and shield mechanics.
        // =========================================================================

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


        // =========================================================================
        // DERIVED CLASS: Enemy (inherits Character, implements IAttackable)
        // Represents all hostile NPCs in the game
        // =========================================================================

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


        // =========================================================================
        // CLASS: BattleSystem
        // Included in the Main Program class. It manages user interface for interaction during battle
        // Handles player action input (attack/defense/heal/inventory) and to display battle progress
        // =========================================================================
        

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

        // =========================================================================
        // CLASS: Story
        // Handles all narrative content
        // Contains methods that hold the segmented level intro 
        // =========================================================================

        class Story
        {
            //--------------- NARRATOR METHOD ----------------
            public static void Narrator(string text)
            {
                foreach (char letter in text)
                {
                    if (Console.KeyAvailable)
                    {
                        Console.ReadKey(true); //Press any ket to skip animation
                        Console.Write(text.Substring(text.IndexOf(letter)));
                        break;
                    }

                    Console.Write(letter);
                    Thread.Sleep(35); //speed of animation
                }
                Console.WriteLine();
            }

            public static void ShowIntro()
            {
                Console.Clear();
                Console.WriteLine("======================================================================");
                Console.WriteLine("                       THE PRINCE OF ELDORIA\t\t\t");
                Console.WriteLine("======================================================================");
                Console.WriteLine(@" 
                                                 ____                                         
                                              .-""    `-.      ,                               
                                            .'          '.   /j\                              
                                           /              \,/:/#\                /\           
                                          ;              ,//' '/#\              //#\          
                                          |             /' :   '/#\            /  /#\         
                                          :         ,  /' /'    '/#\__..--""""""""/    /#\__      
                                           \       /'\'-._:__    '/#\        ;      /#, """"""---
                                            `-.   / ;#\']"" ; """"""--./#J       ':____...!       
                                               `-/   /#\  J  [;[;[;Y]         |      ;        
                """"""""""""---....             __.--""/    '/#\ ;   "" ""  |     !    |   #! |        
                             """"--.. _.--""""     /      ,/#\'-..____.;_,   |    |   '  |        
                                   ""-.        :_....___,/#} ""####"" | '_.-"",   | #['  |        
                                      '-._      |[;[;[;[;|         |.;'  /;\  |      |        
                                      ,   `-.   |        :     _   .;'    /;\ |   #"" |        
                                      !      `._:      _  ;   ##' .;'      /;\|  _,  |        
                                     .#\""""""---..._    ';, |      .;{___     /;\  ]#' |__....--
                          .--.      ;'/#\         \    ]! |       ""| , """"""--./_J    /         
                         /  '%;    /  '/#\         \   !' :        |!# #! #! #|    :`.__      
                        i__..'%] _:_   ;##J         \      :""#...._!   '  ""  ""|__  |    `--.._
                         | .--"""""" !|""""""""  |""""""----...J     | '##"""" `-._       |  """"""---.._    
                     ____: |      #|      |         #|     |          ""]      ;   ___...-""T,  
                    /   :  :      !|      |   _______!_    |           |__..--;""""""     ,;MM;  
                   :____| :    .-.#|      |  /\      /#\   |          /'               ''MM;  
                    |"""""": |   /   \|   .----+  ;      /#\  :___..--"""";                  ,'MM; 
                   _Y--:  |  ;     ;.-'      ;  \______/#: /         ;                  ''MM; 
                  /    |  | ;_______;     ____!  |""##""""""MM!         ;                    ,'MM;
                 !_____|  |  |""#""#""|____.'""""##""  |       :         ;                     ''MM  
                  | """"""""--!._|     |##""""         !       !         :____.....-------"""""""""""" |'
                  |          :     |______                        ___!_ ""#""""#""""#""""""#""""""#""""""|  
                __|          ;     |MM""MM""""""""""---..._______...--""""MM""MM]                   |   
                  ""\-.      :      |#                                  :                   |  
                    /#'.    |      /##,                                !                   |  
                   .',/'\   |       #:#,                                ;       .==.       |  
                  /""\'#""\',.|       ##;#,                               !     ,'||||',     |  
                        /;/`:       ######,          ____             _ :     M||||||M     |  
                       ###          /;""\.__""-._   """"""                   |===..M!!!!!!M_____|  
                                           `--..`--.._____             _!_                    
                                                          `--...____,=""_.'`-.____ 
                ");



                Narrator("The Kingdom of Eldoria had long been a land of peace.");
                Narrator("Under the rule of King Aldren and Queen Lyra, the kingdom prospered and its people lived without fear");
                Narrator("One night, during a grand royal celebration, the sky suddenly darkened.");
                Narrator("A violent storm formed above the castle, and a mysterious portal opened in the throne hall.");
                Narrator("From the portal emerged the dark sorcerer Maldrath and his army of shadow creatures");
                Narrator("In the chaos, Maldrath defeated the royal guards and kidnapped Queen Lyra.");
                Narrator("Before disappearing into the darkness, he left a message:");
                Narrator(" ");
                Narrator("    “If you wish to see the queen again, send your bravest warrior to the Shadow Fortress… if he survives the journey.”");
                Narrator(" ");
                Narrator("The king was gravely injured during the attack, leaving the kingdom in despair.");
                Narrator("With the royal army scattered, the young Prince Aric swore an oath to rescue the queen and restore peace to the kingdom");
                Narrator("The prince sets out on a dangerous journey.\r\n");

                Console.WriteLine("\n(Press any key to skip narration)");
                Console.WriteLine("\nPress any key to begin your destiny...");
                Console.ReadKey();

                Narrator(" ");
                Narrator("The prince arrives at the Ruins of Valtheris, ");
                Narrator("an ancient city abandoned long ago.\r\n");
                Console.WriteLine(@" 
                          .----------.
                          :`.---------`.          .-----------.
                          '.|_.------..'          :`.----------`.  .-----.._
                           | ||    | ||           `.|__.------..' |`--..__.'|
                           | ||__..| ||-._ ___    | | |.... | ||-.|`--.._ |.'----.
                    _ .:::/ / |    | ||   ___  `--| | |  _  | ||  | | | | |\      `.
                  .` `.  -'.|_|    `.||  /`.  `.  '.|_\     `.|/  | | | | ||`.      `.
                 / `. _`.  `-   _     _  |  `.__`   _           _ `.|.' | || |\       \
                 \   /   \ _            _ `. |   |     `--   _          '.|| | `.______.
                 /`. |   |  .'`---...____   `'---' _       _          _    | | |'------'
                 | /`.-_.' |`---...___ . |  `--      ______________________`.|_| |  ` |
                 | | | '|  `---...____|.'    _     |`._-_______-______-_____`. | |    |
                .| | |  | _ |  .---. | '| `-       '.|  ' ' '  '  '  '  ' ' ' || |    |  
                ' `| |  |   /  ||  |'|  |      _    |'.__.-------.__.-----.__.'| | .- | \
                |  | |' |   |  ||  | |  |   `--     | |..|    |  |''|   | |::| `.|____| |
                \   `|__| _ |  || _| '. | .`-._     | |  |    |  |  |   | |  | `--     .'
                 '.         |  ||  |  | | |`.  `. _ | |  |    |  |  |   | |  |   _    `.
                   '-. _    \__|   |  | | |  `.__`. | |  | __ |  |  |___| |  |  `-- _  |
                      `''''.    _  `-.|.' '.  |   |  `|__| ___ `.|__| _ `.|__|        /
                           :    _    _      `.|_.-'   `-             _          _   .'
                            `-._______.::.__     `-     _      _       ___.:::.__.-'
                                            `----.....__..------------`
                ");
                Narrator("While exploring the ruins,");
                Narrator("Aric discovers an ancient scroll that reveals a shocking truth.");
                Console.ReadKey();
                Console.WriteLine(@" 
                _______________________
              =(__    ___      __     _)=
                |                     |
                |                     |
                |       Eldorian      |
                |     Royal Magical   |
                |        Academy      |
                |                     |
                |                     |
                |                     |
                |                     |
                |__    ___   __    ___|
              =(_______________________)=");
                Narrator("Maldrath was once the royal mage of Eldoria, ");
                Narrator("but he was exiled after experimenting with forbidden magic.\r\n");
                Console.ReadKey();
                Narrator("At the center of the ruins, the prince faces the Undead General, ");
                Narrator("a powerful warrior cursed to guard the ruins forever.\r\n");
                Console.WriteLine(@" 
                   , ,, ,                              
                   | || |    ,/  _____  \.             
                   \_||_/    ||_/ _  _\_||             
                     ||       \_| . . |_/              
                     ||         |  L  |                
                    ,||         |.===.|                
                    |>|      ___`>  -<'___             
                    |>|\    /             \            
                    \>| \  /  ,    .    .  |           
                     ||  \/  /|    |    |  |           
                     ||\  ` / | ___|___ |  |     (     
                  (( || `--'  | _______ |  |     ))  ( 
                (  )\|| (  )\ | - --- - | -| (  ( \  ))
                (\/  || ))/ ( | -- - -- |  | )) )  \(( 
                 ( ()||((( ())|         |  |( (( () )
                ");
                Narrator(" ");
                Console.ReadKey();

            }

            public static void AfterLevel1()
            {
                Narrator("After defeating the general, ");
                Narrator("the prince obtains an ancient sword infused with holy magic,  ");
                Console.WriteLine(@" 
                               <>
                             //
                           //
                <>(((((((({@}::::::::::::::::::::::::::::::::::::::::::::::::::::======->
                           \\
                             \\
                               <>
                ");
                Narrator("capable of harming powerful dark creatures. ");
                Console.ReadKey();
                Narrator("The prince begins to question the past of the kingdom. ");
                Narrator(" ");
                Narrator("He continuos the journey........ ");
                Console.ReadKey();

                Narrator("The next obstacle is the deadly Mount Ignivar, ");
                Narrator(" where rivers of lava flow and fire monsters roam. ");
                Console.WriteLine(@" 
                    .                  .-.    .  _   *     _   .
                           *          /   \     ((       _/ \       *    .
                         _    .   .--'\/\_ \     `      /    \  *    ___
                     *  / \_    _/ ^      \/\'__        /\/\  /\  __/   \ *
                       /    \  /    .'   _/  /  \  *' /    \/  \/ .`'\_/\   .
                  .   /\/\  /\/ :' __  ^/  ^/    `--./.'  ^  `-.\ _    _:\ _
                     /    \/  \  _/  \-' __/.' ^ _   \_   .'\   _/ \ .  __/ \
                   /\  .-   `. \/     \ / -.   _/ \ -. `_/   \ /    `._/  ^  \
                  /  `-.__ ^   / .-'.--'    . /    `--./ .-'  `-.  `-. `.  -  `.
                @/        `.  / /      `-.   /  .-'   / .   .'   \    \  \  .-  \%
                @&8jgs@@%% @)&@&(88&@.-_=_-=_-=_-=_-=_.8@% &@&&8(8%@%8)(8@%8 8%@)%
                @88:::&(&8&&8:::::%&`.~-_~~-~~_~-~_~-~~=.'@(&%::::%@8&8)::&#@8::::
                `::::::8%@@%:::::@%&8:`.=~~-.~~-.~~=..~'8::::::::&@8:::::&8:::::'
                 `::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::.'");
                Console.ReadKey();

                Narrator("During the climb, ");
                Narrator("the prince meets a mysterious traveler");
                Console.WriteLine(@" 
                        (                 ,&&&.
                        )                .,.&&
                       (  (              \=__/
                           )             ,'-'.
                     (    (  ,,      _.__|/ /|
                      ) /\ -((------((_|___/ |
                    (  // | (`'      ((  `'--|
                  _ -.;_/ \\--._      \\ \-._/.
                 (_;-// | \ \-'.\    <_,\_\`--'|
                 ( `.__ _  ___,')      <_,-'__,'
                  `'(_ )_)(_)_)'"); 
                Console.ReadKey();
                Narrator("He warns the Prince:\r\n ");
                Narrator("“The darkness you seek was once created by the kingdom you protect.”\r\n");
                Console.ReadKey();

                Narrator("At the summit of the mountain, ");
                Narrator("the prince faces the ancient Fire Dragon, awakened by Maldrath’s magic.\r\n ");
                Console.WriteLine(@"              '
                              __
                          _.-'.-'-.__
                       .-'.       '-.'-._ __.--._
                -..'\,-,/..-  _         .'   \   '----._
                 ). /_ _\' ( ' '.         '-  '/'-----._'-.__
                 '.<a} {a>.'     '-r   _      .-.       '-._ \
                 '.\. Y .).'       ( .'  .      .\          '\'.
                 .-')'|'/'-.        \)    )      '',_      _.c_.\
                   .<, ,>.          |   _/\        . ',   :   : \\
                  .' \_/ '.        /  .'   |          '.     .'  \)
                                  / .-'    '-.        : \   _;   ||
                                 / /    _     \_      '.'\ ' /   ||
                                /.'   .'        \_      .|   \   \|
                               / /   /      __.---'      '._  ;  ||
                              /.'  _:-.____< ,_           '.\ \  ||
                             // .-'     '-.__  '-'-\_      '.\/_ \|
                            ( };====.===-==='        '.    .  \\: \
                             \\ '._        /          :   ,'   )\_ \
                              \\   '------/            \ .    /   )/
                               \|        _|             )Y    |   /
                                \\      \             .','   /  ,/
                                 \\    _/            /     _/
                                  \\   \           .'    .'
                                   '| '1          /    .'
                                     '. \        |:    /
                                       \ |       /', .'
                                        \(      ( ;z'
                                         \:      \ '(_
                                          \_,     '._ '-.___
                                                      '-' -.\
                ");
                Console.ReadKey();
            }

            public static void AfterLevel2()
            {
                Narrator("The dragon attacks fiercely, ");
                Narrator(" but the prince manages to defeat it using the holy sword. ");
                Console.ReadKey();
                Narrator("Before dying, ");
                Narrator("the dragon reveals another secret:\r\n ");
                Narrator("............ ");
                Narrator("“The queen carries a power the sorcerer desires.” ");
                Console.ReadKey();
                Narrator("Now the prince realizes the kidnapping may be part of a much bigger plan.\r\n ");
                Narrator("He keeps moving forward. ");
                Console.ReadKey();
                Narrator("Beyond the mountain lies the land surrounding Shadow Fortress. ");
                Console.WriteLine(@"
                   .         .          /\      .:  *       .          .              .
                                 *    .'  `.      .     .     *      .                  .
                  :             .    /      \  _ .________________  .                    .
                       |            `.+-~~-+.'/.' `.^^^^^^^^\~~~~~\.                      .
                 .    -*-   . .       |u--.|  /     \~~~~~~~|~~~~~|
                       |              |   u|.'       `."" ""  |"" "" ""|                        .
                    :            .    |.u-./ _..---.._ \"" "" | "" "" |
                   -*-            *   |    ~-|U U U U|-~____L_____L_                      .
                    :         .   .   |.-u.| |..---..|""//// ////// /\       .            .
                          .  *        |u   | |       |// /// // ///==\     / \          .
                 .          :         |.--u| |..---..|//////~\////====\   /   \       .
                      .               | u  | |       |~~~~/\u |~~|++++| .`+~~~+'  .
                                      |.-|~U~U~|---..|u u|u | |u ||||||   |  U|
                                   /~~~~/-\---.'     |===|  |u|==|++++|   |   |
                          aaa      |===| _ | ||.---..|u u|u | |u ||HH||U~U~U~U~|        aa@@
                     aaa@@@@@@aa   |===|||||_||      |===|_.|u|_.|+HH+|_/_/_/_/aa    a@@@@@@
                 aa@@@@@@@@@@@@@@a |~~|~~~~\---/~-.._|--.---------.~~~`.__ _.@@@@@@a    ~~~~
                   ~~~~~~    ~~~    \_\\ \  \/~ //\  ~,~|  __   | |`.   :||  ~~~~
                                     a\`| `   _//  | / _| || |  | `.'  ,''|     aa@@@@@@@a
                 aaa   aaaa       a@@@@\| \  //'   |  // \`| |  `.'  .' | |  aa@@@@@@@@@@@@@
                @@@@@a@@@@@@a      ~~~~~ \\`//| | \ \//   \`  .-'  .' | '/      ~~~~~~~  ~~
                @S.C.E.S.W.@@@@a          \// |.`  ` ' /~  :-'   .'|  '/~aa
                ~~~~~~ ~~~~~~         a@@@|   \\ |   // .'    .'| |  |@@@@@@a
                                    a@@@@@@@\   | `| ''.'     .' | ' /@@@@@@@@@a");
                Console.ReadKey();
                Narrator("This area is filled with the sorcerer’s elite forces. ");
                Console.ReadKey();
                Narrator("The prince fights through wave after wave of enemies. ");
                Console.WriteLine(@" 
                                        /|
                                      /'||
                                     |  ||
                                     |  ||
                                     |  ||
                                     |  ||
                                     |  ||
                                     |  ||
                                     |  ||
                                     |  ||
                                     |  ||
                                     |  ||
                                     |  ||
                                     |  ||
                                     |  ||
                                     |  ||
                                     |  ||         __.--._
                                     |  ||      /~~   __.-~\ _
                                     |  ||  _.-~ / _---._ ~-\/~\
                                     |  || // /  /~/  .-  \  /~-\
                                     |  ||((( /(/_(.-(-~~~~~-)_/ |
                                     |  || ) (( |_.----~~~~~-._\ /
                                     |  ||    ) |              \_|
                                     |  ||     (| =-_   _.-=-  |~)        ,
                                     |  ||      | `~~ |   ~~'  |/~-._-'/'/_,
                                     |  ||       \    |        /~-.__---~ , ,
                                     |  ||       |   ~-''     || `\_~~~----~
                                     |  ||_.ssSS$$\ -====-   / )\_  ~~--~
                             ___.----|~~~|%$$$$$$/ \_    _.-~ /' )$s._
                    __---~-~~        |   |%%$$$$/ /  ~~~~   /'  /$$$$$$$s__
                  /~       ~\    ============$$/ /        /'  /$$$$$$$$$$$SS-.
                /'      ./\\\\\\_( ~---._(_))$/ /       /'  /$$$$%$$$$$~      \
                (      //////////(~-(..___)/$/ /      /'  /$$%$$%$$$$'         \
                 \    |||||||||||(~-(..___)$/ /  /  /'  /$$$%$$$%$$$            |
                  `-__ \\\\\\\\\\\(-.(_____) /  / /'  /$$$$%$$$$$%$             |
                      ~~""""""""""""""""""""-\.(____) /   /'  /$$$$$%%$$$$$$\_            /
                                    $|===|||  /'  /$$$$$$$%%%$$$$$( ~         ,'|
                                __  $|===|%\/'  /$$$$$$$$$$$%%%%$$|        ,''  |
                               ///\ $|===|/'  /$$$$$$%$$$$$$$%%%%$(            /'
                                \///\|===|  /$$$$$$$$$%%$$$$$$%%%%$\_-._       |
                                 `\//|===| /$$$$$$$$$$$%%%$$$$$$-~~~    ~      /
                                   `\|-~~(~~-`$$$$$$$$$%%%///////._       ._  |
                                   (__--~(     ~\\\\\\\\\\\\\\\\\\\\        \ \
                                   (__--~~(       \\\\\\\\\\\\\\\\\\|        \/
                                    (__--~(       ||||||||||||||||||/       _/
                                     (__.--._____//////////////////__..---~~
                                     |   """"""""'''''           ___,,,,ss$$$%
                                    ,%\__      __,,,\sssSS$$$$$$$$$$$$$$%%
                                  ,%%%%$$$$$$$$$$\;;;;\$$$$$$$$$$$$$$$$%%%$.
                                 ,%%%%%%$$$$$$$$$$%\;;;;\$$$$$$$$$$$$%%%$$$$
                               ,%%%%%%%%$$$$$$$$$%$$$\;;;;\$$$$$$$$$%%$$$$$$,
                              ,%%%%%%%%%$$$$$$$$%$$$$$$\;;;;\$$$$$$%%$$$$$$$$
                             ,%%%%%%%%%%%$$$$$$%$$$$$$$$$\;;;;\$$$%$$$$$$$$$$$
                             %%%%%%%%%%%%$$$$$$$$$$$$$$$$$$\;;;$$$$$$$$$$$$$$$
                               """"==%%%%%%%$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$SV""
                                           $$$$$$$$$$$$$$$$$$$$====""""""""
                                             """"""""""""""""""~~~~
                ");
                Console.ReadKey();
                Narrator("The Shadow Commander,Alucard, Maldrath’s most loyal warrior appears..... ");
                Console.WriteLine(@" 
                                           __.--|~|--.__           Alucard             ,,;/;
                                         /~     | |    ;~\     The Shadow Commander ,;;;/;;'
                                        /|      | |    ;~\\                      ,;;;;/;;;'
                                       |/|      \_/   ;;;|\                    ,;;;;/;;;;'
                                       |/ \          ;;;/  )                 ,;;;;/;;;;;'
                                   ___ | ______     ;_____ |___....__      ,;;;;/;;;;;'
                             ___.-~ \\(| \  \.\ \__/ /./ /:|)~   ~   \   ,;;;;/;;;;;'
                         /~~~    ~\    |  ~-.     |   .-~: |//  _.-~~--,;;;;/;;;;;'
                        (.-~___     \.'|    | /-.__.-\|::::| //~     ,;;;;/;;;;;'
                        /      ~~--._ \|   /          `\:: |/      ,;;;;/;;;;;'
                  .-|            ~~|   |  /V""""""""V\ |:  |     ,;;;;/;;;;;' \
                    /                   \  |  ~`^~~^'~ |  /    ,;;;;/;;;;;'    ;
                   (        \             \|`\._____./'|/    ,;;;;/;;;;;'      '\
                  / \        \                             ,;;;;/;;;;;'     /    |
                 |            |                          ,;;;;/;;;;;'      |     |
                |`-._          |                       ,;;;;/;;;;;'              \
                |             /                      ,;;;;/;;;;;'  \              \__________
                (             )                 |  ,;;;;/;;;;;'      |        _.--~
                 \          \/ \              ,  ;;;;;/;;;;;'       /(     .-~_..--~~~~~~~~~~
                 \__         '  `       ,     ,;;;;;/;;;;;'    .   /  \   / /~
                 /          \'  |`._______ ,;;;;;;/;;;;;;'    /   :    \/'/'       /|_/|   ``|
                | _.-~~~~-._ |   \ __   .,;;;;;;/;;;;;;' ~~~~'   .'    | |       /~ (/\/    ||
                /~ _.-~~~-._\    /~/   ;;;;;;;/;;;;;;;'          |    | |       / ~/_-'|-   /|
                (/~         \| /' |   ;;;;;;/;;;;;;;;            ;   | |       (.-~;  /-   / |
                |            /___ `-,;;;;;/;;;;;;;;'            |   | |      ,/)  /  /-   /  |
                 \            \  `-.`---/;;;;;;;;;' |          _'   | |    /'('  /  /|- _/  //
                   \           /~~/ `-. |;;;;;''    ______.--~~ ~\  | |  ,~)')  /   | \~-==//
                     \      /~(   `-\  `-.`-;   /|    ))   __-####\ | |   (,   /|    |  \
                       \  /~.  `-.   `-.( `-.`~~ /              '~~)| |   '   / |    |   ~\
                        \(   \    `-._ /~)_/|  /            '       | |      /  \     \_\  `\
                        ,~`\  `-._  / )#####|/            '   /     | |  _--~ _/ | .-~~____--'
                       ,'\  `-._  ~)~~ `                '           | | ((~>/~   \ (((' -_
                     ,'   `-.___)~~      `                          | |           ~-_     ~\_
                 _.,'        ,'           `                         | |            _-~-__    (
                |  `-.     ,'              `                \       | |          ((.-~~~-~_--~
                `\    `-.;'                  `#####""                | |           ""     ((.-~~
                  `-._   )               \     |   |        .       |  \                 ""
                      `~~  _/                  |    \               |   `---------------------
                        |/~                `.  |     \        .     |  O    __.---------------
                         |                   \ ;      \             |   _.-~
                         |                    |        |            |  /  |
                          |                   |         |           |/'  |
                ");
                Console.ReadKey();

            }

            public static void AfterLevel3()
            {
                Narrator("After defeating Alucard, ");
                Narrator("the prince learns the truth:\r\n");
                Console.ReadKey();
                Narrator("Queen Lyra possesses ancient light magic, ");
                Narrator("A rare power capable of either saving or destroying the world. ");
                Narrator("............... ");
                Narrator("Maldrath plans to steal that power. ");
                Console.ReadKey();
                Narrator("Aric becomes more dedicated to learn the truth ");
                Narrator(" ");
                Narrator(" ");
                Narrator("The prince finally reaches the dark stronghold known as the Shadow Fortress ");
                Narrator("The fortress is surrounded by dark clouds and corrupted magic. ");
                Console.WriteLine(@" 
                                   (   .                   _ _ _ _ _
                    (   .     .  .=##                      ]-I-I-I-[                    /
                  .=##   .  (      ( .                     \ `  ' /        \\\' ,      / //
                    ( .   .=##  .       .                   |'  []          \\\//    _/ //'
                  .     .   ( .    .        _----|          |.  '|           \_-//' /  //<'
                                             ----|_----|    | ' .|             \ ///  <//'
                    _ _ _ _ _ _      _----|      | ----|    | [] |             /  >>   \\\`
                    ]-I-I-I-I-[       ----|      |     |    |. ` |            /,)-^>>  _\`
                     \ `   '_/            |     / \    |    | /^\|            (/   \\ / \\\
                      []  `__|            ^    / ^ \   ^    | |*||                  //  //\\\
                      |__   ,|           / \  / ^ ^`\ / \   | ===|                 ((`
                   ___| ___ ,|__        / ^  /=_=_=_=\ ^ \  |, `_|
                   I_I__I_I__I_I       (====(_________)_^___|____|____
                   \-\--|-|--/-/       |     I  [ ]__I I_I__|____I_I_|
                    |[] `    '|__   _  |_   _|`__  ._[  _-\--|-|--/-/
                   / \  [] ` .|  |-| |-| |-| |_| |_| |_| | []   [] |
                  <===>    `  |.            .      .     |    '    |
                  ] []|  `    |   []    --   []      `   |   [] '  |
                  <===>.  `   |  .   '  .       '  .[]   | '       |
                   \_/    .   |       .       '          |   `  [] |
                    | []    . |   .  .           ,  .    | ,    .  |
                    |    . '  |       . []  '            |    []'  |
                   / \   ..   |  `      .    .     `[]   | -   `   |
                  <===>      .|=-=-=-=-=-=-=-=-=-=-=-=-=-|    .   / \
                  ] []|` ` [] |`  .  .   _________   .   |-      <===>
                  <===>  `  ' | '   |||  |       |  |||  |  []   <===>
                   \_/     -- |   . |||  |       |  |||  | .  '   \_/
                  ./|' . . . .|. . .||||/|_______|\|||| /|. . . . .|\_");
                Console.ReadKey();

                Narrator("Inside the throne hall,");
                Narrator(" Prince Aric discovers Queen Lyra trapped inside a magical barrier.  ");
                Console.WriteLine(@" 
                 ||                  __                  ||
                 ||                 /__`.                ||
                 ||                / \ `\\               ||
                 ||               /   \  `\              ||
                 ||              /     \   \             ||
                 ||             /_______\  /\            ||
                 ||             (((( ))))                ||
                 ||            (((' . ')))               ||
                 ||            (((\_-_/)))               ||
                 ||           ((((_) (_))))              ||
                 ||           /((( \ / )))\              ||
                 ||          / (((  ^  ))) \             ||
                 ||         / / ((  ^  )) \ \            ||
                 ||        ( (   \  ^  /   ) )           ||
                 ||         \ \   )www(   / /            ||
                 ||          `\\ /     \ //'             ||
                 ||            /'       `\               ||
                 ||           /           \              ||
                 ||          /             \             ||
                 ||         /               \            ||
                 ||        /                 \           ||
                 ||       /                   \          ||
                 ||      /                     \         ||
                 ||     /                       \        ||
                 ||    /                         \       ||
                 ||   /                           \      ||
                 ||  |                             |     ||
                 ||   `-----......_____......-----'      ||");
                Console.ReadKey();

                Narrator("Before he can free her,");

                Narrator("the dark sorcerer Maldrath appears.\r\n ");
                Console.ReadKey();

                Console.WriteLine(@" 
                                                ==(W{==========-      /===-                        
                                                  ||  (.--.)         /===-_---~~~~~~~~~------____  
                                                  | \_,|**|,__      |===-~___                _,-' `
                                     -==\\        `\ ' `--'   ),    `//~\\   ~~~~`---.___.-~~      
                                 ______-==|        /`\_. .__/\ \    | |  \\           _-~`         
                           __--~~~  ,-/-==\\      (   | .  |~~~~|   | |   `\        ,'             
                        _-~       /'    |  \\     )__/==0==-\<>/   / /      \      /               
                      .'        /       |   \\      /~\___/~~\/  /' /        \   /'                
                     /  ____  /         |    \`\.__/-~~   \  |_/'  /          \/'                  
                    /-'~    ~~~~~---__  |     ~-/~         ( )   /'        _--~`                   
                                      \_|      /        _) | ;  ),   __--~~                        
                                        '~~--_/      _-~/- |/ \   '-~ \                            
                                       {\__--_/}    / \\_>-|)<__\      \                           
                                       /'   (_/  _-~  | |__>--<__|      |                          
                                      |   _/) )-~     | |__>--<__|      |                          
                                      / /~ ,_/       / /__>---<__/      |                          
                                     o-o _//        /-~_>---<__-~      /                           
                                     (^(~          /~_>---<__-      _-~                            
                                    ,/|           /__>--<__/     _-~                               
                                 ,//('(          |__>--<__|     /  -               .----_          
                                ( ( '))          |__>--<__|    |                 /' _---_~\        
                             `-)) )) (           |__>--<__|    |               /'  /     ~\`\      
                            ,/,'//( (             \__>--<__\    \            /'  //        ||      
                          ,( ( ((, ))              ~-__>--<_~-_  ~--____---~' _/'/        /'       
                        `~/  )` ) ,/|                 ~-_~>--<_/-__       __-~ _/                  
                      ._-~//( )/ )) `                    ~~-'_/_/ /~~~~~~~__--~                    
                       ;'( ')/ ,)(                              ~~~~~~~~~~                         
                      ' ') '( (/                                                                   
                        '   '  `");
                Console.ReadKey();

                Narrator("Maldrath reveals that he kidnapped the queen because she possesses ancient light magic, ");
                Narrator("a power he plans to use to strengthen his dark abilities and take revenge on the kingdom.");
                Narrator(" Maldrath:\r\n ");
                Narrator("  “You have come far, prince…");
                Narrator("  but this is where your journey ends!” ");
                Narrator(" The prince refuses to give up");
                Narrator(" he challenges Maldrath to a final battle.\r\n");

                Console.WriteLine(@" ");
            }

            public static void FinalScene()
            {
                Narrator("After the battle, ");
                Narrator("Maldrath collapses as the dark magic surrounding the fortress begins to fade.");
                Console.ReadKey();
                Narrator("Even in defeat,");
                Narrator("he warns that darkness will always exist in the world.\r\n");
                Narrator("Maldrath (weakly): ");
                Narrator(" “Even if I fall… darkness will rise again…”\r\n ");
                Console.ReadKey();
                Narrator("With Maldrath defeated,");
                Narrator(" the magical barrier holding the queen breaks apart. ");
                Narrator("The prince frees her at last.");
                Console.WriteLine(@" 
                                    (,);        ...
                                   ((  ^_.     (()))
                                    ' / /_\    {' ())
                                      L( '}     )_ (()
                                       ) (_    (   (_)
                                     (_  / }{)===='_/
                                     | `/\/\     |   \
                                     L___/ |     |    |
                                      |  . \     |    |
                                      |_/ \_\    |    |
                                      ( ____ )   |    |
                                       | | | |   |    |
                                   ( --' | \ |_  '~~~~'
                                   /_/---) (___)  _/Y");
                Console.ReadKey();
                Narrator("Queen Lyra:\r\n ");
                Narrator(" “You came for me…");
                Narrator("I knew you would.”\r\n ");
                Narrator("Prince Aric:\r\n");
                Narrator(" “I made a promise to bring you home.”\r\n ");
                Console.ReadKey();
                Narrator("The darkness surrounding the fortress slowly disappears as the sky begins to clear.\r\n ");
                Console.WriteLine(@" 
                  ........::::::::::::..           .......|...............::::::::........
                     .:::::;;;;;;;;;;;:::::.... .     \   | ../....::::;;;;:::::.......
                         .       ...........   / \\_   \  |  /     ......  .     ........./\
                ...:::../\\_  ......     ..._/'   \\\_  \###/   /\_    .../ \_.......   _//
                .::::./   \\\ _   .../\    /'      \\\\#######//   \/\   //   \_   ....////
                    _/      \\\\   _/ \\\ /  x       \\\\###////      \////     \__  _/////
                  ./   x       \\\/     \/ x X           \//////                   \/////
                 /     XxX     \\/         XxX X                                    ////   x
                -----XxX-------------|-------XxX-----------*--------|---*-----|------------X--
                       X        _X      *    X      **         **             x   **    *  X
                      _X                    _X           x                *          x     X_");
                Console.ReadKey();

                Narrator("With the queen rescued and the dark sorcerer defeated,");
                Narrator("peace begins to return to the kingdom of Eldoria.\r\n");
                Console.ReadKey();

                Narrator("The prince and the queen leave the Shadow Fortress together,");
                Narrator("knowing that the kingdom is safe once again.\r\n");
                Console.WriteLine(@" 
                      .                         .                             .
                                      .     *          .
                                .
                                               .
                   *              .                                    .
                                                      .           '`                   .
                      .                                         '`
                                  //////                                     ####
                                  //////                                   ########
                                 (//////)                                 ##########
                ... ..  ..  ... . ////// . ... .())((() .. . . . .... . . . ..   . . ...  . . .
                             _____|____|_____  ((()(()))                   / / / / /
                            /                \ (((((((()                     /  / /
                           |                  |()(()(())_
                           |                  |(((()()() |
                           \                  '\()))))(  |
                            \                   \(((((  /
                             \              .    \___. (
                              \            / \    |:| ) \
                              |            | |`------'   |
                              \____________/  \_________/                             
                 ..........................................................................
                ");
                Console.ReadKey();
                Narrator("The prince’s courage and determination have saved the kingdom");
                Narrator(" and proven that he is worthy of becoming its future ruler.\r\n");
                Console.ReadKey();
                Console.WriteLine(@" 
                ,---------. .---.  .---.     .-''-.              .-''-.  ,---.   .--. ______      
                \          \|   |  |_ _|   .'_ _   \           .'_ _   \ |    \  |  ||    _ `''.  
                 `--.  ,---'|   |  ( ' )  / ( ` )   '         / ( ` )   '|  ,  \ |  || _ | ) _  \ 
                    |   \   |   '-(_{;}_). (_ o _)  |        . (_ o _)  ||  |\_ \|  ||( ''_'  ) | 
                    :_ _:   |      (_,_) |  (_,_)___|        |  (_,_)___||  _( )_\  || . (_) `. | 
                    (_I_)   | _ _--.   | '  \   .---.        '  \   .---.| (_ o _)  ||(_    ._) ' 
                   (_(=)_)  |( ' ) |   |  \  `-'    /         \  `-'    /|  (_,_)\  ||  (_.\.' /  
                    (_I_)   (_{;}_)|   |   \       /           \       / |  |    |  ||       .'   
                    '---'   '(_,_) '---'    `'-..-'             `'-..-'  '--'    '--''-----'`     
                                                                                                  ");
                Console.ReadKey();
            }

            //Method to assign respective afterlevel story based on Main Game Loop Iteration
            public static void AfterLevel(int i)
            {
                if (i == 0)
                {
                    AfterLevel1();
                }
                else if (i == 1)
                {
                    AfterLevel2();
                }
                else if (i == 2)
                {
                    AfterLevel3();
                }
                else
                {
                    FinalScene();
                }
            }
        }

        // =========================================================================
        // CLASS: UpgradeSystem
        // Presents as a special quest for game story enrichment
        // =========================================================================
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

        // =========================================================================
        // ABSTRACT CLASS: Item
        // Base class for all collectible items (Weapon and General item)
        // Force consistency and encourage reusability through abstract methods to
        // demonstrate abstraction and polymorphism.
        // =========================================================================
        public abstract class Item  
        {
            public string ItemName;
            public Item(string name_)
            {
                ItemName = name_;
            }

            public abstract string GiveDescription(); 
            public abstract void UseItem(Player prince);
        }

        // =========================================================================
        // DERIVED CLASS: WeaponItem (inherits Item)
        // Only one weapon can be equipped at a time; inventory is capped at 3 slots.
        // =========================================================================

        class WeaponItem :Item
        {
            public int AttackBonus { get; private set; }
            public int ManaBonus { get; private set; }
            public bool IsEquipped { get; set; }


            public WeaponItem(string name_, int attackBonus, int manaBonus) : base(name_)
            {
                AttackBonus = attackBonus;
                ManaBonus = manaBonus;
                IsEquipped = false;
            }

            public override string GiveDescription()
            {
                return $"{ItemName} | Attack: +{AttackBonus} | Mana: +{ManaBonus}";
            }

            public override void UseItem(Player prince)
            {
                prince.attack += AttackBonus;
                prince.mana += ManaBonus;
            }
        }

        // =========================================================================
        // DERIVED CLASS: GeneralItem (inherits Item)
        // Represents consumable items such as potions that restore HP or Mana.
        // Consumed on use and removed from inventory. Capped at 5 slots.
        // =========================================================================
        class GeneralItem : Item
        {
            private int HealthBonus { get; set; }
            private int ManaBonus { get; set; }


            public GeneralItem(string name_, int healthBonus, int manaBonus) : base(name_)
            {
                HealthBonus = healthBonus;
                ManaBonus = manaBonus;
            }

            public override string GiveDescription()
            {
                return $"{ItemName} | Health: +{HealthBonus} | Mana: +{ManaBonus}";
            }

            public override void UseItem(Player prince)
            {
                prince.health += HealthBonus;
                prince.mana += ManaBonus;
            }

        }

        // =========================================================================
        // CLASS: InventorySystem
        // Manages all player inventory interactions including equipping, unequipping,
        // consuming, and discarding items. Accessible during battle and loot phases.
        // =========================================================================

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

                }while (ChooseItem != 4);

            }
        }

    }

}