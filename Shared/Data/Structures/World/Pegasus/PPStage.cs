using System.Collections.Generic;
using Tera.Data.Enums.Pegasus;

namespace Tera.Data.Structures.World.Pegasus
{
    [ProtoBuf.ProtoContract]
    public class PPStage
    {
        [ProtoBuf.ProtoMember(1)]
        public PType Type;

        [ProtoBuf.ProtoMember(2)]
        public int ContinentId;

        [ProtoBuf.ProtoMember(10)]
        public List<FlyStep> FlySteps;
    }
}
