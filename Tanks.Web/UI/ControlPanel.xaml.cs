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

namespace Tanks.Web.UI
{
    public partial class ControlPanel : UserControl
    {
        public ControlPanel()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(ControlPanel_Loaded);
        }

        void ControlPanel_Loaded(object sender, RoutedEventArgs e)
        {

            this.powerSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(powerSlider_ValueChanged);
            this.powerSlider_ValueChanged(null, null);

            Page.SceneLoop.Update += new EventHandler<SceneLoopEventArgs>(sceneLoop_Update);
            this.powerSlider.Focus();
        }

        void sceneLoop_Update(object sender, SceneLoopEventArgs e)
        {
            if (Page.Keyhandler.IsKeyPressed(Key.E)){
                this.powerSlider.Value++;
            }
            if (Page.Keyhandler.IsKeyPressed(Key.D))
            {
                this.powerSlider.Value--;
            }
        }

        void powerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.powerTextBlock.Text = this.powerSlider.Value.ToString("##");
        }

        public void Reset()
        {
            this.powerSlider.Value = 50d;
        }

    }
}
