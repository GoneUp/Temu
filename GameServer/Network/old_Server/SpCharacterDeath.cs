using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpCharacterDeath : ASendPacket
    {
        protected bool IsDeath;
        protected Player Player;

        public SpCharacterDeath(Player player, bool isDeath)
        {
            IsDeath = isDeath;
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Player);
            WriteSingle(writer, Player.Position.X);
            WriteSingle(writer, Player.Position.Y);
            WriteSingle(writer, Player.Position.Z);
            WriteWord(writer, (short) (IsDeath ? 0 : 1));
        }
    }
}