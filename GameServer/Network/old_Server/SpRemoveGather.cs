using System.IO;
using Tera.Data.Structures.Gather;

namespace Tera.Network.old_Server
{
    public class SpRemoveGather : ASendPacket
    {
        protected Gather Gather;
        protected byte DespawnType;

        public SpRemoveGather(Gather gather, byte despawnType = (byte) 0x01)
        {
            Gather = gather;
            DespawnType = despawnType;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Gather);
            WriteByte(writer, DespawnType);
        }
    }
}