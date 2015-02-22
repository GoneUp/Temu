using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpUpdateStamina : ASendPacket
    {
        protected Player Player;

        public SpUpdateStamina(Player player)
        {
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, Player.LifeStats.Stamina); //NowStamina
            WriteDword(writer, 120); //MaxStamina
            WriteWord(writer, 3); //???
        }
    }
}
