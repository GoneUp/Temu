using System.IO;
using Tera.Data.Structures.Creature;

namespace Tera.Network.old_Server
{
    public class SpRemoveHpBar : ASendPacket
    {
        protected Creature Creature;

        public SpRemoveHpBar(Creature creature)
        {
            Creature = creature;
        }
        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Creature);
        }
    }
}
