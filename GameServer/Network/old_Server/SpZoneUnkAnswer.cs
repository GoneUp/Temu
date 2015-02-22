using System.IO;

namespace Tera.Network.old_Server
{
    public class SpZoneUnkAnswer : ASendPacket
    {
        protected byte Sw;

        public SpZoneUnkAnswer(byte sw)
        {
            Sw = sw;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteByte(writer, Sw);
        }
    }
}