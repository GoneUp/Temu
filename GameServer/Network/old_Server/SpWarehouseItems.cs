using System.Collections.Generic;
using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpWarehouseItems : ASendPacket
    {
        //0565 0200 2C00 A40A0B0000800000 01000000000000000000000001000000020000000000000000000000 2C00 6A00 00000000 BC1B000090EC0800000000005F6502000000000000000000010000000400000004000000000000000000000000000000000000000000 6A00000000000000C01B000092EC0800000000005F6502000000000001000000010000000100000001000000000000000000000000000000000000000000
        protected Player Player;
        protected int Offset;

        public SpWarehouseItems(Player player, int offset)
        {
            Player = player;
            Offset = offset;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, (short)Player.CharacterWarehouse.Items.Count);
            WriteWord(writer, 0);
            WriteUid(writer, Player);
            WriteByte(writer, "0100000000000000");
            WriteDword(writer, Offset);
            WriteDword(writer, 0); // last busy slot
            WriteDword(writer, Player.CharacterWarehouse.Items.Count);
            WriteLong(writer, Player.CharacterWarehouse.Money);

            writer.Seek(6, SeekOrigin.Begin);
            WriteWord(writer, (short)writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            int i = 0;
            int last =  Offset;
            lock (Player.CharacterWarehouse.ItemsLock)
            {
                foreach (KeyValuePair<int, StorageItem> item in Player.CharacterWarehouse.Items)
                {
                    if (item.Key < Offset || item.Key >= Offset + 72)
                        continue;

                    if (item.Key > last)
                        last = item.Key;

                    i++;
                    short s = (short)writer.BaseStream.Length;

                    WriteWord(writer, s);
                    WriteWord(writer, 0); //next shift
                    WriteDword(writer, 0);
                    WriteDword(writer, item.Value.ItemId);
                    WriteUid(writer, item.Value);
                    WriteDword(writer, 0x0002655f);
                    WriteDword(writer, 0);
                    WriteDword(writer, item.Key);
                    WriteDword(writer, 1);
                    WriteDword(writer, 1);
                    WriteDword(writer, item.Value.Amount);
                    WriteByte(writer, new byte[18]);

                    if (Player.CharacterWarehouse.Items.Count > i)
                    {
                        writer.Seek(s + 2, SeekOrigin.Begin);
                        WriteWord(writer, (short)writer.BaseStream.Length);
                        writer.Seek(0, SeekOrigin.End);
                    }
                }
            }

            writer.Seek(24, SeekOrigin.Begin);
            WriteDword(writer, (last + 1 < Offset + 71 && last != Offset) ? last + 1 : last);
            writer.Seek(0, SeekOrigin.End);
        }
    }
}
