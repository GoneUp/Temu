using System.IO;

namespace Tera.Network.old_Server
{
    public class SpCharacterZoneData : ASendPacket
    {
        protected byte[] Datas;

        public SpCharacterZoneData(byte[] datas)
        {
            Datas = datas;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteByte(writer, 0);
            WriteByte(writer, Datas);
        }
    }
}