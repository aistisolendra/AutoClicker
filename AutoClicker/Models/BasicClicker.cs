using System.Windows.Forms;

namespace AutoClicker.Models
{
    public class BasicClicker
    {
        public TimeInterval TimeInterval { get; set; }
        public ClickRepeat ClickRepeat { get; set; }
        public CheckCursorPosition CheckCursorPosition { get; set; }
        public ClickPosition ClickPosition { get; set; }
        public ClickOptions ClickOptions { get; set; }
        public BasicStatistics BasicStatistics { get; set; }
        public Timer BasicClickerTimer { get; set; }
        public int ClickTimes { get; set; }

        public BasicClicker()
        {
            TimeInterval = new TimeInterval();
            ClickRepeat = new ClickRepeat();
            CheckCursorPosition = new CheckCursorPosition();
            ClickPosition = new ClickPosition();
            ClickOptions = new ClickOptions();
            BasicStatistics = new BasicStatistics();
            BasicClickerTimer = new Timer();
            ClickTimes = 1;
        }
    }
}