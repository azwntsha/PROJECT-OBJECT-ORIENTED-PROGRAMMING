using System;
namespace PrinceAdventureGame
{
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
}
