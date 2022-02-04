using System;

namespace Aloha.Storages.AmazonS3
{
    public class StorageDocument
    {
        public StorageDocument(string bucket, string path, byte[] file, string name = null)
        {
            Bucket = bucket;
            Path = path;        
            Name = string.IsNullOrEmpty(name) ? Guid.NewGuid().ToString() : name;
            File = file;
            Key = $"{Path}/{Name}";
        }
        
        public string Bucket { get; private set; }


        public string Name { get; private set; }


        public string Path { get; private set; }


        public byte[] File { get; private set; }
        
        
        public string Key { get; private set; }

        public string GenerateUrl(string serviceUrl)
        {
            var url = serviceUrl.Replace("https://", string.Empty);

            return $"https://{Bucket}.{url}/{Key}";
        }
    }
}