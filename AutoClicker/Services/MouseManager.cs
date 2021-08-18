using System;
using System.Runtime.InteropServices;
using AutoClicker.Enums;
using AutoClicker.Models.ClickerModels;
using AutoClicker.Models.MouseModels;
using AutoClicker.Structs;

namespace AutoClicker.Services
{
    public class MouseManager
    {
        private readonly Random _rnd;

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        public MouseManager()
        {
            _rnd = new Random();
        }

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

            for (int i = 0; i < (int) clicker.ClickOptions.ClickType; ++i)
            {
                if (clicker.ClickPosition.ClickPositionType != ClickPositionType.CurrentPosition)
                    SetCursorPosition(position);

                MouseEvent(mouseEvents.MouseDown, position);
                MouseEvent(mouseEvents.MouseUp, position);
            }
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
                ClickPositionType.BetweenBounds => GetPointInBounds(clicker.ClickPosition.ClickPositionBounds),
                ClickPositionType.OnPosition => HandleOnPosition(clicker),

                _ => throw new ArgumentOutOfRangeException(nameof(clicker.ClickPosition.ClickPositionType),
                    clicker.ClickPosition.ClickPositionType, null)
            };
        }

        private MousePoint HandleOnPosition(BasicClicker clicker)
        {
            return clicker.ClickOptions.RandomizeClickingEnabled
                ? GetPointAroundPosition(clicker)
                : new MousePoint(clicker);
        }

        private MousePoint GetPointAroundPosition(BasicClicker clicker)
        {
            var newBounds = new Bounds
            {
                Top = clicker.ClickPosition.MousePositionToClick.Y -
                      clicker.ClickOptions.RandomizeClickingBounds.Top,
                Bot = clicker.ClickPosition.MousePositionToClick.Y +
                      clicker.ClickOptions.RandomizeClickingBounds.Bot,
                Left = clicker.ClickPosition.MousePositionToClick.X -
                       clicker.ClickOptions.RandomizeClickingBounds.Left,
                Right = clicker.ClickPosition.MousePositionToClick.X +
                        clicker.ClickOptions.RandomizeClickingBounds.Right
            };

            newBounds.CheckBounds();

            return GetPointInBounds(newBounds);
        }

        private MousePoint GetPointInBounds(Bounds bounds)
        {
            int x = _rnd.Next(bounds.Left, bounds.Right);
            int y = _rnd.Next(bounds.Top, bounds.Bot);

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
            mouse_event((int) value, position.X, position.Y, 0, 0);
        }
    }
}