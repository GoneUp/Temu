namespace Tera.Network.old_Client
{
    public class RpInventoryUndressItem : ARecvPacket
    {
        protected int From;
        protected int To;

        public override void Read()
        {
            ReadDword(); //Unk
            ReadDword(); //Unk
            From = ReadDword();
            To = ReadDword();
        }

        public override void Process()
        {
            Communication.Global.StorageService.ReplaceItem(Connection.Player, Connection.Player.Inventory, From, To);
        }
    }
}