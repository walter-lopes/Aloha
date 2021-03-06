﻿using Aloha.MessageBrokers.AmazonSQS.Messages;
using Aloha.Types;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Aloha.MessageBrokers.AmazonSQS.Clients
{
    public class AmazonSQSClient : IAmazonSQSClient
    {
        private readonly AmazonSQSOptions _options;
        private readonly IAmazonSQS _amazonSQS;
        private readonly IAlohaSerializer _serializer;

        public AmazonSQSClient(AmazonSQSOptions options, IAlohaSerializer serializer)
        {
            _amazonSQS = new Amazon.SQS.AmazonSQSClient(
                                new BasicAWSCredentials(options.AccessKey, options.SecretKey),
                                new AmazonSQSConfig { ServiceURL = options.ServiceUrl });

            _options = options;
            _serializer = serializer;
        }

        public async Task<bool> DeleteMessagesAsync<T>(List<MessageEntry<T>> messages, string queueUrl)
        {
            var queueEndpoint = GetQueueEndpoint(queueUrl);

            int pageSize = 10;
            int messagesCount = 0;
            int totalMessages = messages.Count();

            IList<Task<DeleteMessageBatchResponse>> responseTasks = new List<Task<DeleteMessageBatchResponse>>();

            while (messagesCount < totalMessages)
            {
                var requests = new List<DeleteMessageBatchRequestEntry>();

                int currentMessagesCount = 0;
                int objIndex = messagesCount;

                while (objIndex < totalMessages && currentMessagesCount < pageSize)
                {
                    var message = messages.ElementAt(objIndex);

                    requests.Add(new DeleteMessageBatchRequestEntry
                    {
                        Id = message.Id,
                        ReceiptHandle = message.ReceiptHandle
                    });

                    currentMessagesCount++;
                    objIndex = messagesCount + currentMessagesCount;
                }

                var request = new DeleteMessageBatchRequest(queueEndpoint, requests);
                responseTasks.Add(_amazonSQS.DeleteMessageBatchAsync(request));

                messagesCount += requests.Count;
            }

            IEnumerable<DeleteMessageBatchResponse> responses = await Task.WhenAll(responseTasks);

            foreach (var response in responses)
            {
                if (response.HttpStatusCode != HttpStatusCode.OK)
                    throw new Exception($"Error deleting message. HttpStatusCode: {response.HttpStatusCode}");

                if (response.Failed?.Any() == true)
                    throw new Exception($"Error deleting message. Code: {response.Failed[0].Code}. Error: {response.Failed[0].Message}");
            }

            return true;
        }

        public async Task<bool> EnqueueBatchAsync<T>(IEnumerable<MessageEntry<T>> messages, string queue = "")
        {
            var queueEndpoint = GetQueueEndpoint(queue);

            int pageSizedLimit = 10;
            int messagesCount = 0;
            int totalMessages = messages.Count();

            var entries = new List<SendMessageBatchRequestEntry>();
            var responseTasks = new List<Task<SendMessageBatchResponse>>();

            int currentMessagesCount = 0;
            int objIndex = messagesCount;

            while (messagesCount < totalMessages)
            {
                while (objIndex < totalMessages && currentMessagesCount < pageSizedLimit)
                {
                    var message = messages.ElementAt(objIndex);

                    string objJson = _serializer.Serialize(message.Message);

                    entries.Add(new SendMessageBatchRequestEntry(message.Id, objJson));

                    currentMessagesCount++;
                    objIndex = messagesCount + currentMessagesCount;
                }

                var request = new SendMessageBatchRequest(queueEndpoint, entries);

                responseTasks.Add(_amazonSQS.SendMessageBatchAsync(request));

                messagesCount += entries.Count;
            }

            IEnumerable<SendMessageBatchResponse> responses = await Task.WhenAll(responseTasks);

            foreach (var response in responses)
            {
                if (response.HttpStatusCode != HttpStatusCode.OK)
                    throw new Exception($"Error sending message. HttpStatusCode: {response.HttpStatusCode}");

                if (response.Failed?.Any() == true)
                    throw new Exception($"Error sending message. Code: {response.Failed[0].Code}. Error: {response.Failed[0].Message}");
            }

            return true;
        }

        public async Task<bool> EnqueueAsync<T>(T obj, string queue = "")
        {
            var queueEndpoint = GetQueueEndpoint(queue);

            string objJson = _serializer.Serialize(obj);

            SendMessageResponse messageResponse = await _amazonSQS.SendMessageAsync(queueEndpoint, objJson);

            if (messageResponse.HttpStatusCode != HttpStatusCode.OK)
                throw new Exception($"Error sending message. HttpStatusCode: {messageResponse.HttpStatusCode}");

            return true;
        }

        public async Task<IEnumerable<MessageEntry<T>>> ReceiveMessageAsync<T>(string queue = "", int maxItems = 1, int timeoutSeconds = 0)
        {
            var queueEndpoint = GetQueueEndpoint(queue);

            var receives = await _amazonSQS.ReceiveMessageAsync(new ReceiveMessageRequest(queueEndpoint)
            {
                MaxNumberOfMessages = (maxItems > 10 ? 10 : maxItems),
                WaitTimeSeconds = timeoutSeconds
            });

            if (receives.HttpStatusCode != HttpStatusCode.OK)
                throw new Exception($"Error receiving message. HttpStatusCode: {receives.HttpStatusCode}");

            IList<MessageEntry<T>> responses = new List<MessageEntry<T>>();

            receives.Messages?.ForEach(message =>
            {
                T messageObj =   _serializer.Deserialize<T>(message.Body);
                responses.Add(new MessageEntry<T>(message.MessageId, messageObj, message.ReceiptHandle));
            });

            return responses;
        }

        private string GetQueueEndpoint(string queue)
        {
            var queueEndpoint = queue != string.Empty
                                ? queue
                                : _options.Endpoint;

            if (queueEndpoint == "")
            {
                throw new ArgumentException("Queue not configured");
            }

            return queueEndpoint;
        }

        public async Task<bool> DeleteMessagesAsync<T>(IEnumerable<MessageEntry<T>> messages, string queueUrl = "") where T : IIdentifiable<string>
        {
            try
            {
                var queueEndpoint = GetQueueEndpoint(queueUrl);

                int pageSize = 10;
                int messagesCount = 0;
                int totalMessages = messages.Count();

                IList<Task<DeleteMessageBatchResponse>> responseTasks = new List<Task<DeleteMessageBatchResponse>>();

                while (messagesCount < totalMessages)
                {
                    var requests = new List<DeleteMessageBatchRequestEntry>();

                    int currentMessagesCount = 0;
                    int objIndex = messagesCount;

                    while (objIndex < totalMessages && currentMessagesCount < pageSize)
                    {
                        var message = messages.ElementAt(objIndex);

                        requests.Add(new DeleteMessageBatchRequestEntry
                        {
                            Id = message.Id,
                            ReceiptHandle = message.ReceiptHandle
                        });

                        currentMessagesCount++;
                        objIndex = messagesCount + currentMessagesCount;
                    }

                    var request = new DeleteMessageBatchRequest(queueEndpoint, requests);
                    var responseTask = _amazonSQS.DeleteMessageBatchAsync(request);
                    responseTasks.Add(responseTask);

                    messagesCount += requests.Count;
                }

                IEnumerable<DeleteMessageBatchResponse> responses = await Task.WhenAll(responseTasks);

                foreach (var response in responses)
                {
                    if (response.HttpStatusCode != HttpStatusCode.OK)
                        throw new Exception($"Error deleting message. HttpStatusCode: {response.HttpStatusCode}");

                    if (response.Failed?.Any() == true)
                        throw new Exception($"Error deleting message. Code: {response.Failed[0].Code}. Error: {response.Failed[0].Message}");
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error to delete messages. {ex.Message}");
            }


            return true;
        }
    }
}
