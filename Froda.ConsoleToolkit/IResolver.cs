using System;

namespace Froda.ConsoleToolkit
{
    public interface IResolver
    {
        T Resolve<T>();
        object Resolve(Type type);
    }
}