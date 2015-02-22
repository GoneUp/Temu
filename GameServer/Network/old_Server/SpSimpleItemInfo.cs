using System.IO;

namespace Tera.Network.old_Server
{
    public class SpSimpleItemInfo : ASendPacket
    {
        protected int ItemId;

        public SpSimpleItemInfo(int itemId)
        {
            ItemId = itemId;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, ItemId);
            WriteDword(writer, 0);
            WriteByte(writer, 0);
        }
    }
}