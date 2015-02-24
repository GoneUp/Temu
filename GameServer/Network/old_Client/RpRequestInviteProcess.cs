namespace Tera.Network.old_Client
{
    public class RpRequestInviteProcess : ARecvPacket
    {
        protected int DialogUid;
        protected string PlayerName;
        protected bool IsAccept;
        protected int Type;

        public override void Read()
        {
            ReadWord(); //name shift
            Type = ReadDword();
            DialogUid = ReadDword();
            ReadDword();
            IsAccept = ReadDword() == 1; //1 - is accept, 2 - decline
            PlayerName = ReadString();
        }

        public override void Process()
        {
            Communication.Global.ActionEngine.ProcessRequest(DialogUid, IsAccept, Connection.Player);
        }
    }
}