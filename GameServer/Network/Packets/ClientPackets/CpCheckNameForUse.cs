using Tera.Communication.Logic;

namespace Tera.Network.Packets.ClientPackets
{
    public class CpCheckNameForUse : ARecvPacket
    {
        protected short Type;
        protected string Name;

        public override void Read()
        {
            Type = (short) ReadWord();
            Name = ReadString();
        }

        public override void Process()
        {
            PlayerLogic.CheckNameForUse(Connection, Name, Type);
        }
    }
}