using System.Runtime.InteropServices;
using AutoClicker.Models;

namespace AutoClicker.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MousePoint
    {
        public int X;
        public int Y;

        public MousePoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public MousePoint(BasicClicker clicker)
        {
            X = clicker.ClickPosition.MousePositionToClick.X;
            Y = clicker.ClickPosition.MousePositionToClick.Y;
        }
    }
}