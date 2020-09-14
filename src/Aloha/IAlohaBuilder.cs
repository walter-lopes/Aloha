using Aloha.Types;
using DryIoc;
using System;

namespace Aloha
{
    public interface IAlohaBuilder
    {
        IContainer Container { get; }

        void AddBuildAction(Action<IContainer> execute);

        bool TryRegister(string name);

        void AddInitializer<TInitializer>() where TInitializer : IInitializer;

        IContainer Build();
    }
}
