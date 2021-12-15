using System;

namespace TagCloudConsoleClient
{
    public static class Program
    { 
        public static void Main(string[] args)
        {
            var client = new ConsoleClient(Console.Out);
            client.Start(args);
        }
    }
}