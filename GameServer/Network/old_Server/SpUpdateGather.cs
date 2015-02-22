using System.IO;
using Tera.Data.Enums.Gather;

namespace Tera.Network.old_Server
{
    public class SpUpdateGather : ASendPacket
    {
        protected TypeName Type;
        protected short Value;

        public SpUpdateGather(TypeName type, short value)
        {
            Type = type;
            Value = value;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, Type.GetHashCode());
            WriteWord(writer, Value);
        }
    }
}