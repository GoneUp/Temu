namespace Tera.Network.old_Client
{
    public class RpPartyVote : ARecvPacket
    {
        protected int PlayerId;
        protected byte Result;

        public override void Read()
        {
            PlayerId = ReadD();
            Result = (byte) ReadC();
        }

        public override void Process()
        {
            
        }
    }
}