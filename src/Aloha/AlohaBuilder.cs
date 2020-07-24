using Aloha.Types;
using DryIoc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Aloha
{
    public class AlohaBuilder : IAlohaBuilder
    {
        private readonly ConcurrentDictionary<string, bool> _registry = new ConcurrentDictionary<string, bool>();
        private readonly List<Action<IContainer>> _buildActions;
        private readonly IContainer _container;

        public AlohaBuilder(IContainer container)
        {
            _buildActions = new List<Action<IContainer>>();
            _container = container;
            _container.Register<IStartupInitializer, StartupInitializer>();
        }

        IContainer IAlohaBuilder.Container => _container;

        public static IAlohaBuilder Create(IContainer services)
            => new AlohaBuilder(services);

        public void AddBuildAction(Action<IContainer> execute)
            => _buildActions.Add(execute);

        public void AddInitializer(IInitializer initializer)
            => AddBuildAction(container =>
            {
                var startupInitializer = container.GetService<IStartupInitializer>();
                startupInitializer.AddInitializer(initializer);
            });

        public void AddInitializer<TInitializer>() where TInitializer : IInitializer
            => AddBuildAction(sp =>
            {
                var initializer = sp.GetService<TInitializer>();
                var startupInitializer = sp.GetService<IStartupInitializer>();
                startupInitializer.AddInitializer(initializer);
            });

        public IContainer Build()
        {
            _buildActions.ForEach(a => a(_container));
            return _container;
        }
    }
}
