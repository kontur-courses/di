using System;
using System.Collections.Generic;
using System.Text;

namespace TagCloud.Commands
{
    public class HelpCommand : ICommand
    {
        public string CommandId { get; } = "help";
        public string Description { get; } = "Show all commands";

        public ICommand[] Commands { get; set; }

        public void Handle(string[] args)
        {
            foreach (var command in Commands)
            {
                Console.WriteLine(command.CommandId);
                Console.WriteLine($"  {command.Description}");
            }
        }
    }
}
