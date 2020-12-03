using System;
using System.IO;
using TagsCloud.Infrastructure;

namespace TagsCloud.App.Commands
{
    public class DetailedHelpCommand : ICommand
    {
        private readonly Lazy<IClient> client;
        private readonly TextWriter writer;
        public string Name { get; } = "help";
        public DetailedHelpCommand(Lazy<IClient> client, TextWriter writer)
        {
            this.client = client;
            this.writer = writer;
        }

        public string Description { get; } = "help <command>      # prints help for <command>";
        public void Execute(string[] args)
        {
            var commandName = args[0];
            var cmd = client.Value.FindCommandByName(commandName);
            if (cmd != null)
                writer.WriteLine(cmd.Description);
            else
                writer.WriteLine("Not a command " + commandName);
        }
    }
}