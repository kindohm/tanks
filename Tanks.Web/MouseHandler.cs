using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Tanks.Web
{
    public class MouseHandler
    {
        bool isMousePressed;
        Canvas target;
        Point currentPoint = new Point(0,0);

        public void Attach(Canvas target)
        {
            this.target = target;
            this.target.MouseLeftButtonDown += new MouseButtonEventHandler(target_MouseLeftButtonDown);
            this.target.MouseLeftButtonUp += new MouseButtonEventHandler(target_MouseLeftButtonUp);
            this.target.MouseMove += new MouseEventHandler(target_MouseMove);
        }

        public Point MousePosition
        {
            get { return this.currentPoint; }
        }

        void target_MouseMove(object sender, MouseEventArgs e)
        {
            this.currentPoint = e.GetPosition(this.target);
        }

        void target_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.isMousePressed = false;
        }

        void target_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.isMousePressed = true;
        }

        public void Detach()
        {
            this.target.MouseLeftButtonDown -= target_MouseLeftButtonDown;
            this.target.MouseLeftButtonUp -= target_MouseLeftButtonUp;
        }

        public bool IsMousePressed
        {
            get { return this.isMousePressed; }
        }

        
    }
}