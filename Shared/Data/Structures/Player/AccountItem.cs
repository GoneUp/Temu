using Tera.Data.Structures.Template.Item;

namespace Tera.Data.Structures.Player
{
    [ProtoBuf.ProtoContract]
    public class AccountItem : TeraObject
    {
        public int ItemId;
        public int Options;

    }
}