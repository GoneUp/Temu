namespace Tera.Network.old_Client
{
    public class RpGuildRemoveMember : ARecvPacket
    {
        protected string Name;

        public override void Read()
        {
            ReadWord();
            Name = ReadString();
        }

        public override void Process()
        {
            Communication.Global.GuildService.KickMember(Connection.Player, Name);
        }
    }
}