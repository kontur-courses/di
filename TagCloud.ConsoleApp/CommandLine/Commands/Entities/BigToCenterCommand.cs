using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.Domain.Settings;

namespace TagCloud.ConsoleApp.CommandLine.Commands.Entities;

public class BigToCenterCommand : ICommand
{
    private readonly LayoutSettings layoutSettings;
    
    public BigToCenterCommand(LayoutSettings layoutSettings)
    {
        this.layoutSettings = layoutSettings;
    }
    
    public string Trigger => "bigcenter";
    
    public bool Execute(string[] parameters)
    {
        if (parameters.Length < 1
            || !int.TryParse(parameters[0], out var parsed)
            || parsed != 0 && parsed != 1)
            throw new ArgumentException(GetHelp());

        layoutSettings.BigToCenter = parsed == 1;
        return false;
    }

    public string GetHelp()
    {
        return GetShortHelp() + Environment.NewLine +
               "Параметры:\n" +
               "int - 1(ближе к центру) или 0(в случайном порядке)\n" +
               $"Актуальное значение {layoutSettings.BigToCenter}";
    }
    
    public string GetShortHelp()
    {
        return Trigger + " позволяет настраивать положение более частых слов";
    }
}