using Microsoft.EntityFrameworkCore;
using RestApiTinderClone.Data;
using RestApiTinderClone.Models;

namespace RestApiTinderClone.Services
{
    public class MatchesService : IMatchesService
    {
        private TinderDataContext _dataContext;
        public MatchesService(TinderDataContext dataContext) {
            _dataContext = dataContext;
        }
        public async Task<User> Like(User user1, User user2)
        {
            if(await GetMatch(user1,user2) == true)
            {
                var Match = await _dataContext.Matches.FirstOrDefaultAsync(s => (s.LeftId == user1.Id || s.RightId == user1.Id) && (s.LeftId == user2.Id || s.RightId == user2.Id));
                if(user2.Id == Match.LeftId && Match.LeftDecision == true)
                {
                    
                    await _dataContext.Matches.Where(s => s.RightId == user1.Id).ExecuteUpdateAsync(s => s.SetProperty(c => c.RightDecision, true));
                    return user2;
                }
                
            }
            else {
                var newMatch = new Matches();
                newMatch.LeftDecision = true;
                newMatch.RightDecision = false;
                newMatch.LeftId = user1.Id;
                newMatch.RightId = user2.Id;
                await _dataContext.AddAsync(newMatch);
                
            }
            return user1;

        }
        public async Task Dislike(User user1, User user2)
        {
            var Match = await _dataContext.Matches.FirstOrDefaultAsync(s => (s.LeftId == user1.Id || s.RightId == user1.Id) && (s.LeftId == user2.Id || s.RightId == user2.Id));
            if(await GetMatch(user1, user2) == true)
            {
                
                await _dataContext.Matches.Where(s => s.RightId == user1.Id).ExecuteUpdateAsync(s => s.SetProperty(c => c.RightDecision, false));
            }
        }
        public async Task<bool> GetMatch(User user1, User user2)
        {
            if (await _dataContext.Matches.CountAsync(s => (s.LeftId == user2.Id || s.RightId == user2.Id)) != 0)
            {
                return true;
            }
            else return false;
        }
        
    }
}
