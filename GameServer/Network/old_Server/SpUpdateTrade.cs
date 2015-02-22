using System.IO;
using Tera.Data.Structures.Player;
using Tera.Data.Structures.World;

namespace Tera.Network.old_Server
{
    public class SpUpdateTrade : ASendPacket
    {
        protected ShoppingCart ShoppingCart;
        protected Player Player;

        public SpUpdateTrade(Player player, ShoppingCart shoppingCart)
        {
            ShoppingCart = shoppingCart;
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, (short) ShoppingCart.BuyItems.Count);
            short buyItemShift = (short) writer.BaseStream.Position;
            WriteWord(writer, 0); //first buyitem shift

            WriteWord(writer, (short) ShoppingCart.SellItems.Count);
            short sellItemShift = (short) writer.BaseStream.Position;
            WriteWord(writer, 0); //first sellitem shift

            WriteUid(writer, Player);
            WriteDword(writer, ShoppingCart.Uid);
            WriteLong(writer, 0);
            WriteLong(writer, ShoppingCart.GetBuyItemsPrice());
            WriteByte(writer, "9A9999999999A93F"); // shit from traidlist
            WriteLong(writer, ShoppingCart.GetSellItemsPrice());

            foreach (var item in ShoppingCart.BuyItems)
            {
                writer.Seek(buyItemShift, SeekOrigin.Begin);
                WriteWord(writer, (short) writer.BaseStream.Length);
                writer.Seek(0, SeekOrigin.End);

                WriteWord(writer, (short) writer.BaseStream.Position);
                buyItemShift = (short) writer.BaseStream.Position;
                WriteWord(writer, 0);

                WriteDword(writer, item.ItemTemplate.Id);
                WriteDword(writer, item.Count);
            }

            foreach (var item in ShoppingCart.SellItems)
            {
                writer.Seek(sellItemShift, SeekOrigin.Begin);
                WriteWord(writer, (short) writer.BaseStream.Length);
                writer.Seek(0, SeekOrigin.End);

                WriteWord(writer, (short) (writer.BaseStream.Length));
                sellItemShift = (short) writer.BaseStream.Position;
                WriteWord(writer, 0);

                WriteDword(writer, item.ItemTemplate.Id);
                WriteDword(writer, item.Count);
                WriteDword(writer, 0);
                WriteDword(writer, 0);
                WriteWord(writer, 0);
            }
        }
    }
}