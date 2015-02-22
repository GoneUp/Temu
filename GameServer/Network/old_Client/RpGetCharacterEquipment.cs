using Tera.Network.old_Server;

namespace Tera.Network.old_Client
{
    public class RpGetCharacterEquipment : ARecvPacket
    {
        public override void Read()
        {
            //nothing
        }

        public override void Process()
        {
            new SendPacket("92C6").Send(Connection);
        }
    }
}