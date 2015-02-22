using Tera.Communication.Logic;

namespace Tera.Network.old_Client
{
    class RpGetBindPoint : ARecvPacket
    {
        public override void Read()
        {
            //Nothing
        }

        public override void Process()
        {
            PlayerLogic.SendBindPoint(Connection);
        }
    }
}
