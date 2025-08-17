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
                var Match = await _dataContext.Matches.FirstOrDefaultAsync(s => (s.Left == user1 || s.Right == user1) && (s.Left == user2 || s.Right == user2));
                if(user2 == Match.Left && Match.LeftDecision == true)
                {
                    
                    await _dataContext.Matches.Where(s => s.Right == user1).ExecuteUpdateAsync(s => s.SetProperty(c => c.RightDecision, true));
                    return user2;
                }
                
            }
            else {
                var newMatch = new Matches();
                newMatch.LeftDecision = true;
                newMatch.RightDecision = false;
                newMatch.Left = user1;
                newMatch.Right = user2;
                await _dataContext.AddAsync(newMatch);
                
            }
            return user1;

        }
        public async Task Dislike(User user1, User user2)
        {
            var Match = await _dataContext.Matches.FirstOrDefaultAsync(s => (s.Left == user1 || s.Right == user1) && (s.Left == user2 || s.Right == user2));
            if(await GetMatch(user1, user2) == true)
            {
                
                await _dataContext.Matches.Where(s => s.Right == user1).ExecuteUpdateAsync(s => s.SetProperty(c => c.RightDecision, false));
            }
        }
        public async Task<bool> GetMatch(User user1, User user2)
        {
            if (await _dataContext.Matches.CountAsync(s => (s.Left == user2 || s.Right == user2)) != 0)
            {
                return true;
            }
            else return false;
        }

        public async Task<User> GetFirstByPreferencies(User user1)
        {
            
            var user = await _dataContext.Users.Where(s => 
            (s != user1) &&
            (s.Age >= user1.Preferencies.MinimalAge)
            && (s.Age <= user1.Preferencies.MaximalAge)
            && (s.Sex != user1.Sex) 
            && (_dataContext.Matches.Where( x => x.Left == user1 && x.Right == s).Count() == 0)).FirstOrDefaultAsync();
            return user;
        }
        
    }
}
