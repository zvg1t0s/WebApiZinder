using Microsoft.EntityFrameworkCore;
using RestApiTinderClone.Data;
using RestApiTinderClone.Models;
using System.IO;
using NetTopologySuite;
using RestApiTinderClone.Tools;
using RestApiTinderClone.Tools.Interfaces;
namespace RestApiTinderClone.Services
{
    public class UserService : IUsersService
    {
        private TinderDataContext _dataContext;
        private IPhotosService _as3PhotosService;
        private IJWTProvider _jwtProvider;
        public UserService(TinderDataContext dataContext, IPhotosService as3, IJWTProvider jwtProvider) { 
            _dataContext = dataContext;
            _as3PhotosService = as3;
            _jwtProvider = jwtProvider;
        }
        public async Task<User> Create(User current)
        {
            current.Password = Crypt.hashPassword(current.Password);
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
        public async Task<string> Auth(User user)
        {
            var modelInDB = await _dataContext.Users.FirstOrDefaultAsync(x => x.Name == user.Name);
            if (Crypt.hashPassword(user.Password) == modelInDB?.Password)
            {
                return _jwtProvider.Generate(modelInDB);
                
            }
            else throw new Exception("Failed to Login");
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
