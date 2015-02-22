using System.IO;

namespace Tera.Network.Packets.ServerPackets
{
    public class SpCharacterCheckNameResult : ASendPacket
    {
        protected int Result;
        protected string Name;
        protected int Type;

        public SpCharacterCheckNameResult(int result, string name, int type = 1)
        {
            Result = result;
            Name = name;
            Type = type;
        }

        public override void Write(BinaryWriter writer)
        {
            //010008000800000016000200 0000 00000000 4E006500770074006500720061000000
            WriteByte(writer, "01000800080000001600");
            WriteDword(writer, Type);
            WriteDword(writer, Result);
            WriteString(writer, Name);
        }
    }
}