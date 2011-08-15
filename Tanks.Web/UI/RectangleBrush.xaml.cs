using System.Windows;
using FarseerGames.FarseerPhysics.Mathematics;

namespace Tanks.Web.UI
{
    public partial class RectangleBrush : IDrawingBrush
    {
        private float height;
        private float width;

        public RectangleBrush()
        {
            this.InitializeComponent();
            this.Extender.RotateTransform = rotateTransform;
            this.Extender.Child = rectangle;
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

        //#region IDrawingBrush Members

        //public void Update()
        //{
        //    this.Extender.Update();
        //}

        //#endregion
    }
}