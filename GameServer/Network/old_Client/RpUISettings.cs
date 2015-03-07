using Tera.Database.DAO;
using Utils;

namespace Tera.Network.old_Client
{
    public class RpUISettings : ARecvPacket
    {
        public byte[] UISettings;

        public override void Read()
        {
            UISettings = ReadByte((int)Reader.BaseStream.Length);
        }

        public override void Process()
        {
            Connection.GameAccount.UiSettings = ByteUtilities.ByteArrayToString(UISettings);
            DAOManager.accountDAO.SaveAccount(Connection.GameAccount);
        }
    }
}