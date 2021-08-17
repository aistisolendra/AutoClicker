using System;
using System.Windows.Forms;
using AutoClicker.Enums;

namespace AutoClicker.Models.EventModels
{
    public class KeyPressedEventArgs : EventArgs
    {
        internal KeyPressedEventArgs(KeyboardModifiers modifier, Keys key)
        {
            Modifier = modifier;
            Key = key;
        }

        public KeyboardModifiers Modifier { get; }

        public Keys Key { get; }
    }
}