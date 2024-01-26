using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.Domain.Settings;

namespace TagCloud.ConsoleApp.CommandLine.Commands.Entities;

public class FileNameCommand : ICommand
{
    private readonly FileSettings fileSettings;

    public FileNameCommand(FileSettings fileSettings)
    {
        this.fileSettings = fileSettings;
    }
    
    public string Trigger => "filename";
    
    public bool Execute(string[] parameters)
    {
        if (parameters.Length < 1)
            throw new ArgumentException(GetHelp());

        fileSettings.OutFileName = parameters[0];

        return false;
    }

    public string GetHelp()
    {
        return GetShortHelp() + Environment.NewLine +
               "Параметры:\n" +
               "stirng - filename\n" +
               $"Актуальное значение {fileSettings.OutFileName}";
    }
    
    public string GetShortHelp()
    {
        return Trigger + " позволяет настраивать имя файла при сохранении облака тегов";
    }
}