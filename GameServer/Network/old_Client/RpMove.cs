using Tera.Communication.Logic;
using Tera.Data.Enums.Player;

namespace Tera.Network.old_Client
{
    public class RpMove : ARecvPacket
    {
        public float X1;
        public float Y1;
        public float Z1;
        protected short Heading;

        protected float X2;
        protected float Y2;
        protected float Z2;

        protected PlayerMoveType MoveType;
        protected short Unk2;
        protected short Unk3;

        public override void Read()
        {
            X1 = Single();
            Y1 = Single();
            Z1 = Single();
            Heading = (short) ReadWord();

            X2 = Single();
            Y2 = Single();
            Z2 = Single();

            MoveType = (PlayerMoveType)ReadWord();
            Unk2 = (short) ReadWord();
            Unk3 = (short) ReadWord();
        }

        public override void Process()
        {
            PlayerLogic.PlayerMoved(Connection.Player, X1, Y1, Z1, Heading, X2, Y2, Z2, MoveType, Unk2, Unk3);
        }
    }
}