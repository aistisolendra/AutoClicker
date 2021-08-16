namespace AutoClicker.Models
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
    }
}