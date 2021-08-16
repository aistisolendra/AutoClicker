using AutoClicker.Enums;

namespace AutoClicker.Models
{
    public class ClickRepeat
    {
        public ClickRepeatType ClickRepeatType { get; set; }
        public int ClickRepeatTimes { get; set; }

        public ClickRepeat()
        {
            ClickRepeatType = ClickRepeatType.RepeatUntilStopped;
            ClickRepeatTimes = 0;
        }
    }
}