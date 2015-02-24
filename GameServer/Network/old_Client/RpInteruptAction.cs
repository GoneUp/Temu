namespace Tera.Network.old_Client
{
    public class RpInteruptAction : ARecvPacket
    {
        protected int Unk1;
        protected int Unk2;
        protected int Unk3;

        public override void Read()
        {
            Unk1 = ReadDword(); // 0
            Unk2 = ReadDword(); // 0
            Unk3 = ReadDword(); // 1
        }

        public override void Process()
        {
            Connection.Player.Controller.Release();
        }
    }
}