using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Aloha.Storages.AmazonS3
{
    public class AmazonS3Client : IStorageClient
    {
        private readonly IAmazonS3 _amazonS3;
        private readonly AmazonS3Options _options;

        public AmazonS3Client(IAmazonS3 amazonS3, AmazonS3Options options)
        {
            _amazonS3 = amazonS3;
            _options = options;
        }

        public async Task<string> CreateAsync(string bucketName, string domain = "")
        {
            try
            {
                bucketName = $"{bucketName}.{domain}";

                if (await AmazonS3Util.DoesS3BucketExistV2Async(_amazonS3, bucketName))
                    return bucketName;

                var request = new PutBucketRequest
                {
                    BucketName = bucketName,
                    CannedACL = S3CannedACL.PublicRead
                };

                PutBucketResponse response = await _amazonS3.PutBucketAsync(request);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity"))
                    throw new Exception("Check the provided AWS Credentials.");
                else
                    throw new Exception($"Error occurred. Message: {amazonS3Exception.Message} when writing an object");
            }

            return bucketName;
        }

        public async Task DeleteAsync(string storageName, string filePath)
        {
            DeleteObjectResponse response = await _amazonS3.DeleteObjectAsync(storageName, filePath);

            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new Exception($"Error deleting file. HttpStatusCode: {response.HttpStatusCode}");
        }

        public async Task<Stream> GetStreamAsync(string storageName, string filePath)
        {
            GetObjectResponse response = await _amazonS3.GetObjectAsync(storageName, filePath);

            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new Exception($"Error getting file. HttpStatusCode: {response.HttpStatusCode}");

            return response.ResponseStream;
        }

        public async Task<string> UploadAsync(StorageDocument storageDocument, bool publicAvailable = false)
        {
            var request = new PutObjectRequest
            {
                BucketName = storageDocument.Bucket,
                InputStream = new MemoryStream(storageDocument.File),
                Key = storageDocument.Key
            };

            if (publicAvailable)
                request.CannedACL = S3CannedACL.PublicRead;

            await UploadAsync(request);

            return storageDocument.GenerateUrl(_options.ServiceUrl);
        }

        public async Task<IEnumerable<string>> UploadFilesAsync(IList<StorageDocument> documents, bool publicAvailable = false)
        {
            var urls = new List<string>(documents.Count);

            foreach (var document in documents)
            {
                urls.Add(await UploadAsync(document, publicAvailable));
            }

            return urls;
        }

        public async Task<string> UploadAsync(string storageName, Stream fromStream,
            string toFilePath, bool publicAvailable = false, bool getUrlToDownload = false)
        {
            var request = new PutObjectRequest
            {
                BucketName = storageName,
                InputStream = fromStream,
                Key = toFilePath
            };

            if (publicAvailable)
                request.CannedACL = S3CannedACL.PublicRead;

            if (getUrlToDownload)
            {
                await UploadAsync(request);
                var preSigned = new GetPreSignedUrlRequest
                {
                    BucketName = storageName,
                    Key = toFilePath,
                    Expires = DateTime.Now.AddHours(1),
                    Protocol = Protocol.HTTP
                };

                return _amazonS3.GetPreSignedURL(preSigned);
            }

            return await UploadAsync(request);
        }

        public async Task<string> UploadAsync(string storageName, string fromFilePath, string toFilePath,
            bool publicAvailable = false)
        {
            var request = new PutObjectRequest
            {
                BucketName = storageName,
                FilePath = fromFilePath,
                Key = toFilePath,
                CannedACL = S3CannedACL.PublicRead
            };

            if (publicAvailable)
                request.CannedACL = S3CannedACL.PublicRead;

            return await UploadAsync(request);
        }

        private async Task<string> UploadAsync(PutObjectRequest request)
        {
            PutObjectResponse response = await _amazonS3.PutObjectAsync(request);

            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new Exception($"Error uploading file. HttpStatusCode: {response.HttpStatusCode}");

            return response.ETag;
        }

        public void Dispose()
        {
            _amazonS3.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}