namespace Tera.Network.old_Client
{
    public class RpDialogSelect : ARecvPacket
    {
        protected int DialogUid;
        protected int SelectedIndex;

        public override void Read()
        {
            DialogUid = ReadDword();
            SelectedIndex = ReadDword();
            ReadDword(); //FFFFFFFF
            ReadDword(); //FFFFFFFF
        }

        public override void Process()
        {
            Communication.Logic.PlayerLogic.ProgressDialog(Connection.Player, SelectedIndex, DialogUid);
        }
    }
}