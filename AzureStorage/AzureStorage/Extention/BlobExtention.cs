using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureStorage.Extention
{
    public static class BlobExtention
    {
        public static async Task SetMetadataAsync(this BlobClient blobClient, string title, string description)
        {
            Dictionary<string, string> metadata = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(title))
            {
                metadata.Add(nameof(title), title);
            }
            if (!string.IsNullOrWhiteSpace(title))
            {
                metadata.Add(nameof(description), description);
            }
            if (metadata.Count > 0)
            {
                await blobClient.SetMetadataAsync(metadata);
            }
        }
    }
}
