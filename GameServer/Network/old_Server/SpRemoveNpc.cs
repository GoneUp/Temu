using System.IO;
using Tera.Data.Structures.Npc;

namespace Tera.Network.old_Server
{
    public class SpRemoveNpc : ASendPacket //len 32
    {
        protected Npc Npc;
        protected int DespawnType; //1 - delete, 5 - death

        public SpRemoveNpc(Npc npc, int despawnType = 1)
        {
            Npc = npc;
            DespawnType = despawnType;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Npc);
            WriteSingle(writer, Npc.Position.X);
            WriteSingle(writer, Npc.Position.Y);
            WriteSingle(writer, Npc.Position.Z);
            WriteDword(writer, DespawnType);
            WriteDword(writer, 0);
        }
    }
}