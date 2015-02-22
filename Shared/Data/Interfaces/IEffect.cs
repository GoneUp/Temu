using Tera.Data.Structures.Creature;

namespace Tera.Data.Interfaces
{
    public interface IEffect
    {
        void Action();
        void SetImpact(CreatureEffectsImpact impact);
        void Release();
    }
}