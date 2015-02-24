namespace Tera.Network.old_Client
{
    public class RpFriendAdd : ARecvPacket
    {
        protected string Name;

        public override void Read()
        {
            ReadWord(); //shift?
            Name = ReadString();
        }

        public override void Process()
        {
            
        }
    }
}