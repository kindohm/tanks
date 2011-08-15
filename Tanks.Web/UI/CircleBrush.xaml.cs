using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Tanks.Web.UI
{
    public partial class CircleBrush : IDrawingBrush
    {
        private float height;
        private float width;
        Color color;

        public CircleBrush()
        {
            this.InitializeComponent();
            this.Extender.RotateTransform = rotateTransform;
            this.Extender.Child = this.circleCanvas;
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

        public Color Color
        {
            get { return this.color; }
            set { this.color = value;
            this.mainColor.Color = this.color;
            }
        }

        

        private void circleCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            this.mainColor.Color = Colors.Gray;
        }

        private void circleCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            this.mainColor.Color = this.Color;
        }
    }
}