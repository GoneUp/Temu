using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpCharacterThings : ASendPacket
    {
        protected Player Player;

        public SpCharacterThings(Player player)
        {
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Player);

            WriteDword(writer, Player.Inventory.GetItemId(1) ?? 0); //Item (hands)
            WriteDword(writer, Player.Inventory.GetItemId(3) ?? 0); //Item (body)
            WriteDword(writer, Player.Inventory.GetItemId(4) ?? 0); //Item (gloves)
            WriteDword(writer, Player.Inventory.GetItemId(5) ?? 0); //Item (shoes)
            WriteDword(writer, 0); //Item ???
            WriteDword(writer, 0); //Item ???
            WriteDword(writer, 0); //Item ???
            WriteDword(writer, 0); //Item ???
            WriteDword(writer, 0); //Item ???
            WriteDword(writer, 0); //Item ???

            WriteDword(writer, Player.Inventory.GetItem(1) != null ? Player.Inventory.GetItem(1).Color : 0);
            WriteDword(writer, Player.Inventory.GetItem(3) != null ? Player.Inventory.GetItem(3).Color : 0);
            WriteDword(writer, Player.Inventory.GetItem(4) != null ? Player.Inventory.GetItem(4).Color : 0);
            WriteDword(writer, Player.Inventory.GetItem(5) != null ? Player.Inventory.GetItem(5).Color : 0); WriteDword(writer, 0); //Item ???
            WriteDword(writer, 0); //Item ???
            WriteDword(writer, 0); //Item ???
            WriteDword(writer, 0); //Item ???
        }
    }
}