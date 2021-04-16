using Microsoft.AspNetCore.Mvc;
using Minesweeper.Models;
using Minesweeper.Services;

namespace Minesweeper.Controllers
{
    [Route("Api/Game/{action}")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;

        }

        [HttpPost]
        public IActionResult Open([FromBody] Coords item)
        {
            return Ok(_gameService.Open(item.X.Value, item.Y.Value));
        }

        [HttpPost]
        public IActionResult OpenAll([FromBody] Coords item)
        {
            return Ok(_gameService.OpenAll(item.X.Value, item.Y.Value));
        }

        [HttpPost]
        public IActionResult Start([FromBody] StartRequest item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(_gameService.InitField(item.CountX.Value, item.CountY.Value, item.BombCount.Value));
        }

        [HttpPost]
        public IActionResult Flag([FromBody] Coords item)
        {
            return Ok(_gameService.Flag(item.X.Value, item.Y.Value));
        }
    }
}