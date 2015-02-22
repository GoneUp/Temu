using System.IO;
using Tera.Data.Enums;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpChatMessage : ASendPacket
    {
        protected Player Player;
        protected string Message;
        protected ChatType Type;

        public SpChatMessage(Player player, string message, ChatType type)
        {
            Player = player;
            Message = message;
            Type = type;
        }

        public SpChatMessage(string message, ChatType type)
        {
            Player = null;
            Message = " " + message;
            Type = type;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, 0); //Sender shift
            WriteWord(writer, 0); //Message shift
            WriteDword(writer, Type.GetHashCode());

            WriteUid(writer, Player);

            byte isGm = 0;

            if (Player != null)
            {
                if (Player.Connection.GameAccount.IsGM == true)
                { isGm = (byte)1; }
                else
                { isGm = (byte)0; }
            }

            WriteByte(writer, 0); //Blue shit
            WriteByte(writer, isGm); //GM

            if (Player != null)
            {
                writer.Seek(4, SeekOrigin.Begin);
                WriteWord(writer, (short) writer.BaseStream.Length);
                writer.Seek(0, SeekOrigin.End);

                WriteString(writer, Player.PlayerData.Name);
            }

            writer.Seek(6, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            WriteString(writer, Message);

            WriteByte(writer, 0);
        }
    }
}