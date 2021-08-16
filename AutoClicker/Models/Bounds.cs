namespace AutoClicker.Models
{
    public class Bounds
    {
        public int TopLeft { get; set; }
        public int TopRight { get; set; }
        public int BotLeft { get; set; }
        public int BotRight { get; set; }

        public Bounds()
        {
            TopLeft = 0;
            TopRight = 0;
            BotLeft = 0;
            BotRight = 0;
        }
    }
}