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
        public async Task<IActionResult> UploadBlob(IFormFile blobFile, string title, string description)
        {
            await _blobStorageRepository.UploadBlobAsync(blobFile, title, description);
            
            return Ok();
        }
    }
}
