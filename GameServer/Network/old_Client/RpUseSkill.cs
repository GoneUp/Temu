using System.Collections.Generic;
using Tera.Data.Structures;
using Tera.Data.Structures.Creature;
using Tera.Data.Structures.World;

namespace Tera.Network.old_Client
{
    public class RpUseSkill : ARecvPacket
    {
        protected List<Creature> Targets = new List<Creature>();

        protected int SkillId;

        protected WorldPosition StartPosition = new WorldPosition();
        protected WorldPosition TargetPosition = new WorldPosition();

        public override void Read()
        {
            int count = ReadWord();
            ReadWord();

            SkillId = ReadDword() - 0x4000000;

            StartPosition.X = Single();
            StartPosition.Y = Single();
            StartPosition.Z = Single();
            StartPosition.Heading = (short) ReadWord();

            TargetPosition.X = Single();
            TargetPosition.Y = Single();
            TargetPosition.Z = Single();
            TargetPosition.Heading = StartPosition.Heading;

            for (int i = 0; i < count; i++)
            {
                ReadDword(); //shifts

                long uid = ReadLong();

                Creature creature = (Creature) TeraObject.GetObject(uid);

                if (creature != null)
                    Targets.Add(creature);
            }
        }

        public override void Process()
        {
            
        }
    }
}