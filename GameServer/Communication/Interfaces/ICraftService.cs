using Tera.Data.Enums.Craft;
using Tera.Data.Structures.Player;

namespace Tera.Communication.Interfaces
{
    public interface ICraftService : IComponent
    {
        void ProcessCraft(Player player, int recipeId);
        void UpdateCraftRecipes(Player player);
        void AddRecipe(Player player, int recipeId);
        void UpdateCraftStats(Player player);
        void ProgressCraftStat(Player player, CraftStat craftStat);
        void InitCraft(Player player, CraftStat craftStat);

    }
}
