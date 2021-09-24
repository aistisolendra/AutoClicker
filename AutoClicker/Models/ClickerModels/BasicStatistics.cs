using System;
using System.Windows.Forms;

namespace AutoClicker.Models.ClickerModels
{
    public class BasicStatistics
    {
        public int TimesClicked { get; set; }
        public TimeSpan TimeWorking { get; set; }
        public Timer Timer { get; set; }

        public BasicStatistics()
        {
            TimesClicked = 0;
            TimeWorking = new TimeSpan();
            Timer = new Timer()
            {
                Interval = 1000
            };
        }
    }
}