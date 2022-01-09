using System;

namespace Aloha.Storages.AmazonS3
{
    public class StorageDocument
    {
        public StorageDocument(string bucket, string path, byte[] file, string name = null)
        {
            this.Bucket = bucket;
            this.Path = path;        
            this.Name = string.IsNullOrEmpty(name) ? Guid.NewGuid().ToString() : name;
            this.File = file;
        }
        
        public string Bucket { get; set; }


        public string Name { get; set; }


        public string Path { get; set; }


        public byte[] File { get; set; }


        public string GenerateUrl(string serviceUrl)
        {
            var url = serviceUrl.Replace("https://", string.Empty);

            return $"https://{Bucket}.{url}/{Path}/{Name}";
        }
    }
}