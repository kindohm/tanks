using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Tanks.Web
{
    public class SceneLoop
    {
        //public delegate void UpdateDelegate(TimeSpan elapsedTime);

        private TimeSpan elapsedTime;
        private DateTime lastUpdateTime = DateTime.MinValue;
        private Storyboard storyboard;

        public event EventHandler<SceneLoopEventArgs> Update;

        public void Attach(Canvas canvas)
        {
            this.storyboard = new Storyboard();
            this.storyboard.SetValue(FrameworkElement.NameProperty, "sceneLoop");
            canvas.Resources.Add("sceneLoop", storyboard);
            this.lastUpdateTime = DateTime.Now;
            this.storyboard.Completed += storyboard_Completed;
            this.storyboard.Begin();
        }

        public void Detach(Canvas canvas)
        {
            this.storyboard.Stop();
            canvas.Resources.Remove("sceneLoop");
        }

        private void storyboard_Completed(object sender, EventArgs e)
        {
            this.elapsedTime = DateTime.Now - lastUpdateTime;
            this.lastUpdateTime = DateTime.Now;
            if (this.Update != null) {
                this.Update(this, new SceneLoopEventArgs(this, this.elapsedTime));
            }
            this.storyboard.Begin();
        }
    }
}