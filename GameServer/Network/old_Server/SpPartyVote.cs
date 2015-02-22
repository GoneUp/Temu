using System.IO;

namespace Tera.Network.old_Server
{
    public class SpPartyVote : ASendPacket
    {
        protected long PlayerUid;

        public SpPartyVote(long playerUid)
        {
            PlayerUid = playerUid;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteLong(writer, PlayerUid);
        }
    }
}