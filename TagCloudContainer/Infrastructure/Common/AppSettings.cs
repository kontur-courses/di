using CommandLine;

namespace TagCloudContainer.Infrastructure.Common;

public class AppSettings : IAppSettings
{
    [Option('w', "imageWidth", Default = 1000, HelpText = "Width of output image")]
    public int ImageWidth { get; set; }

    [Option('h', "imageHeight", Default = 1000, HelpText = "Height of output image")]
    public int ImageHeight { get; set; }

    [Option('i', "inputFile", Required = true, HelpText = "Path of input text")]
    public string InputPath { get; set; }

    [Option('o', "inputPath", Default = "output", HelpText = "Path of output image")]
    public string OutputPath { get; set; }

    [Option('e', "outputExtension", Default = "png", HelpText = "Extension of output image")]
    public string OutputFormat { get; set; }

    [Option('f', "textFont", Default = "Roboto", HelpText = "Font used to display words")]
    public string FontName { get; set; }
}