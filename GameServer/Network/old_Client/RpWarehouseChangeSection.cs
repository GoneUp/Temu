using Tera.Data.Enums.Item;

namespace Tera.Network.old_Client
{
    public class RpWarehouseChangeSection : ARecvPacket
    {
        protected int Offset;

        public override void Read()
        {
            ReadDword();
            ReadDword();
            ReadDword();
            Offset = ReadDword();
        }

        public override void Process()
        {
            if(Offset % 72 != 0 || Offset > 216)
                return;

            Connection.Player.PlayerCurrentBankSection = Offset / 72;

            Communication.Global.StorageService.ShowPlayerStorage(Connection.Player, StorageType.CharacterWarehouse, false, Offset);
        }
    }
}
