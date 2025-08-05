using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiTinderClone.Services;
using RestApiTinderClone.Models;

namespace RestApiTinderClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private IMatchesService _matchesService;
        private IUsersService _usersService;
        public MatchesController(IMatchesService matchesService, IUsersService users)
        {
            _usersService = users;
            _matchesService = matchesService;
        }
        [HttpPost("Like")]
        public async Task<User> LikesAsync([FromBody]User user1, int id)
        {
           var user2 = await _usersService.Get(id);
          return await _matchesService.Like(user1, user2);
        }
        [HttpPost("Dislike")]
        public async Task DisLikesAsync(User user1, int id)
        {
            var user2 = await _usersService.Get(id);
            await _matchesService.Dislike(user1,user2);
        }
    }
}
