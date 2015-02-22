using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpPartyMemberPosition : ASendPacket
    {
        public Player Player;

        public SpPartyMemberPosition(Player player)
        {
            Player = player;
        }
        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, 11); // ^(*,..,*)^
            WriteDword(writer, Player.PlayerId);
            WriteSingle(writer, Player.Position.X);
            WriteSingle(writer, Player.Position.Y);
            WriteByte(writer, "00303CC5"); //Z?
            WriteDword(writer, Player.Position.MapId);
            WriteDword(writer, 4);// possible ID of mark
        }
    }
}
