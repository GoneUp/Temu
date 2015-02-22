using System.IO;
using Tera.Data.Structures.Objects;

namespace Tera.Network.old_Server
{
    public class SpProjectile : ASendPacket // len 60
    {
        protected Projectile Projectile;

        public SpProjectile(Projectile projectile)
        {
            Projectile = projectile;
        }

        public override void Write(BinaryWriter writer)
        {
            WriteUid(writer, Projectile.Parent);
            WriteDword(writer, Projectile.GetModelId());
            WriteDword(writer, 0);
            WriteUid(writer, Projectile);
            WriteDword(writer, Projectile.ProjectileSkill.Id);

            WriteSingle(writer, Projectile.Position.X);
            WriteSingle(writer, Projectile.Position.Y);
            WriteSingle(writer, Projectile.Position.Z);

            if (Projectile.TargetPosition != null)
            {
                WriteSingle(writer, Projectile.TargetPosition.X);
                WriteSingle(writer, Projectile.TargetPosition.Y);
                WriteSingle(writer, Projectile.TargetPosition.Z);
            }
            else
            {
                WriteSingle(writer, Projectile.Position.X);
                WriteSingle(writer, Projectile.Position.Y);
                WriteSingle(writer, Projectile.Position.Z);
            }

            WriteSingle(writer, Projectile.Speed); //Speed here
        }
    }
}