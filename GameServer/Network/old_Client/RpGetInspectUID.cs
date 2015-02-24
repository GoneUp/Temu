using Tera.Data.Structures.Player;
using Tera.Network.old_Server;

namespace Tera.Network.old_Client
{
    public class RpGetInspectUid : ARecvPacket
    {
        protected string CharacterName;

        public override void Read()
        {
            ReadWord(); // 6 Why is it here?
            CharacterName = ReadString();
        }

        public override void Process()
        {
            Player player = Communication.Global.PlayerService.GetPlayerByName(CharacterName);

            if(player == null)
                return;

            new SpInspectUid(player).Send(Connection);
        }
    }
}
