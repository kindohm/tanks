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
using Tanks.Web.UI;

namespace Tanks.Web
{
    public partial class Page : UserControl
    {
        public static SceneLoop SceneLoop;
        public static KeyHandler Keyhandler;
        //public static MouseHandler MouseHandler;
        TankView view;

        public Page()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Page_Loaded);
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();

            Page.Keyhandler = new KeyHandler();
            Page.Keyhandler.Attach(this);
            
            //Page.MouseHandler = new MouseHandler();
            //Page.MouseHandler.Attach(this);

            Page.SceneLoop = new SceneLoop();
            Page.SceneLoop.Attach(this.mainCanvas);

            view = new TankView();
            view.Ended += new EventHandler(view_Ended);
            
            this.mainCanvas.Children.Add(view);
        }

        void view_Ended(object sender, EventArgs e)
        {
            Page.SceneLoop.Detach(this.mainCanvas);
        }

    }
}
