using System;

namespace Froda.Console
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
    }
}