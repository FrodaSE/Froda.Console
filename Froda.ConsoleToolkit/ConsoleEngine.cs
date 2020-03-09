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
        private readonly CommandTree _commandTree;

        public ConsoleEngine(RootCommand rootCommand) : this(rootCommand, new DefaultResolver())
        {
        }

        public ConsoleEngine(RootCommand rootCommand, IResolver resolver)
        {
            _rootCommand = rootCommand;
            _resolver = resolver;

            _rootCommand.Register<ExitCommand>(resolver);
            _rootCommand.Register<ClearCommand>(resolver);
            _rootCommand.Register<ClsCommand>(resolver);

            _commandTree = BuildTree();
        }

        private CommandTree BuildTree()
        {
            var builder = new TreeBuilder(_resolver);

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
                    System.Console.WriteLine("No command found.");
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
                        var color = System.Console.ForegroundColor;

                        System.Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("Command failed to run ...");
                        System.Console.WriteLine(e.Message + ": " + e.StackTrace);
                        System.Console.ForegroundColor = color;
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
            System.Console.WriteLine("Commands:");
            System.Console.WriteLine("");
            foreach (var subNode in nodes)
            {
                System.Console.WriteLine(
                    "{0,-" + (_commandTree.MaxKeywordLength + 5) + "} {1,-" + (_commandTree.MaxDescriptionLength + 5) + ":N1}",
                    subNode.Keyword, subNode.Descrption);
            }

            System.Console.WriteLine("");
        }
    }
}