using System.Threading.Tasks;
using Froda.Console.Commands.Base;

namespace Froda.Console.Commands
{
    public class ExitCommand : ActionCommandBase
    {
        public ExitCommand() : base("exit", "Quit the application")
        {
        }

        public override Task ExecuteAsync()
        {
            System.Console.WriteLine("Exiting ...");

            return Task.CompletedTask;
        }
    }
}