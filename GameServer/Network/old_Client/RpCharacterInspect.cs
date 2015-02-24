using Tera.Data.Structures;
using Tera.Data.Structures.Player;
using Tera.Network.old_Server;

namespace Tera.Network.old_Client
{
    public class RpCharacterInspect : ARecvPacket
    {
        protected long CharacterId;

        public override void Read()
        {
            CharacterId = ReadLong();
        }

        public override void Process()
        {
            Player player = (Player)Uid.GetObject(CharacterId);

            if(player== null)
                return;

            new SpCharacterInspect(player).Send(Connection);
        }
    }
}