using System.Threading.Tasks;
using Froda.ConsoleToolkit.Commands.Base;

namespace Froda.ConsoleToolkit.Commands
{
    public class ClsCommand : ActionCommandBase
    {
        public ClsCommand() : base("cls", "Clear the screen")
        {
        }

        public override Task ExecuteAsync()
        {
            System.Console.Clear();

            return Task.CompletedTask;
        }
    }
}