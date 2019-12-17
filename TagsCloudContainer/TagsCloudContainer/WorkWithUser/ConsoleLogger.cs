using System;

namespace TagsCloudContainer
{
    public class ConsoleLogger : ILogger
    {
        public ConsoleLogger() { }

        public void LogOut(string s) => Console.WriteLine(s);
    }
}