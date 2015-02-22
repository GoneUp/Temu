using Tera.Network.old_Server;

namespace Tera.Network.old_Client
{
    public class RpInspectUnk : ARecvPacket
    {
        public override void Read()
        { }

        public override void Process()
        {
            new SpInspectUnk().Send(Connection);
        }
    }
}
