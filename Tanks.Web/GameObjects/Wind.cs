using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using FarseerGames.FarseerPhysics.Mathematics;

namespace Tanks.Web.GameObjects
{
    public class Wind
    {
        Vector2 direction;
        int refreshRate = 200;
        int refreshCount = 0;

        int maxXScale = 700;
        int maxYScale = 0;

        public Wind()
        {
            this.ChangeDirection();
            Page.SceneLoop.Update += new EventHandler<SceneLoopEventArgs>(sceneLoop_Update);
        }

        public int MaxXScale
        {
            get { return this.maxXScale; }
            set { this.maxXScale = value; }
        }
        
        public int MaxYScale
        {
            get { return 0; }
            set { this.maxYScale = value; }
        }

        public int RefreshRate
        {
            get { return this.refreshRate; }
            set { this.refreshRate = value; }
        }

        void sceneLoop_Update(object sender, SceneLoopEventArgs e)
        {
            refreshCount++;
            if (this.refreshCount > this.refreshRate)
            {
                this.ChangeDirection();
                this.refreshCount = 0;
            }
        
        }

        public Vector2 Direction
        {
            get { return this.direction; }
        }

        public void Blow(BaseGameObject item)
        {
            item.Body.ApplyForce(ref this.direction);
        }

        void ChangeDirection()
        {

            int x = Randomizer.Random.Next(-MaxXScale, MaxXScale);
            int y = 0;
            this.direction = new Vector2((float)x, (float)y);
            if (this.Changed != null)
            {
                this.Changed(this, new WindEventArgs(this));
            }
        }

        public event EventHandler<WindEventArgs> Changed;
    }
}
