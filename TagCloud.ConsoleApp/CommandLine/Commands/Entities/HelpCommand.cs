using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.ConsoleApp.CommandLine.Interfaces;

namespace TagCloud.ConsoleApp.CommandLine.Commands.Entities;

public class HelpCommand : ICommand
{
    public ICommandService CommandService { get; set; }
    public string Trigger => "help";
    
    public bool Execute(string[] parameters)
    {
        if (parameters.Length == 2 && CommandService.TryGetCommand(parameters[0], out var command))
            Console.WriteLine(command.GetHelp());
        else
            Console.WriteLine("Введите любую из команд ниже:\n"
                              + string.Join("\n", CommandService
                                  .GetCommands()
                                  .Select(c => c.Trigger)) 
                              + Environment.NewLine
                              + GetHelp());
        
        return false;
    }

    public string GetHelp()
    {
        return "Получить помощь по зарегистрированным командам, введите --help после команды";
    }
}