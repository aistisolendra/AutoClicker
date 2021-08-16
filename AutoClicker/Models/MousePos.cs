namespace AutoClicker.Models
{
    public class MousePos
    {
        public int X { get; set; }
        public int Y { get; set; }

        public MousePos()
        {
            X = 0;
            Y = 0;
        }

        public MousePos(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}