using System.Windows.Forms;
using AutoClicker.Models.MouseModels;

namespace AutoClicker.Models.ClickerModels
{
    public class BasicClicker
    {
        public TimeInterval TimeInterval { get; set; }
        public ClickRepeat ClickRepeat { get; set; }
        public CursorPosition CursorPosition { get; set; }
        public ClickPosition ClickPosition { get; set; }
        public ClickOptions ClickOptions { get; set; }
        public BasicStatistics BasicStatistics { get; set; }
        public Timer BasicClickerTimer { get; set; }
        public MouseEvents SelectedMouseEvents { get; set; }

        public BasicClicker()
        {
            TimeInterval = new TimeInterval();
            ClickRepeat = new ClickRepeat();
            CursorPosition = new CursorPosition();
            ClickPosition = new ClickPosition();
            ClickOptions = new ClickOptions();
            BasicStatistics = new BasicStatistics();
            BasicClickerTimer = new Timer();
        }
    }
}