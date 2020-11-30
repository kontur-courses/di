using System;
using System.IO;
using System.Linq;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class ConsoleClient : IClient
    {
        private readonly ICommand[] commands;
        private readonly TextWriter writer;

        public ConsoleClient(ICommand[] commands, TextWriter writer)
        {
            this.commands = commands;
            this.writer = writer;
        }

        public string[] GetAvailableCommandName()
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            while (true)
            {
                var line = Console.ReadLine();
                if (line == null || line == "exit") return;
                Execute(line.Split(' '));
            }
        }

        public ICommand FindCommandByName(string name)
        {
            return commands.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        private void Execute(string[] args)
        {
            if (args[0].Length == 0)
            {
                writer.WriteLine("Please specify <command> as the first command line argument");
                return;
            }

            var commandName = args[0];
            var cmd = FindCommandByName(commandName);
            if (cmd == null)
                writer.WriteLine("Sorry. Unknown command {0}", commandName);
            else
                cmd.Execute(args.Skip(1).ToArray());
        }
    }
}