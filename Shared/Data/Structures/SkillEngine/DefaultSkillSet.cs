using System.Collections.Generic;
using Tera.Data.Structures.Player;

namespace Tera.Data.Structures.SkillEngine
{
    [ProtoBuf.ProtoContract]
    public class DefaultSkillSet
    {
        [ProtoBuf.ProtoMember(1)]
        public RaceGenderClass RaceGenderClass { get; set; }

        [ProtoBuf.ProtoMember(2)]
        public List<int> SkillSet { get; set; }
    }
}
