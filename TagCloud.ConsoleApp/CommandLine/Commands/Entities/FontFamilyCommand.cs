using Aspose.Drawing;
using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.Domain.Settings;

namespace TagCloud.ConsoleApp.CommandLine.Commands.Entities;

public class FontFamilyCommand : ICommand
{
    private readonly VisualizerSettings visualizerSettings;

    public FontFamilyCommand(VisualizerSettings visualizerSettings)
    {
        this.visualizerSettings = visualizerSettings;
    }
    
    public string Trigger => "fontfamily";
    
    public bool Execute(string[] parameters)
    {
        if (parameters.Length == 0)
            throw new ArgumentException("Введите название шрифта");

        var font = new Font(parameters[0], visualizerSettings.Font.Size);
        
        if (font.IsSystemFont)
            throw new ArgumentException($"Шрифт {parameters[0]} не является системным");

        visualizerSettings.Font = font;

        return false;
    }

    public string GetHelp()
    {
        return "Позволяет настраивать начертание шрифта\n" +
               "Параметры:\n" +
               "string - название шрифта\n" +
               $"Актуальное значение {visualizerSettings.Font.FontFamily.Name}";
    }
}