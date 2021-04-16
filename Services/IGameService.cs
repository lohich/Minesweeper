using Minesweeper.Models;

namespace Minesweeper.Services
{
    public interface IGameService
    {
        public GameResult OpenAll(int x, int y);
        public GameResult Open(int x, int y);
        public GameResult Flag(int x, int y);
        public GameResult InitField(int x, int y, int bombCount);
    }
}