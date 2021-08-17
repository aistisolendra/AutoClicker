namespace AutoClicker.Models
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