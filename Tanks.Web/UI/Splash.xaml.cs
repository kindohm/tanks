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
    public partial class Splash : UserControl
    {
        public Splash()
        {
            InitializeComponent();
            
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NewGameClicked != null)
            {
                this.NewGameClicked(this, EventArgs.Empty);
            }
        }

        public event EventHandler NewGameClicked;
    }
}
