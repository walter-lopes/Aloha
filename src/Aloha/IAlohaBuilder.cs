using Aloha.Types;

using Microsoft.Extensions.DependencyInjection;
using System;

namespace Aloha
{
    public interface IAlohaBuilder
    {
        IServiceCollection Services { get; }
        bool TryRegister(string name);
        void AddBuildAction(Action<IServiceProvider> execute);
        void AddInitializer(IInitializer initializer);
        void AddInitializer<TInitializer>() where TInitializer : IInitializer;
        IServiceProvider Build();
    }
}
