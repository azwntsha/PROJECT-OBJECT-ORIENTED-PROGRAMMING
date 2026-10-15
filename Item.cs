using System;
namespace PrinceAdventureGame
{
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
}
