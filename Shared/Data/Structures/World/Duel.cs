using Tera.Data.Structures.World.Requests;

namespace Tera.Data.Structures.World
{
    public class Duel
    {
        public Player.Player Initiator;
        public Player.Player Initiated;
        public long LastKickUtc;
        public Request Request;
    }
}
