using Aloha.Types;
using DryIoc;
using System;

namespace Aloha
{
    public interface IAlohaBuilder
    {
        IContainer Services { get; }

        void AddBuildAction(Action<IContainer> execute);

        void AddInitializer<TInitializer>() where TInitializer : IInitializer;

        IContainer Build();
    }
}
