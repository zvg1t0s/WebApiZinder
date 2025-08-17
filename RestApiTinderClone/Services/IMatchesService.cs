using RestApiTinderClone.Models; 

namespace RestApiTinderClone.Services
{
    public interface IMatchesService
    {
        public Task<User> Like(User user1, User user2);

        public Task Dislike(User user1, User user2);

        public Task<bool> GetMatch(User user1,User user2);

        public Task<User> GetFirstByPreferencies(User user1);
    }
}
