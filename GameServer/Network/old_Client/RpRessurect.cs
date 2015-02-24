using Tera.Communication.Logic;

namespace Tera.Network.old_Client
{
    class RpRessurect : ARecvPacket
    {
        protected int Type;
        protected int Unk;

        public override void Read()
        {
            Type = ReadDword();
            Unk = ReadDword();
        }

        public override void Process()
        {
            PlayerLogic.Ressurect(Connection.Player, Type, Unk);
        }
    }
}
