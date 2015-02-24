using Tera.Communication.Logic;

namespace Tera.Network.old_Client
{
    class RpZoneSwitchContinent : ARecvPacket
    {
        protected int ContinentId;

        public override void Read()
        {
            ReadDword(); //Unk1
            ContinentId = ReadDword();
            //ReadDword(); //Unk2 0xFFFFFFFF
        }

        public override void Process()
        {
            if (Connection.Player == null)
                return;

            PlayerLogic.SwitchContinent(Connection.Player, ContinentId);
        }
    }
}
