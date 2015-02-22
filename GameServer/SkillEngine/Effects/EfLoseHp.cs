using Tera.Communication.Logic;
using Tera.Data.Enums;
using Tera.Data.Structures.Player;
using Tera.Network.old_Server;

namespace Tera.SkillEngine.Effects
{
    class EfLoseHp : EfDefault
    {
        public override void Tick()
        {
            switch (Effect.Method)
            {
                case 2:
                    CreatureLogic.HpChanged(Creature, Creature.LifeStats.PlusHp((int)Effect.Value), Creature);
                    break;
                case 3: //Percent
                    CreatureLogic.HpChanged(Creature, Creature.LifeStats.PlusHp((int)(Creature.MaxHp * Effect.Value)), Creature);
                    break;
                default:
                    Player player = Creature as Player;
                    if (player != null)
                        new SpChatMessage("Unknown method " + Effect.Method + " for LoseHp effect.", ChatType.System).Send(player);
                    break;
            }

            //TODO send poison hp - in visible
        }
    }
}
