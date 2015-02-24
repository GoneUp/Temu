namespace Tera.Network.old_Client
{
    public class RpGuildInvite : ARecvPacket //6A2B
    {
        protected string InvitedName;
        protected string Message;
        protected byte[] Unk;

        public override void Read()
        {
            Unk = ReadByte(4);
            InvitedName = ReadString();
            Message = ReadString();
        }

        public override void Process()
        {
            
        }
    }
}