using Tera.Data.Enums.Item;

namespace Tera.Network.old_Client
{
    public class RpGetInventory : ARecvPacket
    {
        public override void Read()
        {
            ReadDword(); //1 //mb type
        }

        public override void Process()
        {
            Communication.Global.StorageService.ShowPlayerStorage(Connection.Player, StorageType.Inventory, false);
        }
    }
}