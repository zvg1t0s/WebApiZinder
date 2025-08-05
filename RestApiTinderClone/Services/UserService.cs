using Microsoft.EntityFrameworkCore;
using RestApiTinderClone.Data;
using RestApiTinderClone.Models;
using System.IO;
using RestApiTinderClone.Data;
using NetTopologySuite;
namespace RestApiTinderClone.Services
{
    public class UserService : IUsersService
    {
        private TinderDataContext _dataContext;
        private IPhotosService _as3PhotosService;
        public UserService(TinderDataContext dataContext, IPhotosService as3) { 
            _dataContext = dataContext;
            _as3PhotosService = as3;
        }
        public async Task<User> Create(User current)
        {
            /*
            User last = _dataContext.Users.LastOrDefault();
            current.Id = last is null ? 1 : last.Id + 1;
            current.Password = Crypt.hashPassword(current.Password);
            _dataContext.Users.Add(current);
            return current;
            */
            await _dataContext.AddAsync(current);
            await _dataContext.SaveChangesAsync();
            return current;
        }
        
        public async Task <String> Update(User current) { 
            
            await _dataContext.Users.Where(b => b.Id == current.Id).ExecuteUpdateAsync(b => b
            .SetProperty(c => c.Password, Crypt.hashPassword(current.Password))
            .SetProperty(c => c.Name, current.Name)
            .SetProperty(c => c.Login, current.Login)
            .SetProperty(c => c.Email, current.Email)
            .SetProperty(c => c.Location,current.Location)
            
            
            );
            return "Успешно";
            
        
        }
        public async Task<User> Auth(User user)
        {
            var modelInDB = await _dataContext.Users.FirstOrDefaultAsync(x => x.Name == user.Name);
            if (Crypt.hashPassword(user.Name) == modelInDB?.Password){
                return modelInDB;
            }
            return user;
        }

        public async Task<User?> Get(int id)
        {
            return await _dataContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<User>> Get(){
            return await _dataContext.Users.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
        }
        public async Task Delete(int Id) {
            
           await _dataContext.Users.Where(x => x.Id == Id).ExecuteDeleteAsync();

        }
        public async Task AddPhoto(IFormFile file)
        {
            /*
            var userPhoto = new UserPhoto();
            userPhoto.UserId = user.Id;
            var keyName = file.FileName;
            userPhoto.ImageAddress = 
            */
            var keyName = file.FileName;
            await _as3PhotosService.UploadFileAsync(file,keyName);
        }
        public async Task<Stream> DownloadPhoto(string keyName)
        {
            return await _as3PhotosService.DownloadFileAsync(keyName);
        }

        public async Task<List<User>> GetUsersInRange(User user, double radiusInMeters)
        {
            var nearlyUsers = await _dataContext.Users.Where(u => u.Location.IsWithinDistance(user.Location, radiusInMeters)).ToListAsync();
            return nearlyUsers;
        }

    }
}
