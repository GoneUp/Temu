using Tera.Communication.Logic;
using Tera.Data.Structures;
using Tera.Data.Structures.World;

namespace Tera.Network.old_Client
{
    public class RpItemPickUp : ARecvPacket
    {
        protected long ItemUid;

        public override void Read()
        {
            ItemUid = ReadQ();
        }

        public override void Process()
        {
            Item item = TeraObject.GetObject(ItemUid) as Item;

            if (item != null)
                PlayerLogic.PickUpItem(Connection, item);
        }
    }
}