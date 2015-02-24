namespace Tera.Network.old_Client
{
    public class RpGetServerGuilds : ARecvPacket
    {
        public override void Read()
        {
            ReadDword();
        }

        public override void Process()
        {
            Communication.Global.GuildService.SendServerGuilds(Connection.Player, 1);
        }
    }
}