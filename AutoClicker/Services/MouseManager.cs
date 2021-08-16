using System;
using System.Runtime.InteropServices;
using System.Timers;
using AutoClicker.Enums;
using AutoClicker.Models;
using Timer = System.Timers.Timer;

namespace AutoClicker.Services
{
    public class MouseManager
    {
        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public void SetCursorPosition(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public void SetCursorPosition(MousePos point)
        {
            SetCursorPos(point.X, point.Y);
        }

        public void Click(ButtonType buttonType, int timesToClick, int timeBetweenClicks)
        {
            switch (buttonType)
            {
                case ButtonType.LButton:
                    PerformClicks(MouseEventFlags.LeftDown, MouseEventFlags.LeftUp, timesToClick, timeBetweenClicks);
                    break;
                case ButtonType.MButton:
                    PerformClicks(MouseEventFlags.MiddleDown, MouseEventFlags.MiddleUp, timesToClick,
                        timeBetweenClicks);
                    break;
                case ButtonType.RButton:
                    PerformClicks(MouseEventFlags.RightDown, MouseEventFlags.RightUp, timesToClick, timeBetweenClicks);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonType), buttonType, null);
            }
        }

        private void PerformClicks(MouseEventFlags mouseDown, MouseEventFlags mouseUp, int timesToClick,
            int timeBetweenClicks)
        {
            for (int i = 0; i < timesToClick; ++i)
            {
                MouseEvent(mouseDown);
                MouseEvent(mouseUp);
            }
        }

        public MousePoint GetCursorPosition()
        {
            MousePoint currentMousePoint;
            bool gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) currentMousePoint = new MousePoint(0, 0);
            return currentMousePoint;
        }

        public void MouseEvent(MouseEventFlags value)
        {
            var position = GetCursorPosition();

            mouse_event
                ((int) value,
                    position.X,
                    position.Y,
                    0,
                    0)
                ;
        }

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
        }
    }
}