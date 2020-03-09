using System;

namespace Froda.Console.Commands.Base
{
    public abstract class CommandBase
    {
        public string Keyword { get; }
        public string Description { get; }

        protected CommandBase(string keyword, string description)
        {
            if (string.IsNullOrEmpty(keyword))
                throw new ArgumentNullException(nameof(keyword), "A keyword is required.");
            
            Description = description;

            Keyword = keyword.ToLower();
        }
    }
}