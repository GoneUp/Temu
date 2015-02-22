using Tera.Communication.Logic;

namespace Tera.Network.old_Client
{
    class RpAbortRelog : ARecvPacket
    {
        public override void Read()
        {
            //Nothing
        }

        public override void Process()
        {
            AccountLogic.AbortExitAction(Connection);
        }
    }
}
