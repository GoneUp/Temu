using Tera.Data.Structures.Creature;
using Tera.Data.Structures.SkillEngine;

namespace Tera.Data.Interfaces
{
    public interface IAi
    {
        void Init(Creature creature);
        void Release();
        void Action();
        void OnUseSkill(Skill skill);
        void OnAttack(Creature target);
        void OnAttacked(Creature attacker, int damage);
        Creature GetKiller();
        void DealExp();
        void DistanceToCreatureRecalculated(Creature creature, double distance);
    }
}