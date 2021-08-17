using System;

namespace AutoClicker.Models.ClickerModels
{
    public class TimeInterval
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int Milliseconds { get; set; }

        public TimeInterval()
        {
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
            Milliseconds = 0;
        }

        public int ToMs()
        {
            int result = (int) TimeSpan.FromHours(Hours).TotalMilliseconds +
                         (int) TimeSpan.FromMinutes(Minutes).TotalMilliseconds +
                         (int) TimeSpan.FromSeconds(Seconds).TotalMilliseconds + Milliseconds;

            return result <= 0 ? 1 : result;
        }
    }
}