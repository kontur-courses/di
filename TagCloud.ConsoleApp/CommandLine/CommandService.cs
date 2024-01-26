using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.ConsoleApp.CommandLine.Interfaces;

namespace TagCloud.ConsoleApp.CommandLine;

public class CommandService : ICommandService
{
    private readonly Dictionary<string, ICommand> commands = new();
    
    public CommandService(IEnumerable<ICommand> commands)
    {
        foreach (var command in commands) 
            this.commands[command.Trigger] = command;
    }

    public void Run()
    {
        Console.WriteLine("Введите команду");
        var input = Console.ReadLine();

        while (true)
        {
            if (Execute(input))
                return;

            Console.WriteLine(Environment.NewLine + "Введите команду");
            input = Console.ReadLine();
        }
    }

    public IEnumerable<ICommand> GetCommands()
    {
        return commands.Values;
    }

    public bool TryGetCommand(string name, out ICommand command)
    {
        return commands.TryGetValue(name, out command);
    }

    private bool Execute(string input)
    {
        var parameters = input.Split(' ');
        try
        {
            if (parameters.Length == 0 || !commands.TryGetValue(parameters[0], out var command))
                return commands["help"].Execute(parameters);
            
            if (parameters.Length == 2 && parameters[1] == "--help")
                return commands["help"].Execute(parameters);
                
            return command.Execute(parameters.Skip(1).ToArray());
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
        
        return false;
    }
}