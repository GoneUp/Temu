using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpPartyRemoveMember : ASendPacket
    {
        protected Player Player;

        public SpPartyRemoveMember(Player player)
        {
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteWord(writer, 0); //name shift
            WriteDword(writer, 11);
            WriteDword(writer, Player.PlayerId);

            writer.Seek(4, SeekOrigin.Begin);
            WriteWord(writer, (short) writer.BaseStream.Length);
            writer.Seek(0, SeekOrigin.End);

            WriteString(writer, Player.PlayerData.Name);
        }
    }
}