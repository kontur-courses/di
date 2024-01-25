using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.Domain.Settings;

namespace TagCloud.ConsoleApp.CommandLine.Commands.Entities;

public class PathToFileCommand : ICommand
{
    private readonly PathSettings pathSettings;

    public PathToFileCommand(PathSettings pathSettings)
    {
        this.pathSettings = pathSettings;
    }
    
    public string Trigger => "path";
    
    public bool Execute(string[] parameters)
    {
        if (parameters.Length < 1)
            throw new ArgumentException(GetHelp());

        pathSettings.PathToFile = parameters[0];

        return false;
    }

    public string GetHelp()
    {
        return "Позволяет настраивать путь для сохранения файла\n" +
               "Параметры:\n" +
               "string - pathToFile\n" +
               $"Актуальное значение {pathSettings.PathToFile}";
    }
}