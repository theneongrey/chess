using ChessApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ChessApi.Controllers
{
    [ApiController]
    [Route("game/[controller]")]
    public class GamesController : ControllerBase
    {
        public GamesController()
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GameShortDto>), StatusCodes.Status200OK)]
        public IActionResult GetGames()
        {
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddGame([FromBody] NewGameDto game)
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GameDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetGame(Guid id)
        {
            return Ok();
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateGame(Guid id, [FromBody] MoveDto move)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(Guid id)
        {
            return Ok();
        }
    }
}