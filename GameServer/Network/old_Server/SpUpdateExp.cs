using System.IO;
using Tera.Data.Structures.Npc;
using Tera.Data.Structures.Player;

namespace Tera.Network.old_Server
{
    public class SpUpdateExp : ASendPacket
    {
        protected Player Player;
        protected long Added;
        protected Npc Npc;

        public SpUpdateExp(Player player, long added, Npc npc = null)
        {
            Player = player;
            Added = added;
            Npc = npc;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteLong(writer, Added);
            WriteLong(writer, Player.PlayerExp);
            WriteLong(writer, Player.GetExpShown());
            WriteLong(writer, Player.GetExpNeed());
            WriteUid(writer, Npc);

            /*new EXP params like death penalty or something else*/

            WriteLong(writer, 0);
            WriteDword(writer, 0);
            WriteByte(writer, "461600000000803F00000000");
        }
    }
}