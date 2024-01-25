using System.Drawing;
using ConsoleApp.CommandLineParsers.Options;
using TagsCloudContainer.Settings;
using TagsCloudContainer.Visualizers;

namespace ConsoleApp.CommandLineParsers.Handlers;

public class SaveImageOptionsHandler : IOptionsHandler<SaveImageOptions>
{
    private readonly ImageSettings imageSettings;
    private readonly ICloudVisualizer visualizer;


    public SaveImageOptionsHandler(ImageSettings imageSettings, ICloudVisualizer visualizer)
    {
        this.imageSettings = imageSettings;
        this.visualizer = visualizer;
    }

    public void Map(SaveImageOptions options)
    {
        if (options.PrimaryColor != default)
            imageSettings.PrimaryColor = options.PrimaryColor;
        if (options.BackgroundColor != default)
            imageSettings.BackgroundColor = options.BackgroundColor;
        if (options.Width != default && options.Height != default)
            imageSettings.ImageSize = new Size(options.Width, options.Height);
        if (options.Font is not null)
            imageSettings.Font = options.Font;
        imageSettings.File = options.FilePath;
    }

    public void Map(object options)
    {
        if (options is SaveImageOptions opts)
            Map(opts);
        else
            throw new ArgumentException();
    }

    public void Execute()
    {
        visualizer.GenerateImage();
        visualizer.SaveImage();
    }
}