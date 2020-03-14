using System;
using Froda.ConsoleToolkit.Commands.Base;

namespace Froda.ConsoleToolkit
{
    public class TreeBuilder
    {
        private readonly IResolver _resolver;
        private readonly bool _autoRegister;

        public TreeBuilder(IResolver resolver, bool autoRegister)
        {
            _resolver = resolver;
            _autoRegister = autoRegister;
        }

        private CommandBase Resolve(Type command)
        {
            var instance = _resolver.Resolve(command);
            
            if(instance == null && _autoRegister)
                _resolver.Register(command);
            
            return (CommandBase) instance;
        }

        public CommandTree CreateTree(RootCommand rootCommand)
        {
            var tree = new CommandTree();

            foreach (var command in rootCommand.Commands)
            {
                var commandInstance = Resolve(command);

                var node = CreateNode(commandInstance);

                tree.Nodes.Add(node);

                if (node.MaxKeywordLength > tree.MaxKeywordLength)
                    tree.MaxKeywordLength = node.MaxKeywordLength;

                if (node.MaxDescriptionLength > tree.MaxDescriptionLength)
                    tree.MaxDescriptionLength = node.MaxDescriptionLength;
            }

            return tree;
        }

        private CommandTreeNode CreateNode(CommandBase command)
        {
            if (command is ActionCommandBase action)
            {
                return new CommandTreeNode
                {
                    Keyword = action.Keyword,
                    Descrption = action.Description,
                    Command = action,
                    MaxKeywordLength = action.Keyword.Length,
                    MaxDescriptionLength = action.Description?.Length ?? 0
                };
            }

            if (command is ContainerCommandBase container)
            {
                var node = new CommandTreeNode
                {
                    Keyword = container.Keyword,
                    Descrption = container.Description,
                    Command = container,
                    MaxKeywordLength = container.Keyword.Length,
                    MaxDescriptionLength = container.Description?.Length ?? 0
                };

                foreach (var containerCommand in container.Commands)
                {
                    var commandInstance = Resolve(containerCommand);

                    var commandTreeNode = CreateNode(commandInstance);
                    
                    node.SubNodes.Add(commandTreeNode);

                    if (commandTreeNode.MaxKeywordLength > node.MaxKeywordLength)
                        node.MaxKeywordLength = commandTreeNode.MaxKeywordLength;

                    if (commandTreeNode.MaxDescriptionLength > node.MaxDescriptionLength)
                        node.MaxDescriptionLength = commandTreeNode.MaxDescriptionLength;
                }

                return node;
            }

            throw new Exception();
        }
    }
}