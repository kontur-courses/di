using Aspose.Drawing;
using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.Domain.Settings;

namespace TagCloud.ConsoleApp.CommandLine.Commands.Entities;

public class FontSizeCommand : ICommand
{
    private readonly VisualizerSettings visualizerSettings;

    public FontSizeCommand(VisualizerSettings visualizerSettings)
    {
        this.visualizerSettings = visualizerSettings;
    }

    public string Trigger => "fontsize";
    
    public bool Execute(string[] parameters)
    {
        if (parameters.Length == 0 || !float.TryParse(parameters[0], out var parsed))
            throw new ArgumentException(GetHelp());

        visualizerSettings.Font = new Font(visualizerSettings.Font.FontFamily, parsed);

        return false;
    }

    public string GetHelp()
    {
        return GetShortHelp() + Environment.NewLine +
               "Параметры:\n" +
               "float - размер шрифта\n" +
               $"Актуальное значение {visualizerSettings.Font.Size}";
    }

    public string GetShortHelp()
    {
        return Trigger + " позволяет настраивать размер шрифта";
    }
}