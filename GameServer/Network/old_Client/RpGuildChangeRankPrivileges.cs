namespace Tera.Network.old_Client
{
    class RpGuildChangeRankPrivileges : ARecvPacket
    {
        public int NewPrivileges;
        protected int RankId;
        public string RankName;

        public override void Read()
        {
            ReadWord(); //unk
            RankId = ReadDword();
            NewPrivileges = ReadDword();
            RankName = ReadString();
        }

        public override void Process()
        {
            Communication.Global.GuildService.ChangeRankPrivileges(Connection.Player, RankId, NewPrivileges, RankName);
        }
    }
}
