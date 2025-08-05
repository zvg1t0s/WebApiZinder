namespace RestApiTinderClone.Services
{
    public interface IPhotosService
    {
        Task<Stream> DownloadFileAsync(string FileName);

        Task<string> UploadFileAsync(IFormFile file, string keyName);
        
    }
}
