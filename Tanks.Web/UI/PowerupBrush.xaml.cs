using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using FarseerGames.FarseerPhysics.Mathematics;
using Tanks.Web.GameObjects;

namespace Tanks.Web.UI
{
    public partial class PowerupBrush : IDrawingBrush, IPowerupBrush
    {
        private float height;
        private float width;
        Powerup powerup;
        Color color;

        public PowerupBrush(Powerup powerup, Color color)
        {
            this.color = color;
            this.powerup = powerup;
            this.powerup.PowerupExpired += new System.EventHandler(powerup_PowerupExpired);
            this.InitializeComponent();
            this.Loaded += new RoutedEventHandler(PowerupBrush_Loaded);
        }

        public void AddContent(FrameworkElement element)
        {
            this.powerupCanvas.Children.Add(element);
        }

        void powerup_PowerupExpired(object sender, System.EventArgs e)
        {
            this.expire.Begin();
        }

        void PowerupBrush_Loaded(object sender, RoutedEventArgs e)
        {
            this.mainColor.Color = this.color;
            this.mainColorAnimation.From = this.color;
            this.expire.Completed += new System.EventHandler(expire_Completed);
            this.Extender.RotateTransform = rotateTransform;
            this.Extender.Child = this.powerupCanvas;
            this.glow.Begin();
        }

        void expire_Completed(object sender, System.EventArgs e)
        {
            Canvas parent = this.Parent as Canvas;
            if (parent != null)
            {
                parent.Children.Remove(this);
            }
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
                this.rectangle.Width = width;
                this.translateTransform.X = -width / 2;
                this.height = value.Y;
                this.rectangle.Height = height;
                this.translateTransform.Y = -height / 2;
                this.rotateTransform.CenterX = width / 2;
                this.rotateTransform.CenterY = height / 2;
            }
            get { return new Vector2(this.width, this.height); }
        }

        //public Color Color
        //{
        //    get
        //    {
        //        return this.mainColor.Color;
        //    }
        //    set
        //    {
        //        this.glow.Stop();
        //        this.mainColor.Color = value;
        //        this.mainColorAnimation.From = value;
        //        this.glow.Begin();
        //    }
        //}

    }
}