using Tera.Data.Structures.Player;

namespace Tera.Communication.Interfaces
{
    public interface IEmotionService : IComponent
    {
        void StartEmotion(Player player, int emoteId);
    }
}
