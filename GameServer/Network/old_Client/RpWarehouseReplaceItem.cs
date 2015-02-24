using Tera.Data.Enums.Item;

namespace Tera.Network.old_Client
{
    public class RpWarehouseReplaceItem : ARecvPacket
    {
        protected int From;
        protected int To;
        protected int Offset;

        public override void Read()
        {
            ReadDword(); // 1
            ReadDword(); // 3
            ReadDword(); // 1
            ReadDword();
            ReadDword();
            From = ReadDword();
            To = ReadDword();
            Offset = ReadDword();
        }

        public override void Process()
         {
             if (Offset < 72)
                 Offset = 0;
             else if (Offset < 144)
                 Offset = 72;
             else if (Offset < 216)
                 Offset = 144;
             else
                 Offset = 216;

            Communication.Global.StorageService.ReplaceItem(Connection.Player, Connection.Player.CharacterWarehouse, From, To, false, false);
            Communication.Global.StorageService.ShowPlayerStorage(Connection.Player, StorageType.CharacterWarehouse, false, Offset);
        }
    }
}
