using Tera.Communication.Logic;

namespace Tera.Network.old_Client
{
    public class RpEnterWorld : ARecvPacket
    {
        public override void Read()
        {
            //Nothing
        }

        public override void Process()
        {
            PlayerLogic.PlayerEnterWorld(Connection.Player);
        }
    }
}