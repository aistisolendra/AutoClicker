using System;
using System.Windows.Forms;
using AutoClicker.Enums;
using AutoClicker.Models.EventModels;

namespace AutoClicker.Models.KeyboardModels
{
    internal sealed class Window : NativeWindow, IDisposable
    {
        private static int WM_HOTKEY = 0x0312;

        public Window()
        {
            CreateHandle(new CreateParams());
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_HOTKEY)
            {
                var key = (Keys) (((int) m.LParam >> 16) & 0xFFFF);
                var modifier = (KeyboardModifiers) ((int) m.LParam & 0xFFFF);

                KeyPressed?.Invoke(this, new KeyPressedEventArgs(modifier, key));
            }
        }

        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        #region IDisposable Members

        public void Dispose()
        {
            DestroyHandle();
        }

        #endregion
    }
}