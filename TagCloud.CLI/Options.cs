using System.Drawing;
using System.Drawing.Imaging;
using CommandLine;
using TagCloud.Common.Options;

namespace TagCloud.CLI;

public class Options
{
    [Option('f', "format", Required = false, HelpText = "Created image format (.jpg/.png/.bmp)", Default = ".png")]
    public string SavingImageFormat { get; set; }
    [Option('p', "path", Required = true, HelpText = "Full path to text file")]
    public string PathToTextFile { get; set; }
    [Option('s', "size", Required = false, HelpText = "Size of created image", Default = 600)]
    public int ImageSize { get; set; }
    [Option('b', "boring_words", Required = false, HelpText = "Size of Boring/Ignoring words", Default = 0)]
    public int BoringWordsThreshold { get; set; }
    [Option('l', "font", Required = false, HelpText = "Minimal word's font size", Default =20)]
    public int MinFontSize { get; set; }

    public VisualizationOptions SwitchToVisualizationOptions()
    {
        var imageSize = new Size(ImageSize, ImageSize);
        var format = VisualizationOptions.ConvertToImageFormat(SavingImageFormat);
        return new VisualizationOptions(PathToTextFile, format, imageSize, BoringWordsThreshold, MinFontSize);
    }
}