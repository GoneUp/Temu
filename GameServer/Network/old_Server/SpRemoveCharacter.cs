using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpRemoveCharacter : ASendPacket
    {
        protected Player Player;

        public SpRemoveCharacter(Player player)
        {
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Player);
            WriteDword(writer, 1); //mb hide timer?
        }
    }
}