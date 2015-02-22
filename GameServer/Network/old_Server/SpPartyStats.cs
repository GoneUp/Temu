using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpPartyStats : ASendPacket
    {
        protected Player Player;

        public SpPartyStats(Player player)
        {
            Player = player;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, 11);
            WriteDword(writer, Player.PlayerId);
            WriteDword(writer, Player.LifeStats.Hp);
            WriteDword(writer, Player.LifeStats.Mp);
            WriteDword(writer, Player.MaxHp);
            WriteDword(writer, Player.MaxMp);
            WriteDword(writer, Player.GetLevel());
            WriteByte(writer, "04000178000000000000000000000000000000");
        }
    }
}