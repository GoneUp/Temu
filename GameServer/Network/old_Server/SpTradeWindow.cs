using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpTradeWindow : ASendPacket
    {
        protected Player Player1;
        protected Player Player2;
        protected Storage Storage1;
        protected Storage Storage2;
        protected int UID;

        public SpTradeWindow(Player me, Player other, Storage mine, Storage their, int uid)
        {
            Player1 = me;
            Player2 = other;
            Storage1 = mine;
            Storage2 = their;
            UID = uid;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, 0); //my items start shift
            WriteWord(writer, 0); //my items lenth
            WriteWord(writer, 0); //other player items start shift
            WriteWord(writer, 0); //other player items lenth

            WriteUid(writer, Player1);
            WriteUid(writer, Player2);

            WriteDword(writer, UID);
            WriteDword(writer, Storage1.Locked ? 1 : 0); // is my locked
            WriteLong(writer, Storage1.Money); // my money
            WriteDword(writer, Storage2.Locked ? 1 : 0); // is thier locked
            WriteLong(writer, Storage2.Money); // their money

            short len = (short) writer.BaseStream.Length;

            writer.Seek(4, SeekOrigin.Begin);
            WriteWord(writer, len);
            writer.Seek(0, SeekOrigin.End);

            lock (Storage1.ItemsLock)
            {
                foreach (StorageItem item in Storage1.Items.Values)
                {
                    WriteDword(writer, 0);
                    WriteDword(writer, item.ItemTemplate.Id);
                    WriteDword(writer, item.Amount);
                    WriteUid(writer, item);
                    WriteDword(writer, 0);
                    WriteByte(writer, 0);
                }
            }

            writer.Seek(6, SeekOrigin.Begin);
            WriteWord(writer, (short) (writer.BaseStream.Length - len));
            writer.Seek(0, SeekOrigin.End);

            len = (short) writer.BaseStream.Length;

            writer.Seek(8, SeekOrigin.Begin);
            WriteWord(writer, len);
            writer.Seek(0, SeekOrigin.End);

            lock (Storage2.ItemsLock)
            {
                foreach (StorageItem item in Storage2.Items.Values)
                {
                    WriteDword(writer, 0);
                    WriteDword(writer, item.ItemTemplate.Id);
                    WriteDword(writer, item.Amount);
                    WriteUid(writer, item);
                    WriteDword(writer, 0);
                    WriteByte(writer, 0);
                }
            }

            writer.Seek(10, SeekOrigin.Begin);
            WriteWord(writer, (short) (writer.BaseStream.Length - len));
            writer.Seek(0, SeekOrigin.End);
        }
    }
}