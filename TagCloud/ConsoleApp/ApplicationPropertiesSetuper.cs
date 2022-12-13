using System.Drawing;
using TagCloud;

namespace ConsoleApp;

public class ApplicationPropertiesSetuper
{
    private readonly ConsoleOptions consoleOptions;

    public ApplicationPropertiesSetuper(ConsoleOptions consoleOptions)
    {
        this.consoleOptions = consoleOptions;
    }

    public void Setup(ApplicationProperties properties, IWordsParser wordsParser)
    {
        SetupApplySizeOption(properties.SizeProperties, properties.CloudProperties);
        SetupFontOption(properties.FontProperties);
        SetupFileOption(properties);
        var cloudProperties = properties.CloudProperties;
        cloudProperties.Density = consoleOptions.Density;
        if (consoleOptions.ExcludedWords is not null)
            cloudProperties.ExcludedWords = wordsParser.Parse(consoleOptions.ExcludedWords);
        var palette = properties.Palette;
        palette.Background = ColorTranslator.FromHtml(consoleOptions.BackgroundColor);
        palette.Foreground = ColorTranslator.FromHtml(consoleOptions.ForegroundColor);
    }

    private void SetupApplySizeOption(SizeProperties sizeProperties, CloudProperties cloudProperties)
    {
        sizeProperties.ImageSize = new Size(consoleOptions.Width, consoleOptions.Height);
        cloudProperties.Center = sizeProperties.ImageCenter;
    }

    private void SetupFontOption(FontProperties fontProperties)
    {
        fontProperties.Family = new FontFamily(consoleOptions.FontName);
        fontProperties.MinSize = consoleOptions.MinFont;
        fontProperties.MaxSize = consoleOptions.MaxFont;
    }

    private void SetupFileOption(ApplicationProperties properties)
    {
        if (consoleOptions.File is not null)
            properties.Path = consoleOptions.File;

        if (Path.GetExtension(consoleOptions.OutputPath) is not (".jpg" or ".jpeg" or ".png"))
            throw new ArgumentException("Unsupported image format in path");
    }
}