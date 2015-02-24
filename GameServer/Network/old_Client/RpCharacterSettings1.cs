using Utils;

namespace Tera.Network.old_Client
{
    public class RpCharacterSettings1 : ARecvPacket
    {
        protected byte[] Settings;

        public override void Read()
        {
            Settings = ReadByte((int) Reader.BaseStream.Length);
        }

        public override void Process()
        {
            Connection.GameAccount.UiSettings = ByteUtilities.ByteArrayToString(Settings);
        }
    }
}