using System.IO;
using Tera.Data.Structures.Player;
using Tera.Data.Structures.World;

namespace Tera.Network.old_Server
{
    public class SpClimb : ASendPacket
    {
        protected Player Player;
        protected Climb Climb;

        public SpClimb(Player player, Climb climb)
        {
            Player = player;
            Climb = climb;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Player);

            WriteSingle(writer, Climb.X1);
            WriteSingle(writer, Climb.Y1);
            WriteSingle(writer, Climb.Z1);

            WriteWord(writer, Climb.Heading);

            WriteSingle(writer, Climb.X2);
            WriteSingle(writer, Climb.Y2);
            WriteSingle(writer, Climb.Z2);

            WriteByte(writer, 0);
        }
    }
}