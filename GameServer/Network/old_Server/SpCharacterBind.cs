using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpCharacterBind : ASendPacket //len 21
    {
        protected Player Player;

        public SpCharacterBind(Player player)
        {
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            //TODO:
            WriteDword(writer, Player.Position.MapId);
            WriteSingle(writer, Player.Position.X);
            WriteSingle(writer, Player.Position.Y);
            WriteSingle(writer, Player.Position.Z);
            WriteByte(writer, 0); //NOT Heading
        }
    }
}