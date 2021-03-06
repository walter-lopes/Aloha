﻿using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.DependencyInjection;

namespace Aloha.Storages.AmazonS3
{
    public static class Extensions
    {
        private const string SectionName = "amazons3";

        public static IAlohaBuilder AddAmazonS3(this IAlohaBuilder builder, string sectionName = SectionName)
        {
            var options = builder.GetOptions<AmazonS3Options>(sectionName);
            builder.Services.AddSingleton(options);

            IAmazonS3 amazonS3 = new Amazon.S3.AmazonS3Client(
                                    new BasicAWSCredentials(options.AccessKey, options.SecretKey),
                                    new AmazonS3Config { ServiceURL = options.ServiceUrl });

            builder.Services.AddSingleton(amazonS3);

            builder.Services.AddSingleton<IStorageClient, AmazonS3Client>();
          
            return builder;
        }
    }
}
