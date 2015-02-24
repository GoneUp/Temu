namespace Tera.Network.old_Client
{
    public class RpZoneChange : ARecvPacket
    {
        public byte[] ZoneDatas;
        public override void Read()
        {
            ZoneDatas = ReadByte(12);
        }

        public override void Process()
        {
            Communication.Logic.PlayerLogic.PlayerEnterZone(Connection.Player, ZoneDatas);
        }
    }
}