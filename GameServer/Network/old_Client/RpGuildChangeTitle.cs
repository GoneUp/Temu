namespace Tera.Network.old_Client
{
    class RpGuildChangeTitle : ARecvPacket
    {
        protected string NewTitle;

        public override void Read()
        {
            ReadWord();
            NewTitle = ReadString();
        }

        public override void Process()
        {
            Communication.Global.GuildService.ChangeTitle(Connection.Player, NewTitle);
        }
    }
}
