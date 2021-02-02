using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Froda.ConsoleToolkit.Commands.Base
{
    public abstract class ActionCommandBase : CommandBase
    {
        protected ActionCommandBase(string keyword, string description = null) : base(keyword, description)
        {
        }

        protected string Query(string message)
        {
            return Input.Query(message);
        }

        protected string Query(string message, string[] selectionOptions)
        {
            return Input.Query<string>(message, selectionOptions);
        }

        protected T Query<T>(string message)
        {
            return Input.Query<T>(message);
        }
        
        protected T Query<T>(string message, string[] selectionOptions)
        {
            return Input.Query<T>(message, selectionOptions);
        }


        public abstract Task ExecuteAsync();
    }

    public class SimpleAutoCompletionHandler : IAutoCompleteHandler
    {
        private readonly string[] _options;

        public SimpleAutoCompletionHandler(params string[] options)
        {
            _options = options;
        }

        public string[] GetSuggestions(string text, int index)
        {
            if (!string.IsNullOrEmpty(text))
            {
                foreach (var option in _options)
                {
                    if(text.StartsWith(option))
                        return new string[0];
                }
            }
            
            return _options;
        }

        public char[] Separators { get; set; } = new[] {' '};
    }
}