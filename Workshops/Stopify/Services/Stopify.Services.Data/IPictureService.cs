namespace Stopify.Services.Data
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IPictureService
    {
        Task<string> UploadPictureAsync(IFormFile file);
    }
}
