using Tera.Data.Structures.Player;
using Tera.Data.Structures.World;
using Tera.Data.Structures.World.Continent;

namespace Tera.Communication.Interfaces
{
    public interface IAreaService : IComponent
    {
        void Init();
        Area GetPlayerArea(Player player);
        Area GetAreaByCoords(WorldPosition coords);
        Section GetCurrentSection(Player player);
        Section GetSectionByCoords(WorldPosition coords);
    }
}
