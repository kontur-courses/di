using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;

namespace TagCloud.ConsoleApp.CommandLine.Commands.Entities;

public class ExitCommand : ICommand
{
    public string Trigger => "exit";
    public bool Execute(string[] parameters)
    {
        return true;
    }

    public string GetHelp()
    {
        return GetShortHelp();
    }
    
    public string GetShortHelp()
    {
        return Trigger + " завершить выполнение программы";
    }
}