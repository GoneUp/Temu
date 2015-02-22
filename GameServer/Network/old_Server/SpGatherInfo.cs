using System.IO;
using Tera.Data.Structures.Gather;

namespace Tera.Network.old_Server
{
    public class SpGatherInfo : ASendPacket
    {
        protected Gather Gather;

        public SpGatherInfo(Gather gather)
        {
            Gather = gather;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Gather);
            WriteDword(writer, Gather.Id);
            WriteDword(writer, Gather.CurrentGatherCounter); //gather counter
            WriteSingle(writer, Gather.Position.X);
            WriteSingle(writer, Gather.Position.Y);
            WriteSingle(writer, Gather.Position.Z);
        }
    }
}