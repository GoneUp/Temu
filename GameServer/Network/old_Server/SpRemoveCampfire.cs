using System.IO;
using Tera.Data.Structures.World;

namespace Tera.Network.old_Server
{
    public class SpRemoveCampfire : ASendPacket
    {
        protected Campfire Campfire;
        protected byte Type;

        public SpRemoveCampfire(Campfire campfire, byte type = 0)
        {
            Campfire = campfire;
            Type = type;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Campfire);
            WriteByte(writer, Type);
        }
    }
}