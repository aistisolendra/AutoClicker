using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoClicker.Services
{
    public sealed class KeyboardManager : IDisposable
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private sealed class Window : NativeWindow, IDisposable
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
                    var modifier = (ModifierKeys) ((int) m.LParam & 0xFFFF);

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

        private readonly Window _window = new();
        private int _currentId;
        public string KeyText = "";

        public KeyboardManager()
        {
            _window.KeyPressed += delegate(object sender, KeyPressedEventArgs args) { KeyPressed?.Invoke(this, args); };
        }

        public void RegisterHotKey(ModifierKeys modifier, Keys key)
        {
            _currentId += 1;

            KeyText = $"{modifier} + {key}";

            if (!RegisterHotKey(_window.Handle, _currentId, (uint) modifier, (uint) key))
                throw new InvalidOperationException("Couldn’t register the hot key.");
        }

        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        #region IDisposable Members

        public void Dispose()
        {
            for (int i = _currentId; i > 0; i--) UnregisterHotKey(_window.Handle, i);

            _window.Dispose();
        }

        #endregion
    }

    public class KeyPressedEventArgs : EventArgs
    {
        internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
        {
            Modifier = modifier;
            Key = key;
        }

        public ModifierKeys Modifier { get; }

        public Keys Key { get; }
    }

    [Flags]
    public enum ModifierKeys : uint
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Win = 8
    }
}