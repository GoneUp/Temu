using Tera.Network.old_Server;
using Utils;

namespace Tera.Network.old_Client
{
    public class RpTeleportUnk : ARecvPacket
    {
        protected byte[] ThreeInt;

        public override void Read()
        {
            ThreeInt = ReadB(12);
        }

        public override void Process()
        {
            new SendPacket("09B000" + ThreeInt.ToHex()).Send(Connection);
        }
    }
}