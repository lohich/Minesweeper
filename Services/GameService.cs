using System;
using System.Linq;
using Minesweeper.Models;

namespace Minesweeper.Services
{
    public class GameService : IGameService
    {
        private ButtonStatus[][] _field;
        private readonly Random _random = new Random();
        private int _bombCount;
        private static readonly int[] nearestDelta = new[] { -1, 0, 1 };

        public GameResult InitField(int x, int y, int bombCount)
        {
            if (bombCount > x * y - 1)
            {
                return new GameResult { Result = GameResultEnum.Win };
            }

            InitField(x, y);
            _bombCount = bombCount;

            for (int i = 0; i < bombCount; i++)
            {
                int bombX;
                int bombY;
                do
                {
                    bombX = _random.Next(x);
                    bombY = _random.Next(y);
                } while (_field[bombY][bombX].IsBomb);

                _field[bombY][bombX].IsBomb = true;
                DoWithNearest(_field[bombY][bombX], x => { if (!x.IsBomb == true) x.Number++; });
            }

            return CreateResult(GameResultEnum.Continue);
        }

        public GameResult Flag(int x, int y)
        {
            var item = _field[y][x];
            switch (item.Status)
            {
                case ButtonStatusEnum.Closed:
                    item.Status = ButtonStatusEnum.Flagged;
                    break;
                case ButtonStatusEnum.Flagged:
                    item.Status = ButtonStatusEnum.Closed;
                    break;
            }

            return CreateResult(GameResultEnum.Continue);
        }

        public GameResult Open(int x, int y)
        {
            var item = _field[y][x];

            Open(item);

            return WinCheck();
        }

        public GameResult OpenAll(int x, int y)
        {
            var item = _field[y][x];

            if (item.Status == ButtonStatusEnum.Opened && GetNearest(item).Count(x => x.Status == ButtonStatusEnum.Flagged) == item.Number)
            {
                DoWithNearest(item, Open);
            }

            return WinCheck();
        }

        private void Open(ButtonStatus item)
        {
            if (item == null)
            {
                return;
            }

            if (item.Status != ButtonStatusEnum.Flagged)
            {
                if (item.IsBomb)
                {
                    foreach (var bomb in _field.SelectMany(row => row.Where(rowItem => rowItem.IsBomb)))
                    {
                        bomb.Status = ButtonStatusEnum.Bomb;
                    }
                }
                else
                {
                    if (item.Status != ButtonStatusEnum.Opened)
                    {
                        item.Status = ButtonStatusEnum.Opened;
                        if (item.Number == 0)
                        {
                            DoWithNearest(item, Open);
                        }
                    }
                }
            }
        }

        private void DoWithNearest(ButtonStatus item, Action<ButtonStatus> whatToDo)
        {
            foreach (var cell in GetNearest(item))
            {
                whatToDo(cell);
            }
        }

        private ButtonStatus[] GetNearest(ButtonStatus item)
        {
            var xes = nearestDelta.Select(x => x + item.X);
            var yes = nearestDelta.Select(y => y + item.Y);

            var result = _field.SelectMany(row => row.Where(y => xes.Contains(y.X) && yes.Contains(y.Y) && y != item)).ToArray();

            return result;
        }

        private void InitField(int x, int y)
        {
            _field = new ButtonStatus[y][];
            for (int i = 0; i < y; i++)
            {
                _field[i] = new ButtonStatus[x];
                for (int j = 0; j < x; j++)
                {
                    _field[i][j] = new ButtonStatus(i, j);
                }
            }
        }

        private GameResult WinCheck()
        {
            var closedCount = CountBy(y => y.Status == ButtonStatusEnum.Closed || y.Status == ButtonStatusEnum.Flagged);

            if (closedCount == _bombCount)
            {
                return CreateResult(GameResultEnum.Win);
            }

            if (_field.Any(x => x.Any(y => y.Status == ButtonStatusEnum.Bomb)))
            {
                return CreateResult(GameResultEnum.Lose);
            }

            return CreateResult(GameResultEnum.Continue);
        }

        private int CountBy(Func<ButtonStatus, bool> byWhat)
        {
            return _field.Select(x => x.Count(byWhat)).Sum();
        }

        private GameResult CreateResult(GameResultEnum status)
        {
            return new GameResult { Result = status, Field = ButtonStatusMapper.Map(_field), BombCount = _bombCount - CountBy(x => x.Status == ButtonStatusEnum.Flagged) };
        }
    }
}