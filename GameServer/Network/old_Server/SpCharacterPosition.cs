using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpCharacterPosition : ASendPacket
    {
        protected Player Player;

        public SpCharacterPosition(Player player)
        {
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Player);
            WriteSingle(writer, Player.Position.X);
            WriteSingle(writer, Player.Position.Y);
            WriteSingle(writer, Player.Position.Z);
            WriteWord(writer, Player.Position.Heading);
            WriteByte(writer, (byte) (Player.LifeStats.IsDead() ? 0 : 1));
        }
    }
}