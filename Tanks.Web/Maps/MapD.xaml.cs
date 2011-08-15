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

namespace Tanks.Web.Maps
{
    public partial class MapD : UserControl, IMap, IDrawingBrush
    {
      bool showingCat;

      public MapD()
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

        public PathGeometry PathGeometry
        {
            get { return this.terrainGeometry; }
        }
    }
}
