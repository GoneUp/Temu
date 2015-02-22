using System.IO;
using Tera.Data.Structures.Creature;

namespace Tera.Network.old_Server
{
    public class SpMarkTarget : ASendPacket
    {
        protected Creature Creature;
        protected int SkillId;
        protected byte Flag;

        public SpMarkTarget(Creature creature, int skillId, byte flag = 1)
        {
            Creature = creature;
            SkillId = skillId + 0x4000000;
            Flag = flag;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Creature);
            WriteDword(writer, SkillId);
            WriteByte(writer, Flag);
        }
    }
}