using Tera.Communication.Logic;

namespace Tera.Network.old_Client
{
    public class RpRelog : ARecvPacket
    {
        public override void Read()
        {
            //empty packet
        }

        public override void Process()
        {
            AccountLogic.RelogPlayer(Connection);
        }
    }
}