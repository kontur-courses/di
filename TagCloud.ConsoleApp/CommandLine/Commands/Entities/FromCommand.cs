using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.Domain.Settings;

namespace TagCloud.ConsoleApp.CommandLine.Commands.Entities;

public class FromCommand : ICommand
{
    private readonly PathSettings pathSettings;

    public FromCommand(PathSettings pathSettings)
    {
        this.pathSettings = pathSettings;
    }
    
    public string Trigger => "from";
    public bool Execute(string[] parameters)
    {
        if (parameters.Length != 1)
            throw new ArgumentException(GetHelp());

        pathSettings.FileFromWithPath = parameters[0];

        return false;
    }

    public string GetHelp()
    {
        return "Позволяет указывать файл, из которого брать слова для облака тегов\n" +
               "Параметры:\n" +
               "stirng - pathToFIle\n" +
               $"Актуальное значение {pathSettings.FileFromWithPath}";
    }
}