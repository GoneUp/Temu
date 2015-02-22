using System.IO;
using Tera.Data.Structures.SkillEngine;

namespace Tera.Network.old_Server
{
    public class SpSkillPurchased : ASendPacket
    {
        protected UserSkill Skill;

        public SpSkillPurchased(UserSkill skill)
        {
            Skill = skill;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteByte(writer, 1); //IsActive?
            WriteByte(writer, 0); //???

            WriteDword(writer, Skill.PrevSkillId);
            WriteDword(writer, Skill.SkillId);
        }
    }
}
