using Azure.Storage.Blobs;
using AzureStorage.Contract;
using AzureStorage.Extention;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
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
        public async Task UploadBlobAsync(Stream blobStream, string fileName, string title, string description)
        {
            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

            // Create the container and return a container client object

            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            await blobContainerClient.CreateIfNotExistsAsync();
            // Get a reference to a blob
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

            //Set a metadata to a blob
            //await blobClient.SetMetadataAsync(title, description);

            //Upload blob data to Blob Storage
            blobStream.Position = 0;
            await blobClient.UploadAsync(blobStream);

        }

    }
}
