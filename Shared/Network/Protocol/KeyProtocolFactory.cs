using Hik.Communication.Scs.Communication.Protocols;

namespace Tera.Network.Protocol
{
    public class KeyProtocolFactory : IScsWireProtocolFactory
    {
        public IScsWireProtocol CreateWireProtocol()
        {
            return new KeyProtocol();
        }
    }
}
