using System;
using System.Collections.Generic;
using System.Linq;

namespace Froda.Console.Commands.Base
{
    public abstract class ContainerCommandBase : CommandBase
    {
        public List<Type> Commands { get; } = new List<Type>();

        protected ContainerCommandBase(string keyword, string description = null) : base(keyword, description)
        {
        }

        protected void Register<TCommand>() where TCommand : CommandBase
        {
            var type = typeof(TCommand);

            if (Commands.Any(x => x == type))
                return;

            Commands.Add(type);
        }
    }
}