using System.Collections.Generic;
using Tera.Data.Interfaces;
using Tera.Data.Structures.Creature;
using Tera.Data.Structures.Npc;
using Tera.Data.Structures.Objects;
using Tera.Data.Structures.Player;
using Tera.Data.Structures.SkillEngine;

namespace Tera.Communication.Interfaces
{
    public interface ISkillEngine : IComponent
    {
        void Init();
        void UseSkill(IConnection connection, UseSkillArgs args);
        void UseSkill(IConnection connection, List<UseSkillArgs> argsList);
        void UseSkill(Npc npc, Skill skill);
        void MarkTarget(IConnection connection, Creature target, int skillId);
        void ReleaseAttack(Player player, int attackUid, int type);
        void UseProjectileSkill(Projectile projectile);
        void AttackFinished(Creature creature);
    }
}