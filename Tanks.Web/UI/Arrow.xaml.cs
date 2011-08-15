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
using Tanks.Web.GameObjects;

namespace Tanks.Web.UI
{
    public partial class Arrow : UserControl
    {
        public Arrow(Wind wind)
        {
            InitializeComponent();
            wind.Changed += new EventHandler<WindEventArgs>(wind_Changed);
            this.UpdateWind(wind);
        }

        void wind_Changed(object sender, WindEventArgs e)
        {
            this.UpdateWind((Wind)sender);
        }

        void UpdateWind(Wind wind)
        {
            this.windValue.Text = Math.Abs(wind.Direction.X).ToString("###0");

            if (wind.Direction.X == 0)
            {
                this.rotation.Angle = 0;
                this.scale.ScaleX = 1;
                this.scale.ScaleY = 1;
            }
            else
            {

                if (wind.Direction.X < 0)
                {
                    this.rotation.Angle = 0;
                }
                else
                {
                    this.rotation.Angle = 180;
                }

                double scaleX = Math.Abs(wind.Direction.X) / wind.MaxXScale;

                if (scaleX < .7)
                {
                    scaleX = .7;
                }

                this.scale.ScaleX = scaleX;
                this.scale.ScaleY = 1;
            }
        }
        
    }
}
