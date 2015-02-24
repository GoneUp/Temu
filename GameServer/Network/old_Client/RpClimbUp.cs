namespace Tera.Network.old_Client
{
    public class RpClimbUp : ARecvPacket
    {
        protected float X;
        protected float Y;
        protected float Z;

        public override void Read()
        {
            X = Single();
            Y = Single();
            Z = Single();
        }

        public override void Process()
        {
            
        }
    }
}