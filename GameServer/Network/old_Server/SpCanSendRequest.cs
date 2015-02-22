using System.IO;

namespace Tera.Network.old_Server
{
    public class SpCanSendRequest : ASendPacket
    {
        protected int RequestType;

        public SpCanSendRequest(int requestType)
        {
            RequestType = requestType;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteDword(writer, RequestType);
        }
    }
}