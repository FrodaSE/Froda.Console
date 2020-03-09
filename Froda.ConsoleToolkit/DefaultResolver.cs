using System;
using Froda.ConsoleToolkit.Commands.Base;

namespace Froda.ConsoleToolkit
{
    public class DefaultResolver : IResolver
    {
        public T Resolve<T>()
        {
            return Activator.CreateInstance<T>();
        }

        public object Resolve(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public void Register<TCommand>() where TCommand : CommandBase
        {
            
        }
    }
}