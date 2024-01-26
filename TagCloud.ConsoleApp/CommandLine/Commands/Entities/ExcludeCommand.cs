using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.Domain.Settings;

namespace TagCloud.ConsoleApp.CommandLine.Commands.Entities;

public class ExcludeCommand : ICommand
{
    private readonly WordSettings wordSettings;
    
    public ExcludeCommand(WordSettings wordSettings)
    {
        this.wordSettings = wordSettings;
    }
    
    public string Trigger => "exclude";
    public bool Execute(string[] parameters)
    {
        if (parameters.Length < 1)
            throw new ArgumentException(GetHelp());

        wordSettings.Excluded.AddRange(parameters);
        
        return false;
    }

    public string GetHelp()
    {
        return "Позволяет исключить слова из облака тегов\n" +
               "Параметры:\n" +
               "string[] - список слов, которые надо исключить через пробел\n" +
               "Сейчас исключено " + string.Join(", ", wordSettings.Excluded);
    }
}