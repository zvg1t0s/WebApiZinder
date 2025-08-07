using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiTinderClone.Models;
using RestApiTinderClone.Services;

namespace RestApiTinderClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        
        [HttpPost]
        public async Task<User> Register([FromBody]User current)
        {
            return await _usersService.Create(current);
        }
        [HttpPost("id")]
        
        public async Task<IActionResult> Auth([FromBody] User current)
        {
            return Ok(await _usersService.Auth(current));
        }
        [HttpPatch]
        public async Task<string> Update([FromBody]User current) { 
            return await _usersService.Update(current);
        }
        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            return await _usersService.Get(id);
        }
        [HttpGet]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _usersService.Get();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) { 

           
            await _usersService.Delete(id);
            return Ok();
        }

    }
}
