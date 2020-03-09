using System;
using System.Collections.Generic;
using System.Linq;

namespace Froda.Console.Commands.Base
{
    public class RootCommand
    {
        public List<Type> Commands { get; } = new List<Type>();


        public void Register<TCommand>() where TCommand : CommandBase
        {
            var type = typeof(TCommand);

            if (Commands.Any(x => x == type))
                return;

            Commands.Add(type);
        }
    }
}