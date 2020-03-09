using System;
using System.Threading.Tasks;
using Froda.ConsoleToolkit;
using Froda.ConsoleToolkit.Commands.Base;

namespace Froda.ConsoleToolkit.SampleNetCore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var rootCommand = new RootCommand();

            rootCommand.Register<SessionsContainer>();

            var engine = new ConsoleEngine(rootCommand);

            await engine.RunAsync();
        }
    }


    public class SessionsContainer : ContainerCommandBase
    {
        public SessionsContainer() : base("sessions", "Handle sessions")
        {
            Register<NewSessionCommand>();
            Register<ReplaySessionContainer>();
        }
    }

    public class ReplaySessionContainer : ContainerCommandBase
    {
        public ReplaySessionContainer() : base("replay")
        {
            Register<NewReplayCommand>();
            Register<NewsReplayCommand>();
            Register<DeleteReplayCommand>();
        }
    }

    public class NewReplayCommand : ActionCommandBase
    {
        public NewReplayCommand() : base("new")
        {
        }

        public override Task ExecuteAsync()
        {
            var shouldDoSomething = Query<bool>("Should do something");
            var userName = Query("Enter username");
            System.Console.WriteLine("New replay created with username " + userName + " " + shouldDoSomething);

            return Task.CompletedTask;
        }
    }


    public class NewsReplayCommand : ActionCommandBase
    {
        public NewsReplayCommand() : base("news")
        {
        }

        public override Task ExecuteAsync()
        {
            Console.WriteLine("News replay");

            return Task.CompletedTask;
        }
    }


    public class DeleteReplayCommand : ActionCommandBase
    {
        public DeleteReplayCommand() : base("delete")
        {
        }

        public override Task ExecuteAsync()
        {
            Console.WriteLine("Deleting replay");

            return Task.CompletedTask;
        }
    }

    public class NewSessionCommand : ActionCommandBase
    {
        public NewSessionCommand() : base("new")
        {
        }

        public override Task ExecuteAsync()
        {
            System.Console.WriteLine("New session create");

            return Task.CompletedTask;
        }
    }
}