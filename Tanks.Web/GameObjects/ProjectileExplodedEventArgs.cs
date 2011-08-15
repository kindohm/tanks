using System;

namespace Tanks.Web.GameObjects
{
    public class ProjectileExplodedEventArgs : EventArgs
    {
        public Projectile Projectile { get; set; }
        public BaseGameObject Target { get; set; }

        public ProjectileExplodedEventArgs(Projectile projectile, BaseGameObject target)
        {
            this.Projectile = projectile;
            this.Target = target;
        }
    }
}
