namespace Tera.Network.old_Client
{
    class RpGuildChangeMotd : ARecvPacket
    {
        protected string NewMotd;

        public override void Read()
        {
            ReadWord();
            NewMotd = ReadString();
        }

        public override void Process()
        {
            Communication.Global.GuildService.ChangeMotd(Connection.Player, NewMotd);
        }
    }
}
