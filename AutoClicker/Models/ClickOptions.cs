using System;
using AutoClicker.Enums;

namespace AutoClicker.Models
{
    public class ClickOptions
    {
        public ButtonType ButtonType { get; set; }
        public ClickType ClickType { get; set; }
        public int TimeBetweenClickTypes { get; set; }
        public bool ClickWorkingEnabled { get; set; }
        public int ClickWorkingPercentage { get; set; }
        public bool RandomizeClickingEnabled { get; set; }
        public Bounds RandomizeClickingBounds { get; set; }

        public ClickOptions()
        {
            ButtonType = ButtonType.LButton;
            ClickType = ClickType.Single;
            TimeBetweenClickTypes = 0;
            ClickWorkingEnabled = false;
            ClickWorkingPercentage = 0;
            RandomizeClickingEnabled = false;
            RandomizeClickingBounds = new Bounds();
        }
    }
}