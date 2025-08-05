using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
namespace RestApiTinderClone.Services
{
    public class AS3PhotosService : IPhotosService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;
        public AS3PhotosService(IConfiguration configuration)
        {
            var config = new AmazonS3Config
            {
                ServiceURL = configuration["S3Storage:ServiceURL"],
                ForcePathStyle = true,
                AuthenticationRegion = configuration["S3Storage:Region"],
            };
            _s3Client = new AmazonS3Client(
                    configuration["S3Storage:AccessKey"],
                    configuration["S3Storage:SecretKey"],
                    config

                );
            _bucketName = configuration["S3Storage:BucketName"];

        }
        public async Task<Stream> DownloadFileAsync(string keyName)
        {
            var request = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = keyName
            };

            var response = await _s3Client.GetObjectAsync(request);
            return response.ResponseStream;
        }
        public async Task<string> UploadFileAsync(IFormFile file, string keyName)
        {
            using MemoryStream memoryStream = new MemoryStream();
            file.CopyTo(memoryStream); 

            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = keyName,
                InputStream = memoryStream, 
                UseChunkEncoding = false,
            };

            await _s3Client.PutObjectAsync(request);

            return keyName;
        }
    }

}
