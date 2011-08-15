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
    public partial class PowerupHelp : UserControl
    {
        public PowerupHelp()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(PowerupHelp_Loaded);
        }

        void PowerupHelp_Loaded(object sender, RoutedEventArgs e)
        {
            this.llamaGun.mainColor.Color = Colors.Orange;
            this.uzi.mainColor.Color = Colors.Orange;
            this.health.mainColor.Color = Color.FromArgb(255,255,100,100);
            this.maxHealth.mainColor.Color = Colors.Gray;
            this.damage.mainColor.Color = Colors.Red;
            this.rateOfFire.mainColor.Color = Colors.Yellow;
            this.accuracy.mainColor.Color = Colors.Purple;
            this.burst.mainColor.Color = Colors.Green;
        }
    }
}
