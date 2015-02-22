using System.IO;
using Tera.Data.Structures.World;

namespace Tera.Network.old_Server
{
    public class SpRemoveItem : ASendPacket
    {
        protected Item Item;

        public SpRemoveItem(Item item)
        {
            Item = item;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Item);
        }
    }
}