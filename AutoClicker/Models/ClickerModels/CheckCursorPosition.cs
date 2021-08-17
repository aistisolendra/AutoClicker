using System.Windows.Forms;

namespace AutoClicker.Models.ClickerModels
{
    public class CheckCursorPosition
    {
        public bool CheckMouseEnabled { get; set; }
        public MousePos MousePos { get; set; }
        public Timer Timer { get; set; }

        public CheckCursorPosition()
        {
            CheckMouseEnabled = false;
            MousePos = new MousePos();
            Timer = new Timer();
        }
    }
}