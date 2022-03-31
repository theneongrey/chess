using AutoMapper;
using ChessApi.Data;
using ChessApi.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ChessApi.Controllers
{
    [ApiController]
    [Route("game/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersService;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepository usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<UserDto>), StatusCodes.Status200OK)]
        public IActionResult GetUsers()
        {
            var users = _usersService.GetUsers();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddUser([FromBody] NewUserDto userData)
        {
            var user = _usersService.AddUser(userData.Name);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, _mapper.Map<UserDto>(user));
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser(Guid id)
        {
            var user = _usersService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateUser(Guid id, [FromBody] NewUserDto userData) 
        {
            var user = _usersService.UpdateUser(id, userData.Name);
            if (user == null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(Guid id)
        {
            if (!_usersService.DeleteUser(id))
            {
                return NotFound();
            }
            return Ok();
        }
    }
}