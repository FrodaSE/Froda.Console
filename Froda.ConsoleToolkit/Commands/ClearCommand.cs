using System.Threading.Tasks;
using Froda.ConsoleToolkit.Commands.Base;

namespace Froda.ConsoleToolkit.Commands
{
    public class ClearCommand : ActionCommandBase
    {
        public ClearCommand() : base("clear", "Clear the screen")
        {
        }

        public override Task ExecuteAsync()
        {
            System.Console.Clear();

            return Task.CompletedTask;
        }
    }
}