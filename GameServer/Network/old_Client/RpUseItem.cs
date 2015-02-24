using Tera.Communication;
using Tera.Data.Structures.World;

namespace Tera.Network.old_Client
{
    public class RpUseItem : ARecvPacket
    {
        protected long PlayerUid;
        protected int ItemId;
        protected WorldPosition Position = new WorldPosition();
        protected byte[] Unk;

        public override void Read()
        {
            PlayerUid = ReadLong();
            ItemId = ReadDword();
            Unk = ReadByte(28); //unk, zeros
            Position.X = Single();
            Position.Y = Single();
            Position.Z = Single();
            Position.Heading = (short)ReadWord(); //think, that this is item slot
        }

        public override void Process()
        {
            Global.ItemService.ItemUse(Connection.Player, ItemId, Position);
        }
    }
}