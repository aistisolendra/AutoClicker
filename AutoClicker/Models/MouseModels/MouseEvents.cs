using AutoClicker.Enums;

namespace AutoClicker.Models.MouseModels
{
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