using Tera.Data.Interfaces;
using Tera.Data.Structures.Creature;
using Tera.Data.Structures.SkillEngine;
using Utils;

namespace Tera.SkillEngine.Effects
{
    abstract class EfDefault : IEffect
    {
        public Creature Creature;

        public Abnormality Abnormality;

        public AbnormalityEffect Effect;

        public bool IsUpdateStats = false;

        public long LastTick = RandomUtilities.GetCurrentMilliseconds();

        //

        public virtual void Init()
        {
            //Instantly
            if (Effect.TickInterval == 0)
                Tick();
        }

        public virtual void Tick()
        {

        }

        public virtual void UpdateStats()
        {

        }

        //

        public void Action()
        {
            if (Effect.TickInterval == 0)
                return;

            long now = RandomUtilities.GetCurrentMilliseconds();
            long nextTick = LastTick + Effect.TickInterval*1000;

            if (nextTick < now)
            {
                LastTick = nextTick;
                Tick();
            }
        }

        public virtual void SetImpact(CreatureEffectsImpact impact)
        {
            
        }

        public virtual void Release()
        {
            Creature = null;
            Abnormality = null;
            Effect = null;
        }
    }
}
