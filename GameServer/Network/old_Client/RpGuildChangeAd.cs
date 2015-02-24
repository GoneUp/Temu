namespace Tera.Network.old_Client
{
    class RpGuildChangeAd : ARecvPacket
    {
        protected string Ad;

        public override void Read()
        {
            ReadWord();
            Ad = ReadString();
        }

        public override void Process()
        {
            Communication.Global.GuildService.ChangeAd(Connection.Player, Ad);
        }
    }
}
