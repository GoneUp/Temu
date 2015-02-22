using Tera.Data.Structures.Player;
using Tera.Data.Structures.Quest.Tasks;
using Tera.Network.old_Server;

namespace Tera.QuestEngine.Steps
{
    class QStepPlayCinematic : QStepDefault
    {
        protected QTaskPlaybackVideo  QTaskPlaybackVideo;
 
        public QStepPlayCinematic(QTaskPlaybackVideo qTaskPlaybackVideo)
        {
            QTaskPlaybackVideo = qTaskPlaybackVideo;
        }

        public override void Init(Player player)
        {
            base.Init(player);
            new SpQuestMovie(QTaskPlaybackVideo.MovieId).Send(player.Connection);
            Processor.FinishStep(player);
        }
    }
}
