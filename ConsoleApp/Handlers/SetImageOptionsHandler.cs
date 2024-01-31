using ConsoleApp.Options;
using SixLabors.ImageSharp;
using TagsCloudContainer.Settings;

namespace ConsoleApp.Handlers;

public class SetImageOptionsHandler : IOptionsHandler
{
    private readonly IImageSettings imageSettings;

    public SetImageOptionsHandler(IImageSettings imageSettings)
    {
        this.imageSettings = imageSettings;
    }

    private void Map(SetImageOptions options)
    {
        if (options.PrimaryColor != default)
            imageSettings.PrimaryColor = options.PrimaryColor;
        if (options.BackgroundColor != default)
            imageSettings.BackgroundColor = options.BackgroundColor;
        if (options.Width != default)
            imageSettings.ImageSize = new Size(options.Width, imageSettings.ImageSize.Height);
        if (options.Height != default)
            imageSettings.ImageSize = new Size(imageSettings.ImageSize.Width, options.Height);
        if (options.Font is not null)
            imageSettings.TextOptions.Font = options.Font;
    }

    private void Map(IOptions options)
    {
        if (options is SetImageOptions opts)
            Map(opts);
        else
            throw new ArgumentException(nameof(options));
    }

    public bool CanParse(IOptions options)
    {
        return options is SetImageOptions;
    }

    public string WithParsed(IOptions options)
    {
        Map(options);
        return "Настройки изображения установлены.";
    }
}