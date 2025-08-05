
using RestApiTinderClone.Models;

namespace RestApiTinderClone.Services
{
    public interface IUsersService
    {
        Task<User> Create(User newUser);
        Task<String> Update(User newUser);

        Task<User> Get(int id);
        Task<List<User>>Get();
        
        Task Delete(int id);

        Task AddPhoto( IFormFile file);

        Task<Stream> DownloadPhoto(string KeyName);

        Task<List<User>> GetUsersInRange(User user, double radius);
       
    }
}
