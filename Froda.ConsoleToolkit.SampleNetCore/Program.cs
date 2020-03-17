using System;
using System.Threading.Tasks;
using Froda.ConsoleToolkit;
using Froda.ConsoleToolkit.Commands.Base;
using Microsoft.Extensions.DependencyInjection;

namespace Froda.ConsoleToolkit.SampleNetCore
{
    public interface IService
    {
        
    }

    public class MyService : IService
    {
        
    }
    
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IService, MyService>();
            
            var resolver = new Resolver(services);
            
            var rootCommand = new RootCommand();

            rootCommand.Register<SessionsContainer>();

            var engine = new ConsoleEngine(rootCommand, resolver);

            await engine.RunAsync();
        }
    }


    public class Resolver : IResolver
    {
        private readonly ServiceCollection _collection;
        private  IServiceProvider _provider;

        public Resolver(ServiceCollection collection) {
            _collection = collection;
        }

        public T Resolve<T>() {
            if(_provider == null)
                _provider = _collection.BuildServiceProvider();

            return _provider.GetService<T>();
        }

        public object Resolve(Type type) {
            if(_provider == null)
                _provider = _collection.BuildServiceProvider();
            return _provider.GetService(type);
        }

        public void Register<TCommand>() where TCommand : CommandBase {
            _collection.AddSingleton<TCommand>();
            _provider = null;
        }


        public void Register(Type command) {
            _collection.AddSingleton(command);
            _provider = null;
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
        private readonly IService _service;

        public NewReplayCommand(IService service) : base("new")
        {
            _service = service;
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