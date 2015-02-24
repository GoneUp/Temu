
namespace Tera.Network.old_Client
{
    class RpGuildChangeMemberRank : ARecvPacket
    {
        protected int MemberId;
        protected int NewRank;

        public override void Read()
        {
            MemberId = ReadDword();
            NewRank = ReadDword();
        }

        public override void Process()
        {
            Communication.Global.GuildService.ChangeMemberRank(Connection.Player, MemberId, NewRank);
        }
    }
}
