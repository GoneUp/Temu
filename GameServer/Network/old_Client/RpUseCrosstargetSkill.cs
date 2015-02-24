using System.Collections.Generic;
using System.IO;
using Tera.Communication.Logic;
using Tera.Data.Structures;
using Tera.Data.Structures.Creature;
using Tera.Data.Structures.Player;
using Tera.Data.Structures.World;
using Utils.Logger;

namespace Tera.Network.old_Client
{
    class RpUseCrosstargetSkill : ARecvPacket
    {
        protected List<UseSkillArgs> ArgsList = new List<UseSkillArgs>();

        public List<Creature> Targets = new List<Creature>();

        public override void Read()
        {
            int tarCount = ReadWord();
            int tarShift = ReadWord();

            int posCount = ReadWord();
            int posShift = ReadWord();

            int skillId = ReadDword() - 0x04000000;

            float x = Single();
            float y = Single();
            float z = Single();
            short heading = (short) ReadWord();

            if (tarCount > 0)
                Reader.BaseStream.Seek(tarShift - 4, SeekOrigin.Begin);

            for (int i = 0; i < tarCount; i++)
            {
                ReadDword();

                long uniqueId = ReadLong();

                Creature creature = TeraObject.GetObject(uniqueId) as Creature;

                if (creature != null)
                    Targets.Add(creature);
            }

            if (posCount > 0)
                Reader.BaseStream.Seek(posShift - 4, SeekOrigin.Begin);

            for (int i = 0; i < posCount; i++)
            {
                ReadDword();

                ArgsList.Add(
                    new UseSkillArgs
                        {
                            IsTargetAttack = true,
                            SkillId = skillId,
                            StartPosition =
                                new WorldPosition
                                    {
                                        X = x,
                                        Y = y,
                                        Z = z,
                                        Heading = heading,
                                    },
                            TargetPosition =
                                new WorldPosition
                                    {
                                        X = Single(),
                                        Y = Single(),
                                        Z = Single(),
                                        Heading = heading,
                                    },
                            Targets = Targets,
                        });
            }
        }

        public override void Process()
        {
            Logger.WriteLine(LogState.Debug,"Count: " + ArgsList.Count);
            PlayerLogic.UseSkill(Connection, ArgsList);
        }
    }
}
