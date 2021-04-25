using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace AzureStorage.Contract
{
    public interface IBlobStorageRepository
    {
        Task UploadBlobAsync(IFormFile blob, FileStream blobStream, string fileName, string title, string description);  
    }
}
