using Tera.Data.Enums.Player;
using Tera.Data.Structures.Player;

namespace Tera.Communication.Interfaces
{
    public interface IRelationService : IComponent
    {
        PlayerRelation GetRelation(Player asker, Player asked);
        void ResendRelation(Player player);
    }
}
