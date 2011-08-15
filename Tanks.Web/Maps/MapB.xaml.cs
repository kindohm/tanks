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
    public partial class MapB : UserControl, IMap, IDrawingBrush
    {
        BrushExtender extender = new BrushExtender();
        bool showing;

        public MapB()
        {
            InitializeComponent();
            this.Extender.Child = this.groundCanvas;
            this.Loaded += new RoutedEventHandler(MapB_Loaded);
        }

        void MapB_Loaded(object sender, RoutedEventArgs e)
        {
            this.wait.Completed += new EventHandler(wait_Completed);
            this.showCat.Completed += new EventHandler(showCat_Completed);
            this.hideCat.Completed += new EventHandler(hideCat_Completed);
            this.wait.Begin();
        }

        void hideCat_Completed(object sender, EventArgs e)
        {
            this.wait.Begin();
        }

        void showCat_Completed(object sender, EventArgs e)
        {
            this.wait.Begin();
        }

        void wait_Completed(object sender, EventArgs e)
        {
            if (showing)
            {
                showing = false;
                this.hideCat.Begin();
            }
            else
            {
                showing = true;
                this.showCat.Begin();
            }
        }

        public BrushExtender Extender { get { return this.extender; } }

        public PathGeometry PathGeometry
        {
            get { return this.terrainGeometry; }
        }

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
