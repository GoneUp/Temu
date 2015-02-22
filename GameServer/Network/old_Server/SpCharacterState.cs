using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpCharacterState : ASendPacket //len 17
    {
        protected Player Player;

        public SpCharacterState(Player player)
        {
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Player);
            WriteDword(writer, Player.PlayerMode.GetHashCode());
            WriteByte(writer, 0);
        }
    }
}