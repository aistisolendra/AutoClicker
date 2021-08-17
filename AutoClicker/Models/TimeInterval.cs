using System;

namespace AutoClicker.Models
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
            return (int) TimeSpan.FromHours(Hours).TotalMilliseconds +
                   (int) TimeSpan.FromMinutes(Minutes).TotalMilliseconds +
                   (int) TimeSpan.FromSeconds(Seconds).TotalMilliseconds + Milliseconds;
        }
    }
}