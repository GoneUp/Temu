using Tera.Communication.Logic;
using Tera.Data.Enums;
using Tera.Data.Structures.Player;

namespace Tera.Network.Packets.ClientPackets
{
    public class CpCreateCharacter : ARecvPacket
    {
        protected PlayerData PlayerData = new PlayerData();

        public override void Read()
        {
            //shifts
            short nameShift = (short) ReadWord();
            short detailsShift = (short) ReadWord();
            short detailsLength = (short) ReadWord();

            PlayerData.Gender = (Gender) ReadDword();
            PlayerData.Race = (Race) ReadDword();
            PlayerData.Class = (PlayerClass) ReadDword();
            PlayerData.Data = ReadByte(8);
            ReadByte();
            PlayerData.Name = ReadString();
            PlayerData.Details = ReadByte(detailsLength);
        }

        public override void Process()
        {
            PlayerLogic.CreateCharacter(Connection, PlayerData);
        }
    }
}