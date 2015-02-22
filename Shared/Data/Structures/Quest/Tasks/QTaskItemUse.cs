using System.Collections.Generic;
using Tera.Data.Interfaces;

namespace Tera.Data.Structures.Quest.Tasks
{
    [ProtoBuf.ProtoContract]
    public class QTaskItemUse : IQuestStep
    {
        [ProtoBuf.ProtoMember(1)]
        public List<int> ItemIds { get; set; }

        [ProtoBuf.ProtoMember(101)]
        public bool IsDebug { get; set; }
    }
}
