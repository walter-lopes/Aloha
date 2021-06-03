using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Aloha.CQRS.Queries.Dispatchers
{
    internal sealed class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> QueryAsync<TResponse>(IQuery<TResponse> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
            dynamic handler = _serviceProvider.GetRequiredService(handlerType);
            return await handler.HandleAsync((dynamic)query);
        }

        public async Task<TResponse> QueryAsync<TQuery, TResponse>(TQuery query) where TQuery : class, IQuery<TResponse>
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResponse>>();
            return await handler.HandleAsync(query);
        }
    }
}
