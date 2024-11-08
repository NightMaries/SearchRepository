using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchRepository.API.Entities;
using SearchRepository.API.Interfaces;
using SearchRepository.API.Repositories;

namespace SearchRepository.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly ITokenService _TokenService;
        HMACSHA256 hmac = new HMACSHA256();

        public UserController(IUserRepository repository, ITokenService tokenService)
        {
            _TokenService = tokenService;
            
            this._repository = repository;
        }
        [Authorize]
        // GET: api/User
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_repository.GetUsers());
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _repository.FindUserById(id);
            if (user == null) return NotFound($"User with id {id} not found.");
            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public IActionResult CreateUser(UserDto userDto)
        {
            User user = new User{
                Login = userDto.Login,                
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password)),
                PasswordSalt = hmac.Key,
                Token = _TokenService.CreateToken(userDto.Login)
                
            };
            if (user == null) return BadRequest("User data is null.");
            var createdUser = _repository.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public IActionResult EditUser(int id, User user)
        {
            if (user == null) return BadRequest("User data is null.");
            var updatedUser = _repository.EditUser(user, id);
            if (updatedUser == null) return NotFound($"User with id {id} not found.");
            return Ok(updatedUser);
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var success = _repository.DeleteUser(id);
            if (!success) return NotFound($"User with id {id} not found.");
            return NoContent();
        }
    }
}