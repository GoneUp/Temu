namespace Tera.Network.old_Client
{
    public class RpInventoryRemoveItem : ARecvPacket
    {
        protected int Slot;
        protected int Counter;
        protected int Unk1;
        protected int Unk2;

        public override void Read()
        {
            Unk1 = ReadDword(); //Unk
            Unk2 = ReadDword(); //Unk
            Slot = ReadDword();
            Counter = ReadDword();
        }

        public override void Process()
        {
            Communication.Global.StorageService.RemoveItem(Connection.Player, Connection.Player.Inventory, Slot, Counter);
        }
    }
}