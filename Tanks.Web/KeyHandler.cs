using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Tanks.Web
{
    public class KeyHandler
    {
        private bool[] isPressed = new bool[255];

        public void ClearKeyPresses()
        {
            for (int i = 0; i < 255; i++)
            {
                this.isPressed[i] = false;
            }
        }

        public void Attach(UserControl target)
        {
            this.ClearKeyPresses();
            target.KeyDown += target_KeyDown;
            target.KeyUp += target_KeyUp;
            target.LostFocus += target_LostFocus;
        }

        public void Detach(UserControl target)
        {
            target.KeyDown -= target_KeyDown;
            target.KeyUp -= target_KeyUp;
            target.LostFocus -= target_LostFocus;
            this.ClearKeyPresses();
        }

        private void target_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.Key < 255)
            {
                this.isPressed[(int)e.Key] = true;
            }
        }

        private void target_KeyUp(object sender, KeyEventArgs e)
        {
            if ((int)e.Key < 255)
            {
                this.isPressed[(int)e.Key] = false;
                if (this.KeyPressed != null)
                {
                    this.KeyPressed(this, e);
                }
            }
        }

        private void target_LostFocus(object sender, EventArgs e)
        {
            this.ClearKeyPresses();
        }

        public bool IsKeyPressed(Key k)
        {
            int v = (int) k;
            if (v < 0 || v > 82) return false;
            return this.isPressed[v];
        }

        public event EventHandler<KeyEventArgs> KeyPressed;
    }
}