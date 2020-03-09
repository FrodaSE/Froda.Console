using System.Collections.Generic;
using Froda.Console.Commands.Base;

namespace Froda.Console
{
    public class CommandTreeNode
    {
        public string Keyword { get; set; }
        public CommandBase Command { get; set; }
        public List<CommandTreeNode> SubNodes { get; } = new List<CommandTreeNode>();
        public string Descrption { get; set; }
        public int MaxKeywordLength { get; set; }
        public int MaxDescriptionLength { get; set; }
    }
}