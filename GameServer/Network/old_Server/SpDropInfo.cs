using System.IO;
using Tera.Data.Structures.World;

namespace Tera.Network.old_Server
{
    public class SpDropInfo : ASendPacket // len 61
    {
        protected Item Item;

        public SpDropInfo(Item item)
        {
            Item = item;
        }

        public SpDropInfo(int itemId, WorldPosition worldPosition)
        {
            Item = new Item
                       {
                           Position = worldPosition,
                           Count = 1,
                           ItemId = itemId,
                           //20000000
                       };
        }

        public override void Write(BinaryWriter writer)
        {
            WriteByte(writer, "01003100"); //Shifts
            WriteUid(writer, Item);
            WriteSingle(writer, Item.Position.X);
            WriteSingle(writer, Item.Position.Y);
            WriteSingle(writer, Item.Position.Z);
            WriteDword(writer, Item.ItemId);
            WriteDword(writer, Item.Count);
            WriteByte(writer, "C0D4010001"); //??? 57
            WriteUid(writer, Item.Npc);
            WriteByte(writer, "31000000"); //Shifts
            WriteUid(writer, Item.Owner);
        }
    }
}