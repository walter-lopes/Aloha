using Aloha.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Aloha
{
    public class AlohaBuilder : IAlohaBuilder
    {
        private readonly ConcurrentDictionary<string, bool> _registry = new ConcurrentDictionary<string, bool>();
        private readonly List<Action<IServiceProvider>> _buildActions;
        private readonly IServiceCollection _services;
        IServiceCollection IAlohaBuilder.Services => _services;

        public AlohaBuilder(IServiceCollection services)
        {
            _buildActions = new List<Action<IServiceProvider>>();
            _services = services;
            _services.AddSingleton<IStartupInitializer>(new StartupInitializer());
        }

        /// <summary>
        /// Create new instance of Aloha Builder
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IAlohaBuilder Create(IServiceCollection services)
           => new AlohaBuilder(services);

        /// <summary>
        /// Try register new library to app
        /// </summary>
        /// <param name="name">Registry name</param>
        /// <returns></returns>
        public bool TryRegister(string name) => _registry.TryAdd(name, true);

        public void AddBuildAction(Action<IServiceProvider> execute)
            => _buildActions.Add(execute);

        public void AddInitializer(IInitializer initializer)
            => AddBuildAction(sp =>
            {
                var startupInitializer = sp.GetService<IStartupInitializer>();
                startupInitializer.AddInitializer(initializer);
            });

        /// <summary>
        /// Init libraries when app starts 
        /// </summary>
        /// <typeparam name="TInitializer"></typeparam>
        public void AddInitializer<TInitializer>() where TInitializer : IInitializer
            => AddBuildAction(sp =>
            {
                var initializer = sp.GetService<TInitializer>();
                var startupInitializer = sp.GetService<IStartupInitializer>();
                startupInitializer.AddInitializer(initializer);
            });

        /// <summary>
        /// Build all services in service provider
        /// </summary>
        /// <returns></returns>
        public IServiceProvider Build()
        {
            var serviceProvider = _services.BuildServiceProvider();
            _buildActions.ForEach(a => a(serviceProvider));
            return serviceProvider;
        }
    }
}
