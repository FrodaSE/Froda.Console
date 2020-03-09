using System;
using System.Collections.Generic;
using System.Linq;

namespace Froda.ConsoleToolkit
{
    class AutoCompletionHandler : IAutoCompleteHandler
    {
        private readonly CommandTree _commandTree;

        public AutoCompletionHandler(CommandTree commandTree)
        {
            _commandTree = commandTree;
        }

        // characters to start completion from
        public char[] Separators { get; set; } = {' '};

        // text - The current text entered in the console
        // index - The index of the terminal cursor within {text}
        public string[] GetSuggestions(string text, int index)
        {
            if (string.IsNullOrEmpty(text))
            {
                return _commandTree.Nodes.Select(x => x.Keyword).ToArray();
            }

            CommandTreeNode node = null;
            
            var parts = text
                .Split(' ')
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.ToLower())
                .ToArray();

            for (var i = 0; i < parts.Length; i++)
            {
                var isLast = i == parts.Length - 1;

                var part = parts[i];

                IEnumerable<CommandTreeNode> tNodes;
                
                if (node == null)
                {
                    //Check for matches
                    tNodes = _commandTree.Nodes.Where(x => x.Keyword.StartsWith(part));
                }
                else
                {
                    //Check for matches
                    tNodes = node.SubNodes.Where(x => x.Keyword.StartsWith(part));
                }
                
                //If none found then return no suggestions
                if(!tNodes.Any())
                    return new string[0];

                //If multiple found and we are on the last keyword then print all suggestions
                if (isLast && tNodes.Count() > 1)
                {
                    return tNodes.Select(x => x.Keyword).ToArray();
                }
                //If not on last but multiple matches
                else if (tNodes.Count() > 1)
                {
                    //Check for exact match
                    node = tNodes.SingleOrDefault(x => x.Keyword == part);
                }
                else
                {
                    //Other take the single node
                    var tNode = tNodes.Single();
                        
                    //Check for exact match
                    if (tNode.Keyword == part)
                    {
                        node = tNode;
                    }
                    //If we are on the last, then take the single node and display suggestion
                    else if(isLast)
                    {
                        return new[] {tNode.Keyword};
                    }
                }
                
                if(node == null)
                    return new string[0];

            }
            
            if(node == null)
                return new string[0];

            return node.SubNodes.Select(x => x.Keyword).ToArray();

        }
    }
}