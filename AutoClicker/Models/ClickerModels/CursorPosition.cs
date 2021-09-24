using System.Windows.Forms;

namespace AutoClicker.Models.ClickerModels
{
    public class CursorPosition
    {
        public bool CheckMouseEnabled { get; set; }
        public MousePos MousePos { get; set; }
        public Timer Timer { get; set; }

        public CursorPosition()
        {
            CheckMouseEnabled = false;
            MousePos = new MousePos();
            Timer = new Timer();
        }
    }
}