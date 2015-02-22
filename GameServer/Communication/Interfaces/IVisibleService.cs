using System.Collections.Generic;
using Tera.Data.Enums.SkillEngine;
using Tera.Data.Interfaces;
using Tera.Data.Structures.Creature;
using Tera.Data.Structures.Geometry;
using Tera.Data.Structures.World;

namespace Tera.Communication.Interfaces
{
    public interface IVisibleService : IComponent
    {
        void Send(Creature creature, ISendPacket packet);
        List<Creature> FindTargets(Creature creature, Point3D position, double distance, TargetingAreaType type);
        List<Creature> FindTargets(Creature creature, WorldPosition position, double distance, TargetingAreaType type);
    }
}