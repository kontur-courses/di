using System.Collections.Generic;

namespace UIConsole
{
    public interface IConsoleCommand
    {
        string Name { get; }
        string Description { get; }
        void Execute(ConsoleUserInterface console, Dictionary<string, object> args);
        List<string> ArgsName { get; }
    }
}