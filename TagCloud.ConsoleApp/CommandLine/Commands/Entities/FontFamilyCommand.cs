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
        
        FontFamily fontFamily;
        try
        {
            fontFamily = new FontFamily(parameters[0]);
        }
        catch (Exception e)
        {
            throw new ArgumentException(e.Message + Environment.NewLine + GetHelp());
        }

        visualizerSettings.Font = new Font(fontFamily, visualizerSettings.Font.Size);

        return false;
    }

    public string GetHelp()
    {
        return "Позволяет настраивать начертание шрифта\n" +
               "Параметры:\n" +
               "string - название шрифта\n" +
               $"Актуальное значение: {visualizerSettings.Font.FontFamily.Name}\n" +
               "Доступные шрифты в системе: " + string.Join(", ", FontFamily.Families.Select(f => f.Name));
    }
}