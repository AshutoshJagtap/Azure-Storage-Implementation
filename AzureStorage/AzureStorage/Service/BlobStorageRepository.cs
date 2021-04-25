using Azure.Storage.Blobs;
using AzureStorage.Contract;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace AzureStorage.Service
{
    public class BlobStorageRepository : IBlobStorageRepository
    {
        private readonly BlobContainerClient _blobContainerClient;

        public BlobStorageRepository(BlobContainerClient blobContainerClient)
        {
            _blobContainerClient = blobContainerClient;
        }
        public async Task UploadBlobAsync(IFormFile blobFile, string title, string description)
        {
            // Get a reference of blob
            BlobClient blobClient = _blobContainerClient.GetBlobClient(blobFile.FileName);

            if (!blobClient.Exists())
            {
                // full path to file in temp location
                var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                if (blobFile.Length > 0)
                {
                    using (var blobStream = new FileStream(filePath, FileMode.Create))
                    {
                        //Copy stream to temp file
                        await blobFile.CopyToAsync(blobStream);

                        //Set a metadata to a blob
                        //await blobClient.SetMetadataAsync(title, description);

                        //Upload blob data to Blob Storage
                        blobStream.Position = 0;
                        await blobClient.UploadAsync(blobStream);
                    }
                }

                //Delete temp file
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
    }
}
