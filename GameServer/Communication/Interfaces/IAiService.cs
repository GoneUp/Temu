using Tera.Data.Interfaces;
using Tera.Data.Structures.Creature;

namespace Tera.Communication.Interfaces
{
    public interface IAiService : IComponent
    {
        IAi CreateAi(Creature creature);
    }
}