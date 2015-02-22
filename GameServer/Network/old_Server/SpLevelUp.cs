using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpLevelUp : ASendPacket
    {
        protected Player Player;

        public SpLevelUp(Player player)
        {
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Player);
            WriteDword(writer, Player.GetLevel());
        }
    }
}