using System.IO;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpPegasusFlight : ASendPacket
    {
        protected Player Player;
        protected int TeleportId;
        protected int State;
        protected int Time;

        public SpPegasusFlight(Player player, int teleportId, int state, int time)
        {
            Player = player;
            TeleportId = teleportId;
            State = state;
            Time = time;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Player);
            WriteDword(writer, TeleportId);
            WriteDword(writer, State);
            WriteDword(writer, Time);
        }
    }
}