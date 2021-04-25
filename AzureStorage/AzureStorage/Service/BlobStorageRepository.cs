using Azure.Storage.Blobs;
using AzureStorage.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace AzureStorage.Service
{
    public class BlobStorageRepository : IBlobStorageRepository
    {
        private readonly string _containerName;
        private readonly string _connectionString;
        public BlobStorageRepository(IConfiguration configuration)
        {
            _connectionString = configuration["BlobStorage:ConnectionString"];
            _containerName = configuration["BlobStorage:ContainerName"];
        }
        public async Task UploadBlobAsync(IFormFile blobFile, FileStream blobStream, string fileName, string title, string description)
        {
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

            // Create the container client object
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            //Create the container if not exists
            await blobContainerClient.CreateIfNotExistsAsync();

            // Get a reference of blob
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

            if (!blobClient.Exists())
            {
                //Copy stream to temp file
                await blobFile.CopyToAsync(blobStream);

                //Set a metadata to a blob
                //await blobClient.SetMetadataAsync(title, description);

                //Upload blob data to Blob Storage
                blobStream.Position = 0;
                await blobClient.UploadAsync(blobStream);

                //Delete temp file
                if (File.Exists(blobStream.Name))
                {
                    File.Delete(blobStream.Name);
                }
            }
        }
    }
}
