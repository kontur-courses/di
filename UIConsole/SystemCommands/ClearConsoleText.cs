using System.Collections.Generic;

namespace UIConsole.SystemCommands
{
    internal class ClearConsoleText : IConsoleCommand
    {
        public string Name => "Clear";

        public string Description => "Чистит консоль";

        public List<string> ArgsName => new List<string>();

        public void Execute(ConsoleUserInterface console, Dictionary<string, object> args)
        {
            ConsoleUserInterface.ClearConsole();
            ConsoleUserInterface.PrintInConsole(ConsoleUserInterface.HelloMessage);
        }
    }
}