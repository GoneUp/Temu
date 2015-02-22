using Tera.Communication.Logic;
using Tera.Data.Enums;
using Tera.Data.Structures.Creature;
using Tera.Data.Structures.Player;
using Tera.Network.old_Server;

namespace Tera.SkillEngine.Effects
{
    class EfAttackSpeedInc : EfDefault
    {
        public override void Init()
        {
            CreatureLogic.UpdateCreatureStats(Creature);
        }

        public override void SetImpact(CreatureEffectsImpact impact)
        {
            switch (Effect.Method)
            {
                case 2:
                    impact.AttackSpeedModificator += (short) Effect.Value;
                    break;
                case 3: //Percent
                    impact.AttackSpeedPercentModificator += Effect.Value;
                    break;
                default:
                    Player player = Creature as Player;
                    if (player != null)
                        new SpChatMessage("Unknown method " + Effect.Method + " for EfAttackSpeedInc effect.", ChatType.System).Send(player);
                    break;
            }
        }

        public override void Release()
        {
            CreatureLogic.UpdateCreatureStats(Creature);

            base.Release();
        }
    }
}
