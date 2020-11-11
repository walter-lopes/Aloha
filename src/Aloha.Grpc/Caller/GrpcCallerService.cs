using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace Aloha.Grpc.Caller
{
    public class GrpcCallerService : IGrpcCallerService
    {
        public async Task<TResponse> CallService<TResponse>(string urlGrpc, Func<GrpcChannel, Task<TResponse>> func)
        {
            var channel = GrpcChannel.ForAddress(urlGrpc);

            return await func(channel);
        }

        public async Task CallService<TResponse>(string urlGrpc, Func<GrpcChannel, Task> func)
        {
            var channel = GrpcChannel.ForAddress(urlGrpc);

            await func(channel);
        }
    }
}
