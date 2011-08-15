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
using FarseerGames.FarseerPhysics.Dynamics.Springs;
using FarseerGames.FarseerPhysics.Mathematics;

namespace Tanks.Web.UI
{
    public partial class LinearSpringBrush : IDrawingBrush
    {
        private float _x1;
        private float _x2;
        private float _y1;
        private float _y2;
        public LinearSpring LinearSpring;
        
        public LinearSpringBrush()
        {
            InitializeComponent();
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

        public Vector2 Endpoint1
        {
            set
            {
                if (_x1 != value.X)
                {
                    _x1 = value.X;
                    line.X1 = _x1;
                }
                if (_y1 != value.Y)
                {
                    _y1 = value.Y;
                    line.Y1 = _y1;
                }
            }
        }

        public Vector2 Endpoint2
        {
            set
            {
                if (_x2 != value.X)
                {
                    _x2 = value.X;
                    line.X2 = _x2;
                }
                if (_y2 != value.Y)
                {
                    _y2 = value.Y;
                    line.Y2 = _y2;
                }
            }
        }

        //#region IDrawingBrush Members

        //public void Update()
        //{
        //    if (LinearSpring == null) return;
        //    Endpoint1 = LinearSpring.Body1.GetWorldPosition(LinearSpring.AttachPoint1);
        //    Endpoint2 = LinearSpring.Body2.GetWorldPosition(LinearSpring.AttachPoint2);
        //}

        //#endregion
    }
}
