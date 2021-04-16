namespace Minesweeper.Models
{
    public class GameResult
    {
        public GameResultEnum Result { get; set; }

        public ButtonStatusDto[][] Field { get; set; }

        public int BombCount { get; set; }
    }
}