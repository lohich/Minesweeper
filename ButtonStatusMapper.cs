using Minesweeper.Models;

namespace Minesweeper
{
    public static class ButtonStatusMapper
    {
        public static ButtonStatusDto Map(ButtonStatus item)
        {
            return new ButtonStatusDto { Status = item.Status, Number = item.Status == ButtonStatusEnum.Opened ? item?.Number : null };
        }

        public static ButtonStatusDto[][] Map(ButtonStatus[][] item)
        {
            if (item == null)
            {
                return null;
            }

            var result = new ButtonStatusDto[item.Length][];
            for (int i = 0; i < item.Length; i++)
            {
                result[i] = new ButtonStatusDto[item[i].Length];
                for (int j = 0; j < result[i].Length; j++)
                {
                    result[i][j] = Map(item[i][j]);
                }
            }

            return result;
        }
    }
}