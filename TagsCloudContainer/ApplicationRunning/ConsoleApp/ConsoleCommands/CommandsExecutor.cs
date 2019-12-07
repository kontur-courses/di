using System;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.ApplicationRunning.ConsoleApp.ConsoleCommands
{
    public class CommandsExecutor
    {
        private readonly IConsoleCommand[] commands;

        public CommandsExecutor(IConsoleCommand[] commands)
        {
            this.commands = commands;
        }

        private void GetHelp()
        {
            var helpInfo = commands.Select(c => $"{c.Name}: {c.Description}\n{c.Name} {c.Arguments}");
            Console.WriteLine("Command: description\nusage\n\r");
            foreach (var info in helpInfo)
                Console.WriteLine(info);
        }
        
        private IConsoleCommand TryToFindCommandByName(string name)
        {
            return commands.FirstOrDefault(a => string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public void Execute(string[] args)
        {
            if(args.Length == 0)
                Console.WriteLine("No commands. Try 'help' for help on commands.");
            var commandName = args[0];
            var command = TryToFindCommandByName(commandName);
            if (string.Equals(commandName, "help", StringComparison.OrdinalIgnoreCase))
            {
                GetHelp();
                return;
            }
            if(command is null)
                Console.WriteLine($"No command '{commandName}' found. Try 'help' for help on commands.");
            else
                command.Act(args.Skip(1).ToArray());
        }
    }
}