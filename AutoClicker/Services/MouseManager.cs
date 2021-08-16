using System;
using System.Runtime.InteropServices;
using AutoClicker.Enums;
using AutoClicker.Models;

namespace AutoClicker.Services
{
    public class MouseManager
    {
        private readonly Random _rnd = new();

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

        public void SetCursorPosition(MousePoint point)
        {
            SetCursorPos(point.X, point.Y);
        }

        public void Click(BasicClicker clicker)
        {
            var mouseEvents = GetMouseEvents(clicker);
            var position = HandlePosition(clicker);

            for (int i = 0; i < clicker.ClickTimes; ++i)
                if (clicker.ClickPosition.ClickPositionType != ClickPositionType.CurrentPosition)
                    SetCursorPosition(position);

            MouseEvent(mouseEvents.MouseDown, position);
            MouseEvent(mouseEvents.MouseUp, position);
        }

        private static MouseEvents GetMouseEvents(BasicClicker clicker)
        {
            return clicker.ClickOptions.ButtonType switch
            {
                ButtonType.LButton => new MouseEvents(MouseEventFlags.LeftDown, MouseEventFlags.LeftUp),
                ButtonType.MButton => new MouseEvents(MouseEventFlags.MiddleDown, MouseEventFlags.MiddleUp),
                ButtonType.RButton => new MouseEvents(MouseEventFlags.RightDown, MouseEventFlags.RightUp),

                _ => throw new ArgumentOutOfRangeException(nameof(clicker.ClickOptions.ButtonType),
                    clicker.ClickOptions.ButtonType, null)
            };
        }

        private MousePoint HandlePosition(BasicClicker clicker)
        {
            return clicker.ClickPosition.ClickPositionType switch
            {
                ClickPositionType.CurrentPosition => GetCursorPosition(),
                ClickPositionType.BetweenBounds => GetPointInBounds(clicker),
                ClickPositionType.OnPosition => new MousePoint(clicker.ClickPosition.MousePositionToClick.X,
                    clicker.ClickPosition.MousePositionToClick.Y),

                _ => throw new ArgumentOutOfRangeException(nameof(clicker.ClickPosition.ClickPositionType),
                    clicker.ClickPosition.ClickPositionType, null)
            };
        }

        private MousePoint GetPointInBounds(BasicClicker clicker)
        {
            int x = _rnd.Next(clicker.ClickPosition.ClickPositionBounds.Left,
                clicker.ClickPosition.ClickPositionBounds.Right);
            int y = _rnd.Next(clicker.ClickPosition.ClickPositionBounds.Top,
                clicker.ClickPosition.ClickPositionBounds.Bot);

            return new MousePoint(x, y);
        }

        public MousePoint GetCursorPosition()
        {
            bool gotPoint = GetCursorPos(out var currentMousePoint);

            if (!gotPoint)
                currentMousePoint = new MousePoint(0, 0);

            return currentMousePoint;
        }

        public void MouseEvent(MouseEventFlags value, MousePoint position)
        {
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

        public class MouseEvents
        {
            public MouseEventFlags MouseDown { get; set; }
            public MouseEventFlags MouseUp { get; set; }

            public MouseEvents()
            {
                MouseDown = MouseEventFlags.LeftDown;
                MouseUp = MouseEventFlags.LeftUp;
            }

            public MouseEvents(MouseEventFlags mouseDown, MouseEventFlags mouseUp)
            {
                MouseDown = mouseDown;
                MouseUp = mouseUp;
            }
        }
    }
}