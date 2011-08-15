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
    public partial class RoundTitle : UserControl
    {
        string title;
        public RoundTitle(string title)
        {
            this.title = title;
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(RoundTitle_Loaded);
        }

        void RoundTitle_Loaded(object sender, RoutedEventArgs e)
        {
            this.roundNameText.Text = this.title;
            this.delay.Completed += new EventHandler(delay_Completed);
            this.delay.Begin();
        }

        void delay_Completed(object sender, EventArgs e)
        {
            if (this.Closed != null)
            {
                this.Closed(this, null);
            }
        }

        public event EventHandler Closed;

    }
}
