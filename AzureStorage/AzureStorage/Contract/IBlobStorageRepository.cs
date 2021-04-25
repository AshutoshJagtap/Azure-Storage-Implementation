using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace AzureStorage.Contract
{
    public interface IBlobStorageRepository
    {
        Task UploadBlobAsync(IFormFile blobFile, string title, string description);  
    }
}
