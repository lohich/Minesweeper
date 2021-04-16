using System.ComponentModel.DataAnnotations;

namespace Minesweeper.Models
{
    public class StartRequest
    {
        [Required]
        public int? CountX { get; set; }

        [Required]
        public int? CountY { get; set; }

        [Required]
        public int? BombCount { get; set; }
    }
}