using Froda.ConsoleToolkit.Commands.Base;
using System;
using System.ComponentModel;
using System.Linq;

namespace Froda.ConsoleToolkit
{
    public static class Input
    {
        public static string Query(string message)
        {
            return Query<string>(message);
        }

        public static string Query(string message, string[] selectionOptions)
        {
            return Query<string>(message, selectionOptions);
        }

        public static T Query<T>(string message)
        {
            string[] selectionOptions = null;

            if (typeof(T) == typeof(bool) || typeof(T) == typeof(bool?))
                selectionOptions = new[] { false.ToString(), true.ToString() };


            return Query<T>(message, selectionOptions);
        }

        public static T Query<T>(string message, string[] selectionOptions)
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

            return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(input);
        }
        
        public static string QueryPassword(string message = "Password: ") 
        {
            return ReadLine.ReadPassword(message);
        }
    }
}
