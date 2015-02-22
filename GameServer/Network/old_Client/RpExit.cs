using Tera.Communication.Logic;

namespace Tera.Network.old_Client
{
    class RpExit : ARecvPacket
    {
        public override void Read()
        {
            //Nothing
        }

        public override void Process()
        {
            AccountLogic.ExitPlayer(Connection);
        }
    }
}