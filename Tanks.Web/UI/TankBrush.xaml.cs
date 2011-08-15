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
    public partial class TankBrush : IDrawingBrush
    {
        private float height;
        private float width;
        Vehicle vehicle;

        public TankBrush()
        {
            this.InitializeComponent();
        }

        public TankBrush(Vehicle vehicle)
        {
            this.vehicle = vehicle;
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(TankBrush_Loaded);
        }

        public double DamageTextOpacity
        {
            get { return this.damageText.Opacity; }
            set { this.damageText.Opacity = value; }
        }

        public bool StatusInfoVisible
        {
            get { return this.recharge.Opacity != 0; }
            set
            {
                if (value)
                {
                    this.recharge.Opacity = 1;
                    this.health.Opacity = 1;
                    this.hitPointText.Opacity = 1;
                }
                else
                {
                    this.recharge.Opacity = 0;
                    this.health.Opacity = 0;
                    this.hitPointText.Opacity = 0;
                }
            }
        }

        void TankBrush_Loaded(object sender, RoutedEventArgs e)
        {
            this.ResetDamageText();
            this.doDamage.Completed += new EventHandler(doDamage_Completed);
            this.Extender.RotateTransform = rotateTransform;
            this.Extender.Child = this.vehicleCanvas;
            vehicle.Died += new EventHandler<VehicleEventArgs>(vehicle_Died);
            vehicle.FiredShot += new EventHandler<FiredShotEventArgs>(vehicle_FiredShot);
            vehicle.HitPointsChanged += new EventHandler<HitPointsChangedEventArgs>(vehicle_HitPointsChanged);
            this.explode.Completed += new EventHandler(explode_Completed);
            Page.SceneLoop.Update += new EventHandler<SceneLoopEventArgs>(sceneLoop_Update);

            this.UpdateHitPointsDisplay();
            this.UpdateRecharge();
        }

        void doDamage_Completed(object sender, EventArgs e)
        {
            this.ResetDamageText();
        }

        void ResetDamageText()
        {
            this.damageTranslate.Y = 0;
            this.damageText.Opacity = 0;
        }

        void vehicle_HitPointsChanged(object sender, HitPointsChangedEventArgs e)
        {
            if (e.Change < 0)
            {
                this.damageText.Text = e.Change.ToString();
            }
            else
            {
                this.damageText.Text = "+" + e.Change.ToString();
            }
            this.doDamage.Begin();
            this.UpdateHitPointsDisplay();
        }

        void UpdateHitPointsDisplay()
        {
            this.health.Value = (double)this.vehicle.HitPoints/(double)this.vehicle.MaxHitPoints * 100d;
            this.hitPointText.Text = this.vehicle.HitPoints.ToString();
        }

        public Color Color
        {
            get
            {
                return this.bodyFill.Color;
            }
            set
            {
                this.bodyFill.Color = value;
                this.trackFill.Color = value;
                this.cannonFill.Color = value;
            }
        }

        void vehicle_FiredShot(object sender, FiredShotEventArgs e)
        {
            if (!vehicle.ControlledByUser)
            {
                this.UpdateCannon(e.Trajectory);
            }
        }

        void sceneLoop_Update(object sender, SceneLoopEventArgs e)
        {
            if (this.vehicle.ControlledByUser)
            {
                this.UpdateCannon();
            }
            this.UpdateRecharge();
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
            else if (degrees < -180)
            {
                degrees = -180;
            }

            this.cannonRotation.Angle = degrees;
        }

        protected void UpdateCannon()
        {
            Vector2 vector = new Vector2((float)TankView.MouseHandler.MousePosition.X - this.vehicle.Position.X,
                ((float)TankView.MouseHandler.MousePosition.Y - this.vehicle.Position.Y));
            vector = Vector2.Normalize(vector);
            this.UpdateCannon(vector);
        }

        void UpdateRecharge()
        {
            this.recharge.Value = this.vehicle.WeaponCharge * 100;
        }

        BrushExtender extender = new BrushExtender();
        public BrushExtender Extender { get { return this.extender; } }

        public virtual void Update()
        {
            this.Extender.Update();
        }

        public UIElement GetBrush()
        {
            return this;
        }

        public Vector2 Size
        {
            set
            {
                this.width = value.X;
                this.scaleTransform.ScaleX = this.width / 100d;
                this.vehicleCanvas.Width = width;
                this.translateTransform.X = -width / 2;
                this.height = value.Y;
                this.vehicleCanvas.Height = height;
                this.scaleTransform.ScaleY = this.height / 50d;
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
                double cannonWidth = 57;
                double degrees = this.cannonRotation.Angle;
                double hypot = cannonWidth;
                double y = hypot * Math.Sin(degrees / 57.2957d) - 10;
                double x = hypot * Math.Cos(degrees / 57.2957d);
                if (x < 0)
                {
                    x = x - 10;
                }
                else
                {
                    x = x + 10;
                }
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
            this.vehicleCanvas.Children.Remove(this.shapeCanvas);
            this.explode.Begin();
        }

    }
}
