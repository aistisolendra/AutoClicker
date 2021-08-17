using System.Windows.Forms;
using AutoClicker.Enums;

namespace AutoClicker.Models.ClickerModels
{
    public class ClickPosition
    {
        public ClickPositionType ClickPositionType { get; set; }
        public Bounds ClickPositionBounds { get; set; }
        public MousePos MousePositionToClick { get; set; }
        public Timer Timer { get; set; }

        public ClickPosition()
        {
            ClickPositionType = ClickPositionType.CurrentPosition;
            ClickPositionBounds = new Bounds();
            MousePositionToClick = new MousePos();
            Timer = new Timer();
        }
    }
}