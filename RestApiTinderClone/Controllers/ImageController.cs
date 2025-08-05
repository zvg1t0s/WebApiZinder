using Amazon.Util.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiTinderClone.Models;
using RestApiTinderClone.Services;
using System.IO;
namespace RestApiTinderClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private IUsersService _usersService;
        public ImageController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
           await _usersService.AddPhoto(file);
           return Ok(); 
        }
        [HttpGet("download/{keyName}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> DownloadImage(string keyName, string path = "")
        {
            var fullPath = string.IsNullOrEmpty(path)
            ? $"{keyName}"
            : $"{path.TrimEnd('/')}/{keyName}";
            var stream = await _usersService.DownloadPhoto($"{fullPath}");
            return File(stream, "application/octet-stream", keyName);
        }

    }
}
