namespace Tera.Network.old_Client
{
    class RpGuildChangeLeader : ARecvPacket
    {
        protected string NewLeaderName;

        public override void Read()
        {
            ReadWord();
            NewLeaderName = ReadString();
        }

        public override void Process()
        {
            Communication.Global.GuildService.ChangeGuildLeader(Connection.Player, NewLeaderName);
        }
    }
}
