namespace Tera.Network.old_Client
{
    class RpServerGuildsPage : ARecvPacket
    {
        protected int TabId;

        public override void Read()
        {
            TabId = ReadDword();
        }

        public override void Process()
        {
            Communication.Global.GuildService.SendServerGuilds(Connection.Player, TabId);
        }
    }
}
