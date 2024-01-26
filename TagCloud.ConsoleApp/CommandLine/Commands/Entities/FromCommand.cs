using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.Domain.Settings;

namespace TagCloud.ConsoleApp.CommandLine.Commands.Entities;

public class FromCommand : ICommand
{
    private readonly FileSettings fileSettings;

    public FromCommand(FileSettings fileSettings)
    {
        this.fileSettings = fileSettings;
    }
    
    public string Trigger => "from";
    public bool Execute(string[] parameters)
    {
        if (parameters.Length != 1)
            throw new ArgumentException(GetHelp());

        fileSettings.FileFromWithPath = parameters[0];

        return false;
    }

    public string GetHelp()
    {
        return GetShortHelp() + Environment.NewLine +
               "Параметры:\n" +
               "stirng - pathToFIle\n" +
               $"Актуальное значение {fileSettings.FileFromWithPath}";
    }

    public string GetShortHelp()
    {
        return Trigger + " позволяет указывать файл, из которого брать слова для облака тегов";
    }
}