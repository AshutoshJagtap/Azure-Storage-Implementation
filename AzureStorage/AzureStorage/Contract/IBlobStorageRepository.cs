using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AzureStorage.Contract
{
    public interface IBlobStorageRepository
    {
        Task UploadBlobAsync(Stream blobStream, string fileName, string title, string description);  
    }
}
