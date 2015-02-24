using Tera.Communication.Logic;

namespace Tera.Network.Packets.ClientPackets
{
    public class CpCheckName : ARecvPacket
    {
        protected string Name;
        protected short Type;

        public override void Read()
        {
            ReadByte(14);
            Type = (short) ReadWord();
            ReadWord();
            Name = ReadString();
        }

        public override void Process()
        {
            PlayerLogic.CheckName(Connection, Name, Type);
        }
    }
}