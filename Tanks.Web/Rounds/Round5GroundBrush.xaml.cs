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
    public partial class Round5GroundBrush : UserControl, IDrawingBrush
    {
        bool showingCat;

        public Round5GroundBrush()
        {
            InitializeComponent();
            this.Extender.Child = this.groundCanvas;
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
            if (!showingCat)
            {
                showingCat = true;
                this.showCat.Begin();
            }
            else
            {
                showingCat = false;
                this.hideCat.Begin();
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
    }
}
