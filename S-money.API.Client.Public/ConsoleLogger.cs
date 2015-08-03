using System;
using System.Runtime.CompilerServices;

namespace Smoney.API.Client
{
    public class ConsoleLogger : ILogAdapter
    {
        private void Log(string message, [CallerMemberName] string caller = null)
        {
            Console.WriteLine(caller.ToUpper() + " - " + message);
        }

        public void Warn(string message)
        {
            Log(message);
        }

        public void Debug(string message) { }

        public void Trace(string message) { }
    }
}