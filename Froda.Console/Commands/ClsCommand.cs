using System.Threading.Tasks;
using Froda.Console.Commands.Base;

namespace Froda.Console.Commands
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