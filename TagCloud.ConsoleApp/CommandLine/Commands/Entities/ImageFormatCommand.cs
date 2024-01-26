using Aspose.Drawing.Imaging;
using TagCloud.ConsoleApp.CommandLine.Commands.Interfaces;
using TagCloud.Domain.Settings;
using TagCloud.Utils.Extensions;

namespace TagCloud.ConsoleApp.CommandLine.Commands.Entities;

public class ImageFormatCommand : ICommand
{
    private readonly FileSettings fileSettings;

    public ImageFormatCommand(FileSettings fileSettings)
    {
        this.fileSettings = fileSettings;
    }
    
    public string Trigger => "format";
    public bool Execute(string[] parameters)
    {
        if (parameters.Length != 1
            || !parameters[0].TryConvertToImageFormat(out var format))
            throw new ArgumentException("Данный формат недоступен\n" + GetHelp());

        fileSettings.ImageFormat = format;
        return false;
    }

    public string GetHelp()
    {
        return "Позволяет настраивать начертание шрифта\n" +
               "Параметры:\n" +
               "string - название шрифта\n" +
               $"Актуальное значение: {fileSettings.ImageFormat}\n";
    }
}