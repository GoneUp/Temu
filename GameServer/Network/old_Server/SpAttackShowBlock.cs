using System.IO;
using Tera.Data.Structures.Creature;

namespace Tera.Network.old_Server
{
    public class SpAttackShowBlock : ASendPacket
    {
        protected Creature Creature;
        protected int SKillId;

        public SpAttackShowBlock(Creature creature, int skillId)
        {
            Creature = creature;
            SKillId = skillId;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Creature);
            WriteDword(writer, SKillId);
            WriteDword(writer, 0);
        }
    }
}
