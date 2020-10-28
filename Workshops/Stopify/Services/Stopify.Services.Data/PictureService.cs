namespace Stopify.Services.Data
{
    using System.IO;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class PictureService : IPictureService
    {
        private readonly Cloudinary cloudinary;

        public PictureService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public async Task<string> UploadPicture(IFormFile file)
        {
            byte[] fileAsByteArray;

            await using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                fileAsByteArray = ms.ToArray();
            }

            ImageUploadResult uploadResult; 

            await using (var ms = new MemoryStream(fileAsByteArray))
            {
                var uploadParams = new ImageUploadParams()
                {
                    UniqueFilename = true,
                    Folder = "Stopify",
                    File = new FileDescription(file.FileName, ms)
                };

                uploadResult =  await this.cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult.SecureUrl.ToString();
        }
    }
}