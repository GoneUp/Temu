using Tera.Communication.Logic;

namespace Tera.Network.Packets.ClientPackets
{
    public class CpRequestVersion : ARecvPacket
    {
        protected int[] Version;
       
        public override void Read()
        {
            int count = ReadWord();
            ReadWord(); 

            Version = new int[count];
            for (int i = 0; i < count; i++)
            {
                ReadDword(); //unk
                int tmp = ReadWord();
                if (tmp == 1)
                {
                    ReadWord();
                }

                Version[i] = ReadWord(); //They are diffrent in every game version, butI could not find any relations to known version numerbs.
                //e.g. 2903 - Version1 11403, Version2 15663

                ReadWord();
            }
        }

        public override void Process()
        {
            GlobalLogic.CheckVersion(Connection, Version);
        }
    }
}