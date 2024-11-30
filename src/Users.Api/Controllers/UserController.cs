using Microsoft.AspNetCore.Mvc;
using Users.Api.Entities;
using Users.Api.Services;

namespace Users.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<User> users = await _usersService.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            User user = await _usersService.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest(new { message = "Invalid user data" });
            }

            await _usersService.CreateAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest(new { message = "Invalid user data" });
            }

            User existingUser = await _usersService.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound(new { message = "User not found" });
            }

            user.Id = id;
            await _usersService.UpdateAsync(id, user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            User existingUser = await _usersService.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound(new { message = "User not found" });
            }

            await _usersService.DeleteAsync(id);

            return NoContent();
        }
    }
}