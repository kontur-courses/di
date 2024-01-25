using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.Domain.Settings;

namespace TagCloud.ConsoleApp.CommandLine.Commands.Entities;

public class FileNameCommand : ICommand
{
    private readonly PathSettings pathSettings;

    public FileNameCommand(PathSettings pathSettings)
    {
        this.pathSettings = pathSettings;
    }
    
    public string Trigger => "filename";
    
    public bool Execute(string[] parameters)
    {
        if (parameters.Length < 1)
            throw new ArgumentException(GetHelp());

        pathSettings.FileName = parameters[0] + ".png";

        return false;
    }

    public string GetHelp()
    {
        return "Позволяет настраивать имя файла при сохранении облака тегов\n" +
               "Параметры:\n" +
               "stirng - filename\n" +
               $"Актуальное значение {pathSettings.FileName}";
    }
}