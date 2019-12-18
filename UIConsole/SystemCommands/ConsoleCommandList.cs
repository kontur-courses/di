using System.Collections.Generic;
using System.Text;

namespace UIConsole.SystemCommands
{
    internal class ConsoleCommandList : IConsoleCommand
    {
        public string Name => "l-cmd";

        public List<string> ArgsName => new List<string>();

        public string Description => "Выводит список возможных команд";

        public void Execute(ConsoleUserInterface console, Dictionary<string, object> args)
        {
            var output = new StringBuilder();
            output.Append("AllCommands: \n \n");
            foreach (var command in console.GetCommandsList())
            {
                output.Append($"{command.Name}");
                for (int i = 0; i < 20 - command.Name.Length; i++)
                    output.Append(" ");
                output.Append($"- {command.Description}\n");
            }

            console.PrintInConsole(output.ToString());
        }
    }
}