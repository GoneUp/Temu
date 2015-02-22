using Tera.Data.Structures.World;

namespace Tera.Communication.Interfaces
{
    public interface IGeoService : IComponent
    {
        void Init();
        void FixZ(WorldPosition position);
        void FixOffset(int mapId, float x, float y, float z);
    }
}