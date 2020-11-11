using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace Aloha.Grpc
{
    public interface IGrpcCallerService
    {
        Task<TResponse> CallService<TResponse>(string urlGrpc, Func<GrpcChannel, Task<TResponse>> func);

        Task CallService<TResponse>(string urlGrpc, Func<GrpcChannel, Task> func);
    }
}
