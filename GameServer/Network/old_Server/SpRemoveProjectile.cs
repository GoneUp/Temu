using System.IO;
using Tera.Data.Structures.Objects;

namespace Tera.Network.old_Server
{
    public class SpRemoveProjectile : ASendPacket
    {
        protected Projectile Projectile;

        public SpRemoveProjectile(Projectile projectile)
        {
            Projectile = projectile;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Projectile);
            WriteByte(writer, 1);
        }
    }
}