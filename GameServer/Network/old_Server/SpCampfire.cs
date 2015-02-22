using System.IO;
using Tera.Data.Structures.World;

namespace Tera.Network.old_Server
{
    public class SpCampfire : ASendPacket
    {
        protected Campfire Campfire;

        public SpCampfire(Campfire campfire)
        {
            Campfire = campfire;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, 0);
            WriteUid(writer, Campfire);
            WriteDword(writer, Campfire.Type);
            WriteSingle(writer, Campfire.Position.X);
            WriteSingle(writer, Campfire.Position.Y);
            WriteSingle(writer, Campfire.Position.Z);
            WriteDword(writer, Campfire.Status);
        }
    }
}
