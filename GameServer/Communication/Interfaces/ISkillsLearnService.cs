using System.Collections.Generic;
using Tera.Data.Structures.Player;
using Tera.Data.Structures.SkillEngine;

namespace Tera.Communication.Interfaces
{
    public interface ISkillsLearnService : IComponent
    {
        void Init();
        List<UserSkill> GetSkillLearnList(Player player);
        void BuySkill(Player player, int skillId, bool isActive);
        void LearnMountSkill(Player player, int skillId);
        void UseSkillBook(Player player, int bookId);
    }
}