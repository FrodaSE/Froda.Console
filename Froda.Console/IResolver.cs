using System;

namespace Froda.Console
{
    public interface IResolver
    {
        T Resolve<T>();
        object Resolve(Type type);
    }
}