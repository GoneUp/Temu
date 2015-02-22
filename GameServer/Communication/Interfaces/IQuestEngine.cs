using System;
using System.Collections.Generic;
using Tera.Data.Structures.Npc;
using Tera.Data.Structures.Player;
using Tera.Data.Structures.Quest;
using Tera.Data.Structures.World;
using Tera.Data.Structures.World.Continent;

namespace Tera.Communication.Interfaces
{
    public interface IQuestEngine : IComponent
    {
        void Init();
        void ResendQuestData(Player player);
        void PlayerLevelUp(Player player);
        void ShowIcon(Player player, Npc npc, bool force = false);
        void AddButtonsToDialog(Player player, Npc npc, List<DialogButton> buttons);
        void ProcessDialog(Object dialogController, int questId, int questStep);
        QuestReward GetRewardForPlayer(Player player, Quest quest);
        void OnPlayerKillNpc(Player player, Npc npc);
        void OnPlayerLearnSkill(Player player);
        void OnPlayerEnterZone(Player player, Section section);
    }
}