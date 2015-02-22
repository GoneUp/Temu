using Tera.Communication.Logic;

namespace Tera.Network.Packets.ClientPackets
{
    public class CpRequestCharacterList : ARecvPacket
    {
        public override void Read()
        {
            //Nothing
        }

        public override void Process()
        {
            AccountLogic.GetPlayerList(Connection);
        }
    }
}