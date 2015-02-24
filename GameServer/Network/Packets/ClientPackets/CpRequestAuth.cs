using System.Text;

namespace Tera.Network.Packets.ClientPackets
{
    public class CpRequestAuth : ARecvPacket
    {
        protected string AccountName;
        protected string Session;

        public override void Read()
        {
            ReadWord(); //unk1
            ReadWord(); //unk2
            int length = ReadWord();
            ReadByte(5); //unk3
            ReadDword(); //unk4
            AccountName = ReadString(); //AccountName !!! ???
            Session = Encoding.ASCII.GetString(ReadByte(length));
        }

        public override void Process()
        {
            Communication.Logic.AccountLogic.TryAuthorize(Connection, AccountName, Session);
        }
    }
}