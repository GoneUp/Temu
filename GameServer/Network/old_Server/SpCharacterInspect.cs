using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpCharacterInspect : ASendPacket
    {
        protected Player Player;

        public SpCharacterInspect(Player player)
        {
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, 5);
            WriteWord(writer, 0); // Items shift
            
            WriteDword(writer, 0);

            WriteWord(writer, 0); // Name shift
            WriteWord(writer, 0); // Name end
            // Unk shifts
            WriteWord(writer, 0); // Name end + 2
            WriteWord(writer, 0); // Name end + 4

            WriteDword(writer, Player.PlayerData.SexRaceClass);
            WriteUid(writer, Player);

            WriteLong(writer, 0x6f3);
            WriteDword(writer, 0x46);

            WriteWord(writer, (short)Player.PlayerLevel);
            WriteGatherStats(writer, Player);

            WriteWord(writer, 2);
            WriteWord(writer, 0x40d1);
            WriteDword(writer, 0);
            WriteWord(writer, 0);

            // Experience
            WriteLong(writer, Player.GetExpShown());
            WriteLong(writer, Player.GetExpNeed());

            WriteDword(writer, 0);
            WriteDword(writer, 0x90b202);
            WriteDword(writer, 0x501403);
            WriteDword(writer, 0xa0f3a0);

            WriteDword(writer, 0x4533);
            WriteDword(writer, 0);
            WriteDword(writer, 0);

            WriteStats(writer, Player);

            WriteWord(writer, 3);
            WriteDword(writer, 0x76);
            WriteDword(writer, 0x78);

            // Levels
            WriteDword(writer, 1); // Item level
            WriteDword(writer, 1); // Inventory level

            WriteByte(writer, new byte[14]);

            // Name
            writer.Seek(12, SeekOrigin.Begin);
            WriteWord(writer, (short)writer.BaseStream.Length); // Name shift
            writer.Seek(0, SeekOrigin.End);

            WriteString(writer, Player.PlayerData.Name);

            writer.Seek(14, SeekOrigin.Begin);
            WriteWord(writer, (short)writer.BaseStream.Length); // Name end
            writer.Seek(0, SeekOrigin.End);

            WriteWord(writer, 0);
            writer.Seek(16, SeekOrigin.Begin);
            WriteWord(writer, (short)writer.BaseStream.Length); // Unk1 shift
            writer.Seek(0, SeekOrigin.End);

            WriteWord(writer, 0);
            writer.Seek(18, SeekOrigin.Begin);
            WriteWord(writer, (short)writer.BaseStream.Length); // Unk2 shift
            writer.Seek(0, SeekOrigin.End);

            WriteWord(writer, 0);
            writer.Seek(6, SeekOrigin.Begin);
            WriteWord(writer, (short)writer.BaseStream.Length); // Items shift
            writer.Seek(0, SeekOrigin.End);

            short lastShift = -1;
            lock(Player.Inventory.ItemsLock)
                for (int i = 0; i < 20; i++)
                    if (Player.Inventory.Items.ContainsKey(i))
                    {
                        // this shift
                        WriteWord(writer, (short)writer.BaseStream.Length);
                        lastShift = (short)writer.BaseStream.Length;
                        // next item shift
                        WriteWord(writer, 0);

                        WriteDword(writer, Player.Inventory.Items[i].ItemId);

                        WriteUid(writer, Player.Inventory.Items[i]);

                        WriteDword(writer, 0x6f3);
                        WriteDword(writer, 0);

                        WriteDword(writer, i);
                        WriteDword(writer, 0);
                        WriteDword(writer, Player.Inventory.Items[i].ItemTemplate.Level);
                        WriteByte(writer, new byte[94]);

                        writer.Seek(lastShift, SeekOrigin.Begin);
                        WriteWord(writer, (short)writer.BaseStream.Length);
                        writer.Seek(0, SeekOrigin.End);
                    }

            if (lastShift == -1)
                return;

            writer.Seek(lastShift, SeekOrigin.Begin);
            WriteWord(writer, 0);
            writer.Seek(0, SeekOrigin.End);
        }
    }
}
