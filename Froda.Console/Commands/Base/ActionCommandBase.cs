using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Froda.Console.Commands.Base
{
    public abstract class ActionCommandBase : CommandBase
    {
        protected ActionCommandBase(string keyword, string description = null) : base(keyword, description)
        {
        }

        protected string Query(string message)
        {
            return Query<string>(message);
        }

        protected T Query<T>(string message)
        {
            string[] selectionOptions = null;

            if (typeof(T) == typeof(bool) || typeof(T) == typeof(bool?))
                selectionOptions = new[] {false.ToString(), true.ToString()};
                
            
            return Query<T>(message, selectionOptions);
        }
        
        protected T Query<T>(string message, string[] selectionOptions)
        {
            if (selectionOptions != null && selectionOptions.Any())
            {
                ReadLine.AutoCompletionHandler = new SimpleAutoCompletionHandler(selectionOptions);
            }
            else
            {
                ReadLine.AutoCompletionHandler = null;
            }
            
            var input = ReadLine.Read(message + ": ");

            return (T) TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(input);
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