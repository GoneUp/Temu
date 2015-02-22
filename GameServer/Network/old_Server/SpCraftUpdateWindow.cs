using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpCraftUpdateWindow : ASendPacket
    {
        protected StorageItem Item;

        public SpCraftUpdateWindow(StorageItem item)
        {
            Item = item;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Item);
            WriteDword(writer, Item.ItemId);
            WriteDword(writer, /*Item.ItemTemplate.Extraction*/ 0); // in original "Tier" and i don't know, what that mean
            WriteDword(writer, 0);
            WriteByte(writer, 0);
        }
    }
}