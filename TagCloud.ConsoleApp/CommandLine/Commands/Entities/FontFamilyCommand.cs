using Aspose.Drawing;
using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.Domain.Settings;
using TagCloud.Utils.Extensions;

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
        
        if (!parameters[0].TryParseFontFamily(out var fontFamily))
            throw new ArgumentException("Данный шрифт не поддерживается в системе\n" + GetHelp());

        visualizerSettings.Font = new Font(fontFamily, visualizerSettings.Font.Size);

        return false;
    }

    public string GetHelp()
    {
        return GetShortHelp() + Environment.NewLine +
               "Параметры:\n" +
               "string - название шрифта\n" +
               $"Актуальное значение: {visualizerSettings.Font.FontFamily.Name}\n" +
               "Доступные шрифты в системе: " + string.Join(", ", FontFamily.Families.Select(f => f.Name));
    }

    public string GetShortHelp()
    {
        return Trigger + " позволяет настраивать начертание шрифта";
    }
}