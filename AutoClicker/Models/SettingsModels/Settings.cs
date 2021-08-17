using AutoClicker.Models.ClickerModels;

namespace AutoClicker.Models.SettingsModels
{
    public class Settings
    {
        public BasicClickerBinds BasicClickerBinds { get; set; }

        public Settings()
        {
            BasicClickerBinds = new BasicClickerBinds();
        }
    }
}