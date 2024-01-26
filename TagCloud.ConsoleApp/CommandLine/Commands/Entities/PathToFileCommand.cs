using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.Domain.Settings;

namespace TagCloud.ConsoleApp.CommandLine.Commands.Entities;

public class PathToFileCommand : ICommand
{
    private readonly FileSettings fileSettings;

    public PathToFileCommand(FileSettings fileSettings)
    {
        this.fileSettings = fileSettings;
    }
    
    public string Trigger => "path";
    
    public bool Execute(string[] parameters)
    {
        if (parameters.Length < 1)
            throw new ArgumentException(GetHelp());

        fileSettings.OutPathToFile = parameters[0];

        return false;
    }

    public string GetHelp()
    {
        return "Позволяет настраивать путь для сохранения файла\n" +
               "Параметры:\n" +
               "string - pathToFile\n" +
               $"Актуальное значение {fileSettings.OutPathToFile}";
    }
}