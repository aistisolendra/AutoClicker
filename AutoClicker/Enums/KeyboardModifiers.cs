using System;

namespace AutoClicker.Enums
{
    [Flags]
    public enum KeyboardModifiers : uint
    {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Win = 8
    }
}