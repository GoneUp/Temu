namespace Tera.Network.old_Client
{
    public class RpInventoryReplaceItem : ARecvPacket
    {
        protected int From;
        protected int To;
        protected int Unk1;
        protected int Unk2;

        public override void Read()
        {
            Unk1 = ReadDword(); //Unk
            Unk2 = ReadDword(); //Unk
            From = ReadDword();
            To = ReadDword();
        }

        public override void Process()
        {
            Communication.Global.StorageService.ReplaceItem(Connection.Player, Connection.Player.Inventory, From, To);
        }
    }
}