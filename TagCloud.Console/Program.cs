using System;
using System.Collections.Generic;
using System.Linq;
using CommandLine;
using TagCloud.Utility;
using TagCloud.Utility.Data;

namespace TagCloud.Console
{
    static class Program
    {
        private static void Main(string[] args)
        {
            
            while (true)
            {
                System.Console.Clear();
                Parser.Default
                    .ParseArguments<Options>(args)
                    .WithParsed(Run);
                System.Console.WriteLine("Waiting for next command... ( write \"exit\" to exit)");
                args = System.Console.ReadLine()?.Split(' ');
                
                if (args.Contains("exit"))
                    break;
            }
        }

        private static void Run(Options options)
        {
            var logger = new Logger();
            TagCloudProgram.Start(options, logger);
            if (logger.Exceptions.Any())
            {
                PrintExceptions(logger.Exceptions);
                logger.Exceptions.Clear();
            }
            else
            {
                System.Console.WriteLine($"Picture saved to {logger.LastRunningOptions.PathToPicture}");
            }
        }

        private static void PrintExceptions(IEnumerable<Exception> exceptions)
        {
            foreach (var exception in exceptions)
                System.Console.WriteLine(exception.Message);
        }
    }
}