using System;
using System.IO;
using System.Threading.Tasks;

namespace Aloha.Storages.AmazonS3
{
    public interface IStorageClient : IDisposable
    {
        Task<string> UploadAsync(string storageName, Stream fromStream, string toFilePath,
           bool publicAvailable = false, bool getUrlToDownload = false);

        Task<string> UploadAsync(string storageName, string fromFilePath, string toFilePath,
            bool publicAvailable = false);

        Task<Stream> GetStreamAsync(string storageName, string filePath);

        Task DeleteAsync(string storageName, string filePath);

        Task<string> CreateAsync(string storageName, string domain = "");
    }
}
