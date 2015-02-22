using Tera.Data.Structures;
using Tera.Data.Structures.Npc;
using Tera.Data.Structures.Player;
using Tera.Data.Structures.World;

namespace Tera.Communication.Interfaces
{
    public interface IMapService : IComponent
    {
        void Init();
        void SpawnMap(MapInstance instance);
        void SpawnTeraObject(TeraObject obj, MapInstance instance);
        void DespawnTeraObject(TeraObject obj);
        void PlayerEnterWorld(Player player);
        void CreateDrop(Npc npc, Player player);
        void PickUpItem(Player player, Item item);
        void PlayerLeaveWorld(Player player);

        bool IsDungeon(int mapId);
    }
}