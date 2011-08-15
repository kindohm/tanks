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

namespace Tanks.Web.Rounds
{
    public partial class Round6GroundBrush : UserControl, IDrawingBrush
    {
        bool showingCat;

        public Round6GroundBrush()
        {
            InitializeComponent();
            this.Extender.Child = this.groundCanvas;
            
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
    }
}
