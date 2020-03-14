using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Froda.ConsoleToolkit.Commands;
using Froda.ConsoleToolkit.Commands.Base;

namespace Froda.ConsoleToolkit
{
    public class ConsoleEngine
    {
        private readonly RootCommand _rootCommand;
        private readonly IResolver _resolver;
        private readonly bool _autoRegisterCommands;
        private readonly CommandTree _commandTree;

        public ConsoleEngine(RootCommand rootCommand) : this(rootCommand, new DefaultResolver(), true)
        {
        }

        public ConsoleEngine(RootCommand rootCommand, IResolver resolver) : this(rootCommand, resolver, true)
        {
        }
        
        public ConsoleEngine(RootCommand rootCommand, IResolver resolver, bool autoRegisterCommands)
        {
            _rootCommand = rootCommand;
            _resolver = resolver;
            _autoRegisterCommands = autoRegisterCommands;

            _rootCommand.Register<ExitCommand>(resolver);
            _rootCommand.Register<ClearCommand>(resolver);
            _rootCommand.Register<ClsCommand>(resolver);

            _commandTree = BuildTree();
        }

        private CommandTree BuildTree()
        {
            var builder = new TreeBuilder(_resolver, _autoRegisterCommands);

            return builder.CreateTree(_rootCommand);
        }

        public async Task RunAsync()
        {
            var autoCompletionHandler = new AutoCompletionHandler(_commandTree);


            while (true)
            {
                ReadLine.AutoCompletionHandler = autoCompletionHandler;
                var input = ReadLine.Read("> ");

                if (string.IsNullOrEmpty(input))
                {
                    
                    PrintAvailableCommands(_commandTree.Nodes);

                    continue;
                }

                var node = _commandTree.FindNode(input);

                if (node == null)
                {
                    Console.WriteLine("No command found.");
                    continue;
                }

                if (node.Command is ExitCommand exitCommand)
                {
                    try
                    {
                        await exitCommand.ExecuteAsync();
                    }
                    catch (Exception e)
                    {
                        var color = Console.ForegroundColor;

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Command failed to run ...");
                        Console.WriteLine(e.Message + ": " + e.StackTrace);
                        Console.ForegroundColor = color;
                        throw;
                    }

                    //Exit :) 
                    return;
                }

                if (node.Command is ActionCommandBase action)
                {
                    await action.ExecuteAsync();
                }
                else if (node.Command is ContainerCommandBase container)
                {
                    PrintAvailableCommands(node.SubNodes);
                }
            }
        }

        private void PrintAvailableCommands(IEnumerable<CommandTreeNode> nodes)
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("");
            foreach (var subNode in nodes)
            {
                Console.WriteLine(
                    "{0,-" + (_commandTree.MaxKeywordLength + 5) + "} {1,-" + (_commandTree.MaxDescriptionLength + 5) + ":N1}",
                    subNode.Keyword, subNode.Descrption);
            }

            Console.WriteLine("");
        }
    }
}