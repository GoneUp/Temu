namespace Tera.Network.old_Client
{
    public class RpRequestAccept : ARecvPacket
    {
        protected int Uid;
        protected int Type;

        public override void Read()
        {
            Type = ReadDword();
            Uid = ReadDword();
        }

        public override void Process()
        {
            Communication.Global.ActionEngine.ProcessRequest(Uid, true, Connection.Player);
        }
    }
}