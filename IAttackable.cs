using System;
namespace PrinceAdventureGame
{
    public interface IAttackable
    {
        void DefaultAttack(Character target);
        void GiveDamage(Character target, int damage);
    }
}
