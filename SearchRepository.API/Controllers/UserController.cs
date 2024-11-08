using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchRepository.API.Entities;
using SearchRepository.API.Interfaces;
using SearchRepository.API.Repositories;
using SqlKata.Execution;

namespace SearchRepository.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly ITokenService _TokenService;
        HMACSHA256 hmac = new HMACSHA256();

        private readonly QueryFactory _quaryFactory;

        public UserController(SearchRepositoryContext connectionDb, ITokenService tokenService, IUserRepository repository)
        {
            _TokenService = tokenService;
            _repository = repository;
            _quaryFactory = connectionDb.SqlQueryFactory;
        }
        //[Authorize]
        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(_quaryFactory.Query("Users").Get());
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _quaryFactory.Query("Users").Where("Id",$"{id}").Get();
            
            if (user == null) return NotFound($"User with id {id} not found.");
            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult> CreateUser(UserDto userDto)
        {

            var user = _quaryFactory.Query("Users").Insert(new 
            {
                    Login = userDto.Login,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password)),
                    PasswordSalt = hmac.Key,
                    Token = _TokenService.CreateToken(userDto.Login)
            });
            return Ok(user);
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public IActionResult EditUser(int id, UserDto userDto)
        {

            var updatedUser = _quaryFactory.Query("Users").Where("Id",$"{id}").Update(new
            {
            
                    Login = userDto.Login,
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDto.Password)),
                    PasswordSalt = hmac.Key,
                    Token = _TokenService.CreateToken(userDto.Login)
            

            });
            if (userDto == null) return BadRequest("User data is null.");
            
            if (updatedUser == null) return NotFound($"User with id {id} not found.");
            return Ok(updatedUser);
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            /*
            var user = _quaryFactory.Query("Users").Where("Id",$"{id}").Get();
            if (user == null) return NotFound($"User with id {id} not found.");
*/
            var success = _quaryFactory.Query("Users").Where("Id",$"{id}").Delete();
            
            return NoContent();
        }
    }
}