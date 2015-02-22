using Hik.Communication.Scs.Communication.Messages;

namespace Tera.Network.Messages
{
    public class GameMessage : ScsMessage
    {
        public short OpCode;

        public byte[] Data;
    }
}