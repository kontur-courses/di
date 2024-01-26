using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;

namespace TagCloud.ConsoleApp.CommandLine.Interfaces;

public interface ICommandService
{
    public void Run();

    public IEnumerable<ICommand> GetCommands();

    public bool TryGetCommand(string name, out ICommand command);
}