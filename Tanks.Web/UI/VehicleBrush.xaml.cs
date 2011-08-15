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
using FarseerGames.FarseerPhysics.Mathematics;
using Tanks.Web.GameObjects;

namespace Tanks.Web.UI
{
    public partial class VehicleBrush : IDrawingBrush
    {
        private float height;
        private float width;
        Vehicle vehicle;

        public VehicleBrush(Vehicle vehicle)
        {
            InitializeComponent();
            this.vehicle = vehicle;
            this.Extender.RotateTransform = rotateTransform;
            this.Extender.Child = this.vehicleCanvas;
            vehicle.Died += new EventHandler<VehicleEventArgs>(vehicle_Died);
            vehicle.FiredShot += new EventHandler<FiredShotEventArgs>(vehicle_FiredShot);
            this.explode.Completed += new EventHandler(explode_Completed);
            if (vehicle.ControlledByUser)
            {
                Page.SceneLoop.Update += new EventHandler<SceneLoopEventArgs>(sceneLoop_Update);
            }
            vehicle.ControlledByUserChanged += new EventHandler<VehicleEventArgs>(vehicle_ControlledByUserChanged);
        }

        void vehicle_FiredShot(object sender, FiredShotEventArgs e)
        {
            if (!vehicle.ControlledByUser)
            {
                this.UpdateCannon(e.Trajectory);
            }
        }

        void vehicle_ControlledByUserChanged(object sender, VehicleEventArgs e)
        {
            if (vehicle.ControlledByUser)
            {
                Page.SceneLoop.Update += new EventHandler<SceneLoopEventArgs>(sceneLoop_Update);
            }
            else
            {
                Page.SceneLoop.Update -= sceneLoop_Update;
            }
        }

        void sceneLoop_Update(object sender, SceneLoopEventArgs e)
        {
            this.UpdateCannon();
        }

        void UpdateCannon(Vector2 shotVector)
        {
            float tan = shotVector.Y / shotVector.X;
            double radians = Math.Atan((double)tan);
            double degrees = radians * 57.2957d;

            if (shotVector.X < 0)
            {
                degrees = degrees - 180;
            }
            
            //min/max
            if (degrees > 0)
            {
                degrees = 0;
            }
            else if(degrees < -180)
            {
                degrees = -180;
            }

            this.cannonRotation.Angle = degrees;
        }

        protected void UpdateCannon()
        {
            Vector2 vector = new Vector2((float)Page.MouseHandler.MousePosition.X - this.vehicle.Position.X,
                ((float)Page.MouseHandler.MousePosition.Y - this.vehicle.Position.Y));
            vector = Vector2.Normalize(vector);
            this.UpdateCannon(vector);
        }

        public BrushExtender Extender = new BrushExtender();

        public virtual void Update()
        {
            this.Extender.Update();
        }

        public UIElement GetBrush()
        {
            return this;
        }

        public string HitPointText
        {
            get { return this.hitPointsTextBlock.Text; }
            set { this.hitPointsTextBlock.Text = value; }
        }

        public Vector2 Size
        {
            set
            {
                this.width = value.X;
                this.rectangle.Width = width * .75f;
                this.cannon.Width = width;
                this.cannon.SetValue(Canvas.LeftProperty, width / 2d);
                this.translateTransform.X = -width / 2;
                this.height = value.Y;
                this.rectangle.Height = height;
                this.cannon.Height = 10d;
                this.cannon.SetValue(Canvas.TopProperty, height / 2d);
                this.translateTransform.Y = -height / 2;
                this.rotateTransform.CenterX = width / 2;
                this.rotateTransform.CenterY = height / 2;
            }
            get { return new Vector2(this.width, this.height); }
        }

        public Vector2 CannonEnd
        {
            get
            {
                double degrees = this.cannonRotation.Angle;
                double hypot = this.cannon.Width;
                double y = hypot * Math.Sin(degrees / 57.2957d);
                double x = hypot * Math.Cos(degrees / 57.2957d);
                return new Vector2((float)x, (float)y);
            }
        }

        void explode_Completed(object sender, EventArgs e)
        {
            if (this.Parent != null)
            {

                (this.Parent as Canvas).Children.Remove(this);
            }
        }

        void vehicle_Died(object sender, VehicleEventArgs e)
        {
            this.vehicleCanvas.Children.Remove(this.rectangle);
            this.explode.Begin();
        }
    
    }
}
