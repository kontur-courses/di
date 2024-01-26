using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.Domain.Settings;
using TagCloud.Utils.Extensions;

namespace TagCloud.ConsoleApp.CommandLine.Commands.Entities;

public class ColorCommand : ICommand
{
    private readonly VisualizerSettings visualizerSettings;

    public ColorCommand(VisualizerSettings visualizerSettings)
    {
        this.visualizerSettings = visualizerSettings;
    }
    
    public string Trigger => "color";
    
    public bool Execute(string[] parameters)
    {
        if (parameters.Length < 3
            || !int.TryParse(parameters[0], out var red)
            || !int.TryParse(parameters[1], out var green)
            || !int.TryParse(parameters[2], out var blue)
            || !(red, green, blue).TryParseColor(out var color))
            throw new ArgumentException(GetHelp());

        visualizerSettings.Color = color;
        return false;
    }

    public string GetHelp()
    {
        return GetShortHelp() + Environment.NewLine +
               "Параметры:\n" +
               "int - red channel\n" +
               "int - green channel\n" +
               "int - blue channel\n" +
               $"Актуальное значение {visualizerSettings.Color}";
    }
    
    public string GetShortHelp()
    {
        return Trigger + " позволяет настраивать цвет шрифта";
    }
}