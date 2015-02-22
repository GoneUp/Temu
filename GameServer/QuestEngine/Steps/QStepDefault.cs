using System.Collections.Generic;
using Tera.Controllers;
using Tera.Data.Structures.Npc;
using Tera.Data.Structures.Player;
using Tera.Data.Structures.Quest;
using Tera.Data.Structures.World.Continent;

namespace Tera.QuestEngine.Steps
{
    class QStepDefault
    {
        public Quest Quest;

        public QuestProcessor Processor;

        public virtual void Init(Player player)
        {
            QuestData questData = player.Quests[Quest.QuestId];
            questData.Counters = new List<int>(1) {0};
        }

        public virtual void OnKillNpc(Player player, Npc npc)
        {

        }

        public virtual void OnNewSkillLearned(Player player)
        {
            
        }

        public virtual void OnEnterZone(Player player, Section section)
        {

        }

        public virtual List<int> GetParticipantVillagers(Player player)
        {
            return new List<int>();
        }

        public virtual List<int> GetParticipantMonsters(Player player)
        {
            return new List<int>();
        }

        public virtual void ProcessTalk(Player player, DialogController dialog, bool isReward)
        {
            
        }

        public virtual bool IsCountersComplete(Player player)
        {
            return true;
        }
    }
}
