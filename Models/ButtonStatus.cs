namespace Minesweeper.Models
{
    public class ButtonStatus
    {
        public ButtonStatus(int x, int y)
        {
            X = x;
            Y = y;
        }

        public ButtonStatusEnum Status { get; set; }

        public int Number { get; set; }

        public bool IsBomb { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
    }
}