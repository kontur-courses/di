using Aspose.Drawing;
using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.Domain.Settings;

namespace TagCloud.ConsoleApp.CommandLine.Commands.Entities;

public class SizeCommand : ICommand
{
    private readonly LayoutSettings layoutSettings;
    
    public SizeCommand(LayoutSettings layoutSettings)
    {
        this.layoutSettings = layoutSettings;
    }
    
    public string Trigger => "size";
    
    public bool Execute(string[] parameters)
    {
        if (parameters.Length < 2
            || !int.TryParse(parameters[0], out var width)
            || !int.TryParse(parameters[1], out var height))
            throw new ArgumentException(GetHelp());

        layoutSettings.Dimensions = new Size(width, height);

        return false;
    }

    public string GetHelp()
    {
        return GetShortHelp() + Environment.NewLine +
               "Параметры:\n" +
               "int - width\n" +
               "int - height\n" +
               $"Актуальное значение {layoutSettings.Dimensions}";
    }

    public string GetShortHelp()
    {
        return Trigger + " позволяет настраивать размер выходного изображения";
    }
}