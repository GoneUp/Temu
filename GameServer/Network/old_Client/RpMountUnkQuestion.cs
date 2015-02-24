namespace Tera.Network.old_Client
{
    class RpMountUnkQuestion : ARecvPacket
    {
        protected long Uid;
        protected int Unk2;

        public override void Read()
        {
            Uid = ReadLong();
            Unk2 = ReadDword(); //30
        }

        public override void Process()
        {
            Communication.Global.MountService.UnkQuestion(Connection.Player, Unk2);
        }
    }
}
