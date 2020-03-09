using System.Threading.Tasks;
using Froda.Console.Commands.Base;

namespace Froda.Console.Commands
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