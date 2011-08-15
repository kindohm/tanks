using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Mathematics;

namespace Tanks.Web
{
    public class BrushExtender
    {
        float rotation;
        float x;
        float y;
        Body body;
        FrameworkElement child;
        RotateTransform rotateTransform;

        public Vector2 Position
        {
            set
            {
                child.SetValue(Canvas.LeftProperty, Convert.ToDouble(value.X));
                child.SetValue(Canvas.TopProperty, Convert.ToDouble(value.Y));
            }
        }

        public FrameworkElement Child
        {
            get { return this.child; }
            set { this.child = value; }
        }

        public RotateTransform RotateTransform
        {
            get { return this.rotateTransform; }
            set { this.rotateTransform = value; }
        }

        public Body Body
        {
            get { return this.body; }
            set { this.body = value; }
        }

        public virtual void Update()
        {
            if (this.Body == null) return;
            if (this.x != this.Body.Position.X)
            {
                this.x = this.Body.Position.X;
                this.Child.SetValue(Canvas.LeftProperty, Convert.ToDouble(this.x));
            }
            if (this.y != this.Body.Position.Y)
            {
                this.y = this.Body.Position.Y;
                this.Child.SetValue(Canvas.TopProperty, Convert.ToDouble(this.y));
            }
            if (this.Body.Rotation != this.rotation)
            {
                this.rotation = this.Body.Rotation;
                if (this.RotateTransform != null)
                {
                    this.RotateTransform.Angle = (this.rotation * 360) / (2 * Math.PI);
                }
            }
        }

    }
}
