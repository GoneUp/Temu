namespace Tera.Network.Packets.ClientPackets
{
    public class CpDeleteCharacter : ARecvPacket
    {
        protected int PlayerIndex;

        public override void Read()
        {
            PlayerIndex = ReadDword();
        }

        public override void Process()
        {
            Communication.Logic.AccountLogic.RemovePlayer(Connection, PlayerIndex);
        }
    }
}