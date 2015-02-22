using Tera.Communication.Logic;

namespace Tera.Network.Packets.ClientPackets
{
    public class CpRequestVersion : ARecvPacket
    {
        protected int Version;

        public override void Read()
        {
            int count = ReadH();
            ReadH(); //First shift

            for (int i = 0; i < count; i++)
            {
                ReadD(); //NowShift & NextShift
                ReadD(); //Unk1
                ReadD(); //Unk2
            }
        }

        public override void Process()
        {
            GlobalLogic.CheckVersion(Connection, OpCodes.Version);
        }
    }
}