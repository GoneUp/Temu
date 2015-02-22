using Tera.Data.Enums;

namespace Tera.Data.Structures.World
{
    public class AttackResult
    {
        public Creature.Creature Target;
        public int Damage;
        public int AttackUid;
        public int HpDiff;
        public int AngleDif;
        public AttackType AttackType;
        public VisualEffect VisualEffect;
    }
}
