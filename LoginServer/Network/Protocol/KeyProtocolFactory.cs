using NetworkApi.Communication.Scs.Communication.Protocols;

namespace Network
{
    public class KeyProtocolFactory : IScsWireProtocolFactory
    {
        public IScsWireProtocol CreateWireProtocol()
        {
            return new KeyProtocol();
        }
    }
}
