namespace Tera.Network.old_Client
{
    public class RpClimbEnd : ARecvPacket
    {
        protected float X;
        protected float Y;
        protected float Z;
        protected short Heading;

        public override void Read()
        {
            X = Single();
            Y = Single();
            Z = Single();
            Heading = (short) ReadWord();
        }

        public override void Process()
        {
            
        }
    }
}