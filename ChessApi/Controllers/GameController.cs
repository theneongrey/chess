using AutoMapper;
using ChessApi.Dto;
using ChessApi.Model;
using ChessApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChessApi.Controllers
{
    [ApiController]
    [Route("game/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GamesController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;

        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<GameShortDto>), StatusCodes.Status200OK)]
        public IActionResult GetGames()
        {
            var games = _gameService.GetGames();
            return Ok(_mapper.Map<IEnumerable<GameShortDto>>(games));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult NewGame([FromBody] NewGameDto newGame)
        {
            var game = _gameService.NewGame(newGame.Player1, newGame.Player2);
            return CreatedAtAction(nameof(GetGame), new { id = game.Id }, _mapper.Map<GameShortDto>(game));
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GameDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetGame(Guid id)
        {
            var game = _gameService.GetGameById(id);
            if (game == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GameDto>(game));
        }


        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult MovePiece(Guid id, [FromBody] MoveDto move)
        {
            var from = CellPosition.FromString(move.From);
            var to = CellPosition.FromString(move.To);

            try
            {
                var game = _gameService.MovePiece(id, from, to);
                if (game == null)
                {
                    return NotFound();
                }
            }
            catch (IllegalMoveException moveException)
            {
                return BadRequest(new { error = moveException.Message });
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteGame(Guid id)
        {
            var deleted = _gameService.DeleteGame(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}