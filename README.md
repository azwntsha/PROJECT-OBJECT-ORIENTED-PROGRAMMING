# The Prince of Eldoria  

## Group Members  
| No. | Name                                   | Student ID |
|-----|----------------------------------------|------------|
| 1   | ZULFA ALEYA BINTI ZUL EMRAN            | 25014269   |
| 2   | MUHAMMAD AQIL BIN MUHAMMAD AMRAN       | 24005828   |
| 3   | AZWA NATASHA BINTI AHMAD SARJI         | 25014192   |
| 4   | HADIF IRFAN UDA HARRIS                 | 25014026   |
| 5   | NUR ALYAA NABILAH BINTI MAZLAN         | 25014139   |
| 6   | NIK MUHAMMAD AQIL NIK SALIMI           | 25014164   |
| 7   | NURUL IRDINA BATRISYIA                 | 25013549   |

---

## 1.0 Project Description  
This project is a console-based adventure RPG was developed using C#.  
This Single-Player game is featuring a playable character named Prince Aric as the protagonist of this game.  
A specific Story system will navigate the storyline and narrate the progress of the game to the player.  
Player will be able to control this young prince to fight against legendary enemies with ultimate purpose is to save the Queen and restore the peace of Eldoria Kingdom.  

---

## 2.0 System Features  

- **2.1 Interactive narrative**  
  - The game interface was displayed in third-person perspective.  
  - Narrator-style text is used.  
  - ASCII art is included to enhance user experience.  

- **2.2 Battle System**  
  - The battle mechanic is based on a level progression system between the player and enemies.  
  - Instead of roaming or random encounters, the player only faces the next enemy.  
  - The next enemy appears only after the current one is successfully defeated.  

- **2.3 Inventory System**  
  - This game comes with an inventory system where player are given a spatial medium with prefix slots for two types of item.  
  - There are 5 slots for General Item.  
  - General Item consists of items that can be consumed such as potion to increase their stat immediately.  
  - There are 3 slots for Weapon Item.  
  - The player could choose one weapon to equip to enjoy stat boost at all time.  
  - Interaction between player and inventory system are included during battle.  
  - Interaction also occurs post level after defeating an enemy.  

- **2.4 Loot System**  
  - This system can be described as a Reward System.  
  - Player will be able to choose 5 options of reward into their inventory.  
  - Rewards are given upon defeating enemies on each level.  
  - Boss level does not provide rewards.  

---

## 3.0 OOP Concept Implemented  

- **3.1 Inherintance**  
  - Player class inherits from base class Character.  
  - Enemy class inherits from base class Character.  

- **3.2 Polymorphism**  
  - Class Item consists of abstract methods.  
  - Derived classes are enforced to implement these methods according to their behaviour.  
  - This is where polymorphism is applied.  
  - Example: `GiveDescription` and `UseItem`.  

- **3.3 Encapsulation**  
  - Attributes of Character such as health and mana are protected.  
  - These attributes are controlled by public properties.  
  - They are also managed through the constructor.  

- **3.4 Abstraction and Interface**  
  - `IAttackable` is implemented as a 'Contract'.  
  - Whenever Prince and Enemy attack, they should also return the damage.  
  - Item is an abstract class.  
  - Item possesses 2 abstract methods.  
  - `GeneralItem` and `WeaponItem` classes are its derived classes.  


---

## 4.0 How to Run the Program  
1. Open the project in **Visual Studio**.  
2. Build the project using **Build Solution**.  
3. Run the program with **Start Debugging (F5)**.  
4. Follow the instructions in the console to choose actions during battles.  

---
## 5.0 How to Open the README File  
**The file is written in Markdown format, therefore it may look different in plain text editors.** 

1. Download the project folder and locate the file named **README.md**.  
2. Right‑click the file and choose **Open with → Microsoft Visual Studio Version Selector**.  
3. Select **Visual Studio Community 2026** (or your installed version).  
4. The file will open inside Visual Studio.  
5. Use the **Preview option** in Visual Studio to view the Markdown formatting correctly. 

---


## Project Structure  
| File                  | Description                                                   |
|-----------------------|---------------------------------------------------------------|
| **Program.cs**        | Main program, Game Loop, battle system implement, Loot logic  |
| **Player.cs**         | Player class with attack logic and mana system                |
| **Enemy.cs**          | Enemy class with attack logic                                 |
| **Character.cs**      | Base class for Player and Enemy                               |
| **IAttackable.cs**    | Interface for attack methods                                  |
| **BattleSystem.cs**   | Display and controls turn-based battle mechanics              |
| **InventorySystem.cs**| Inventory management logic                                    |
| **Item.cs**           | Abstract item class, base class to WeaponItem and GeneralItem |
| **WeaponItem.cs**     | Derived class inherit Item with attack and mana bonus         |
| **GeneralItem.cs**    | Derived class inherit Item with mana and health bonus         |
| **Story.cs**          | Narration based of current level and ASCII art storytelling   |
| **UpgradeSystem.cs**  | Special quest to enrich game experience, offer upgrade        |


