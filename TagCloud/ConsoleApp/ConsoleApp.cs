using System;
using System.IO;
using CommandLine;
using TagCloud.TextHandlers;

namespace TagCloud.ConsoleApp
{
    public class ConsoleApp : IApp
    {
        private IReader reader;

        public ConsoleApp(IReader reader)
        {
            this.reader = reader;
        }

        public void Run(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    if (!File.Exists(o.Filename))
                    {
                        Console.WriteLine($"File {o.Filename} does not exist!");
                        return;
                    }

                    var words = reader.Read(o.Filename);
                });
        }
    }
}