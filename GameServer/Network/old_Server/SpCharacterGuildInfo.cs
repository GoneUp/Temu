using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpCharacterGuildInfo : ASendPacket
    {
        public Player Player;
        public string GuildName;
        public string GuildMemberLevel;

        public SpCharacterGuildInfo(Player player, string guildName, string guildMemberLevel)
        {
            Player = player;
            GuildName = guildName;
            GuildMemberLevel = guildMemberLevel;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, 0); //guild name start shift
            WriteDword(writer, 0); //member level start shift
            WriteDword(writer, 0); //member level end shift
            WriteDword(writer, 0); //possible guildlogo shift
            WriteUid(writer, Player);

            writer.Seek(4, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            WriteString(writer, GuildName);

            writer.Seek(6, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            WriteString(writer, GuildMemberLevel);

            writer.Seek(8, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            WriteWord(writer, 0);

            writer.Seek(10, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            WriteWord(writer, 0);
        }
    }
}