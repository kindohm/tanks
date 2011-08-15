using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using FarseerGames.FarseerPhysics.Collisions;

namespace Tanks.Web.UI
{
    public partial class ProjectileBrush : IDrawingBrush
    {
        private float height;
        private float width;
        Geom geom;
        int damage;

        public ProjectileBrush(Geom geom, int damage)
        {
            this.geom = geom;
            this.damage = damage;
            this.InitializeComponent();
            this.Extender.RotateTransform = rotateTransform;
            this.Extender.Child = this.circleCanvas;
            this.geom.OnCollision += new Geom.CollisionEventHandler(HandleCollision);
            this.Loaded += new RoutedEventHandler(ProjectileBrush_Loaded);
        }

        void ProjectileBrush_Loaded(object sender, RoutedEventArgs e)
        {
        }

        public UIElement GetBrush()
        {
            return this;
        }

        BrushExtender extender = new BrushExtender();
        public BrushExtender Extender { get { return this.extender; } }

        public virtual void Update()
        {
            this.Extender.Update();
        }
        
        public float Radius
        {
            set
            {
                this.width = value * 2;
                this.ellipse.Width = width;
                this.translateTransform.X = -width / 2;
                this.height = value * 2;
                this.ellipse.Height = height;
                this.translateTransform.Y = -height / 2;
                this.rotateTransform.CenterX = value;
                this.rotateTransform.CenterY = value;
            }
            get { return this.width / 2; }
        }

        public Ellipse Ellipse
        {
            get { return this.ellipse; }
        }

        bool HandleCollision(Geom geom1, Geom geom2, ContactList list)
        {
            this.circleCanvas.Children.Remove(this.ellipse);
            //this.blowUpScale.ScaleX = (double)this.damage / 10d;
            //this.blowUpScale.ScaleY = this.blowUpScale.ScaleX;
                //            <local:Explosion.RenderTransform>
                //    <ScaleTransform x:Name="blowUpScale" ScaleX=".7" ScaleY=".7" CenterX="50" CenterY="50" />
                //</local:Explosion.RenderTransform>

            ScaleTransform scale = new ScaleTransform();
            scale.CenterX = 50d;
            scale.CenterY = 50d;
            scale.ScaleX = (double)this.damage / 10d;
            scale.ScaleY = scale.ScaleX;
            this.explosion.RenderTransform = scale;

            this.explode.Begin();
            return true;
        }

        private void explode_Completed(object sender, EventArgs e)
        {
            if (this.Exploded != null)
            {
                this.Exploded(this, EventArgs.Empty);
            }
        }

        public event EventHandler Exploded;
       
    }
}
