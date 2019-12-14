using System.Collections.Generic;

namespace UIConsole.SystemCommands
{
    internal class Exit : IConsoleCommand
    {
        public string Name => "Exit";
        public string Description => "Прекращает работу приложения";
        public void Execute(ConsoleUserInterface console, Dictionary<string, object> args)
        {
            console.IsStopped = true;
        }

        public List<string> ArgsName => new List<string>();
    }
}