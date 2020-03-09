using System.Collections.Generic;
using System.Linq;

namespace Froda.Console
{
    public class CommandTree
    {
        public List<CommandTreeNode> Nodes { get; } = new List<CommandTreeNode>();
        public int MaxKeywordLength { get; set; }
        public int MaxDescriptionLength { get; set; }

        public CommandTreeNode FindNode(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            var parts = input
                .Split(' ')
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.ToLower())
                .ToArray();

            CommandTreeNode node = null;

            for (int i = 0; i < parts.Length; i++)
            {
                var part = parts[i].ToLower();

                if (string.IsNullOrEmpty(part))
                    continue;

                if (node == null)
                {
                    node = Nodes.SingleOrDefault(x => x.Keyword == part);
                }
                else
                {
                    node = node.SubNodes.SingleOrDefault(x => x.Keyword == part);
                }

                if (node == null)
                    return null;
            }

            return node;
        }
    }
}