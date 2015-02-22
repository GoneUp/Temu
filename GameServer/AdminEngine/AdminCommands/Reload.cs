using Tera.Data.Enums;
using Tera.Data.Interfaces;
using Tera.Network.old_Server;

namespace Tera.AdminEngine.AdminCommands
{
    class Reload : ACommand
    {
        public override void Process(IConnection connection, string msg)
        {
            switch (msg)
            {
                case "skills":
                    Data.Data.LoadSkills();
                    new SpChatMessage("Skills reloaded...", ChatType.Notice).Send(connection);
                    break;

                case "quests":
                    Data.Data.LoadQuests();
                    new SpChatMessage("Quests reloaded...", ChatType.Notice).Send(connection);
                    break;

                case "npctemplates":
                    Data.Data.LoadNpcTemplates();
                    new SpChatMessage("Npc templates reloaded...", ChatType.Notice).Send(connection);
                    break;
            }
        }
    }
}
