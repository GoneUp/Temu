using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpChatInfo : ASendPacket
    {
        protected Player Player;
        protected int Type; // 1 - whisper, friend, block, report; other - whisper, friend, follow

        public SpChatInfo(Player player, int type)
        {
            Player = player;
            Type = type;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, 0); //name start shift
            WriteDword(writer, Type);
            WriteDword(writer, Player.PlayerData.SexRaceClass);
            WriteDword(writer, Player.GetLevel());

            WriteWord(writer, 1);
            WriteWord(writer, 11);

            writer.Seek(4, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            WriteString(writer, Player.PlayerData.Name);
        }
    }
}