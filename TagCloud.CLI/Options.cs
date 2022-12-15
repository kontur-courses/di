using System.Drawing;
using CommandLine;
using TagCloud.Common.Options;

namespace TagCloud.CLI;

public class Options
{
    [Option('f', "format", Required = false, HelpText = "Created image format (.jpg/.png/.bmp)", Default = ".png")]
    public string SavingImageFormat { get; set; }

    [Option('p', "path", Required = true, HelpText = "Full path to text file")]
    public string PathToTextFile { get; set; }

    [Option('n', "ImageName", Required = false, HelpText = "Name of saving image", Default = "Randomize")]
    public string FileName { get; set; }

    [Option('s', "size", Required = false, HelpText = "Size of created image", Default = 600)]
    public int ImageSize { get; set; }

    [Option('b', "BoringWordsSize", Required = false, HelpText = "Size of Boring/Ignoring words", Default = 0)]
    public int BoringWordsThreshold { get; set; }

    [Option('l', "font", Required = false, HelpText = "Minimal word's font size", Default = 20)]
    public int MinFontSize { get; set; }

    [Option("SavePath", Required = false, HelpText = "Where you want to save file", Default = "Text file directory")]
    public string SavePath { get; set; }

    [Option('g', "BGColor", Required = false, HelpText = "Image background color", Default = "Black")]
    public string BackgroundColor { get; set; }

    [Option('c', "TextColor", Required = false, HelpText = "Image text color", Default = "Red")]
    public string TextColor { get; set; }

    public VisualizationOptions MapToVisualizationOptions()
    {
        var wordsOptions = new WordsOptions(BoringWordsThreshold, MinFontSize, PathToTextFile);
        var savingOptions = MapToSavingOptions();
        var drawingOptions = MapToDrawingOptions();

        return new VisualizationOptions(wordsOptions, drawingOptions, savingOptions);
    }

    private SavingOptions MapToSavingOptions()
    {
        var random = new Random();
        var format = SavingOptions.ConvertToImageFormat(SavingImageFormat);
        var savePath = SavePath == "Text file directory" ? Path.GetDirectoryName(PathToTextFile)! : SavePath;
        var fileName = FileName == "Randomize" ? random.Next(1000).ToString() : FileName;
        return new SavingOptions(savePath, fileName, format);
    }

    private DrawingOptions MapToDrawingOptions()
    {
        var textColor = Color.FromName(TextColor);
        var imageSize = new Size(ImageSize, ImageSize);
        var bgColor = Color.FromName(BackgroundColor);
        return new DrawingOptions(bgColor, imageSize, textColor);
    }
}