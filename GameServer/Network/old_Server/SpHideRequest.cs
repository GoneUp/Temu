using System.IO;
using Tera.Data.Structures.World.Requests;

namespace Tera.Network.old_Server
{
    public class SpHideRequest : ASendPacket
    {
        protected Request Request;

        public SpHideRequest(Request request)
        {
            Request = request;
        }

        public override void Write(BinaryWriter writer)
        {
            // todo places
            WriteUid(writer, Request.Owner);
            WriteUid(writer, Request.Target);
            WriteDword(writer, (int)Request.Type);
            WriteDword(writer, Request.UID);
        }
    }
}