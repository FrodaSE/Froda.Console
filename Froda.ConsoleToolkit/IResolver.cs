using System;
using Froda.ConsoleToolkit.Commands.Base;

namespace Froda.ConsoleToolkit
{
    public interface IResolver
    {
        T Resolve<T>();
        object Resolve(Type type);
        void Register<TCommand>() where TCommand : CommandBase;
        void Register(Type command);
    }
}