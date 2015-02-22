using Tera.Communication.Logic;

namespace Tera.Network.old_Client
{
    public class RpUnstuck : ARecvPacket
    {
        public override void Read()
        {
            //Unused 8 bytes
        }

        public override void Process()
        {
            PlayerLogic.Unstuck(Connection);
        }
    }
}
