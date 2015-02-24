using Tera.Communication.Logic;

namespace Tera.Network.old_Client
{
    public class RpPlay : ARecvPacket
    {
        protected int PlayerId;
        public bool IsProlog;

        public override void Read()
        {
            PlayerId = ReadDword();
            IsProlog = ReadByte() == 1;
        }

        public override void Process()
        {
            PlayerLogic.PlayerSelected(Connection, PlayerId, IsProlog);
        }
    }
}