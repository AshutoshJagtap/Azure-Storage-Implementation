using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceExtention
    {
        public static IServiceCollection AddBlobContainerClient(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            string _connectionString = configuration["BlobStorage:ConnectionString"];
            string _containerName = configuration["BlobStorage:ContainerName"];

            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);

            // Create the container client object
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            //Create the container if not exists
            blobContainerClient.CreateIfNotExists();

            serviceCollection.AddSingleton(blobContainerClient);

            return serviceCollection;
        }
    }
}
