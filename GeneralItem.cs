using System;
namespace PrinceAdventureGame
{
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
}
