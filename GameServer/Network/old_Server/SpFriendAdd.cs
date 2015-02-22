using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpFriendAdd : ASendPacket
    {
        protected Player Friend;

        public SpFriendAdd(Player friend)
        {
            Friend = friend;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, 0); //name shift
            WriteDword(writer, Friend.PlayerId);
            WriteDword(writer, Friend.GetLevel());
            WriteDword(writer, Friend.PlayerData.Race.GetHashCode());
            WriteDword(writer, Friend.PlayerData.Class.GetHashCode());
            WriteByte(writer, "0100000001000000020000000700000000");

            writer.Seek(4, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            WriteString(writer, Friend.PlayerData.Name);
        }
    }
}