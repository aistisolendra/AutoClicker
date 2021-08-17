namespace AutoClicker.Models.ClickerModels
{
    public class Bounds
    {
        public int Top { get; set; }
        public int Bot { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }

        public Bounds()
        {
            Top = 0;
            Bot = 0;
            Left = 0;
            Right = 0;
        }

        public void CheckBounds()
        {
            if (Top < 0)
                Top = 0;
            if (Bot < 0)
                Bot = 0;
            if (Left < 0)
                Left = 0;
            if (Right < 0)
                Right = 0;
        }
    }
}