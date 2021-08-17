using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AutoClicker.Enums;
using AutoClicker.Models.EventModels;
using AutoClicker.Models.KeyboardModels;

namespace AutoClicker.Services
{
    public sealed class KeyboardManager : IDisposable
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private readonly Window _window = new();
        private int _currentId;
        public string KeyText = "";

        public KeyboardManager()
        {
            _window.KeyPressed += delegate(object sender, KeyPressedEventArgs args) { KeyPressed?.Invoke(this, args); };
        }

        public void RegisterHotKey(KeyboardModifiers modifier, Keys key)
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
}