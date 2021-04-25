using AzureStorage.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace AzureStorage.Controllers
{
    public class BlobStorageController : Controller
    {
        private readonly IBlobStorageRepository _blobStorageRepository;

        public BlobStorageController(IBlobStorageRepository blobStorageRepository)
        {
            _blobStorageRepository = blobStorageRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UploadBlob()
        {
            return View();
        }

        [HttpPost("UploadBlob")]
        public async Task<IActionResult> UploadBlob(IFormFile blob, string title, string description)
        {
            if (blob.Length > 0)
            {
                // full path to file in temp location
                var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await _blobStorageRepository.UploadBlobAsync(blob,stream, blob.FileName, title, description);
                }
            }
            return Ok();
        }
    }
}
