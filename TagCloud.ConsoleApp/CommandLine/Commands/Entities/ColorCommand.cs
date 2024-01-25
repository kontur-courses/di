using Aspose.Drawing;
using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.Domain.Settings;

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
            || !int.TryParse(parameters[2], out var blue))
            throw new ArgumentException(GetHelp());

        var color = Color.FromArgb(1, red, green, blue);

        visualizerSettings.Color = color;
        return false;
    }

    public string GetHelp()
    {
        return "Позволяет настраивать цвет шрифта\n" +
               "Параметры:\n" +
               "int - red channel\n" +
               "int - green channel\n" +
               "int - blue channel\n" +
               $"Актуальное значение {visualizerSettings.Color}";
    }
}