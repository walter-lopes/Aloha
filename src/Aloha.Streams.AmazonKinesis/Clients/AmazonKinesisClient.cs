using Amazon;
using Amazon.KinesisFirehose;
using Amazon.KinesisFirehose.Model;
using Amazon.Runtime;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aloha.Streams.AmazonKinesis.Clients
{
    public class AmazonKinesisClient
    {
        private readonly IAmazonKinesisFirehose _client;
        private readonly IAlohaSerializer _serializer;

        public AmazonKinesisClient(AmazonKinesisOptions options, IAlohaSerializer serializer)
        {
            var region = RegionEndpoint.GetBySystemName(options.Region);

            _client = new AmazonKinesisFirehoseClient(
                new BasicAWSCredentials(options.AccessKey, options.SecretKey),
                new AmazonKinesisFirehoseConfig { RegionEndpoint = region });

            _serializer = serializer;
        }

        public async Task<bool> RecordAsync<T>(T @event, string streamName)
        {
            string objAsJson = _serializer.Serialize(@event);

            byte[] objAsBytes = Encoding.UTF8.GetBytes(objAsJson + "\n");

            using (var ms = new MemoryStream(objAsBytes))
            {
                var record = new Record { Data = ms };

                var request = new PutRecordRequest
                {
                    DeliveryStreamName = streamName,
                    Record = record
                };

                PutRecordResponse response = await _client.PutRecordAsync(request);

                if (response.HttpStatusCode != HttpStatusCode.OK)
                    throw new System.Exception($"Error sending message. HttpStatusCode: {response.HttpStatusCode}");
            }

            return true;
        }

        public async Task<bool> RecordBatchAsync<T>(IEnumerable<T> events, string streamName)
        {
            if (!events.Any())
                return false;

            var records = new List<Record>();

            foreach (T obj in events)
            {
                string objAsJson = _serializer.Serialize(events);
                byte[] objAsBytes = Encoding.UTF8.GetBytes(objAsJson + "\n");

                using MemoryStream ms = new MemoryStream(objAsBytes);
                Record record = new Record { Data = ms };
                records.Add(record);
            }

            var request = new PutRecordBatchRequest
            {
                DeliveryStreamName = streamName,
                Records = records
            };

            PutRecordBatchResponse response = await _client.PutRecordBatchAsync(request);

            if (response.HttpStatusCode != HttpStatusCode.OK)
                throw new System.Exception($"Error sending message. HttpStatusCode: {response.HttpStatusCode}");

            return true;
        }
    }
}
