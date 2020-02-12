using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Models;
using WebApi.DTO;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var authToken = _userService.Authenticate(model);

            if (authToken == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            return Ok(authToken);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            if (users == null)
                return NoContent();
            
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(long id)
        {
            var user = _userService.GetUser(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateUser(CreateUserDto user)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var createdUser =_userService.CreateUser(user);
            if (createdUser == null)
                return BadRequest();
            
            return Ok(createdUser);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(long id, EditUserDto editUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var updatedUser = _userService.UpdateUser(id, editUserDto);
            if (updatedUser == null)
                return NotFound();

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(long id)
        {
            if (_userService.DeleteUser(id))
                return Ok();
            
            return NotFound();
        }
    }
}
