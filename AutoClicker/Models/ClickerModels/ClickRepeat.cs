using AutoClicker.Enums;

namespace AutoClicker.Models.ClickerModels
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