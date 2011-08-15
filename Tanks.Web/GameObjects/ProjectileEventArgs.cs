using System;

namespace Tanks.Web.GameObjects
{
    public class ProjectileEventArgs : EventArgs
    {
        public Projectile Projectile { get; set; }

        public ProjectileEventArgs(Projectile projectile)
        {
            this.Projectile = projectile;
        }
    }
}
