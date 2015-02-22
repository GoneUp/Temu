using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpCharacterCraftStats : ASendPacket
    {
        protected Player Player;

        public SpCharacterCraftStats(Player player)
        {
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, 6); // craft skill counter?
            WriteWord(writer, 0); //first skill shift
            WriteDword(writer, 0);
            WriteWord(writer, 1);
            WriteWord(writer, 0); //craft skills end shift
            WriteByte(writer, "009A0100000D000000");

            writer.Seek(6, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            int i = 0;
            foreach (var skill in Player.PlayerCraftStats.CraftSkillCollection)
            {
                i++;

                short position = (short) writer.BaseStream.Length;

                WriteWord(writer, position);
                WriteWord(writer, 0); //next skill shift
                WriteDword(writer, skill.Key.GetHashCode());
                WriteDword(writer, skill.Key.GetHashCode());
                WriteDword(writer, skill.Value);

                if (i < Player.PlayerCraftStats.CraftSkillCollection.Count)
                {
                    writer.Seek(position + 2, SeekOrigin.Begin);
                    WriteWord(writer, (short) writer.BaseStream.Length);
                    writer.Seek(0, SeekOrigin.End);
                }
            }

            i = 0;
            writer.Seek(14, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            foreach (var extractSkill in Player.PlayerCraftStats.ExtractSkillCollection)
            {
                i++;

                short position = (short) writer.BaseStream.Length;

                WriteWord(writer, position);
                WriteWord(writer, 0); //next skill shift
                WriteDword(writer, extractSkill.Key.GetHashCode());
                WriteDword(writer, extractSkill.Key.GetHashCode());
                WriteDword(writer, extractSkill.Value);

                if (i < Player.PlayerCraftStats.ExtractSkillCollection.Count)
                {
                    writer.Seek(position + 2, SeekOrigin.Begin);
                    WriteWord(writer, (short) writer.BaseStream.Length);
                    writer.Seek(0, SeekOrigin.End);
                }
            }
        }

        //46A0    0600 1900 00000000 0100 7900 009A0100000D000000    1900 2900 010000000100000002000000    2900 3900 020000000200000003000000    3900 4900 030000000300000004000000   4900 5900 040000000400000005000000   5900 6900 050000000500000006000000   6900 0000 060000000600000007000000 7900 0000 0D0000000D00000008000000
    }
}