//NuGet CommandLineParser

using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using CommandLine;

namespace TagsCloudContainer;

public class CommandLineOptions
{
    [Option('i', "Input", Required = false, HelpText = "Укажите путь до файла", Default = "../../../TextFiles/Text.docx")]
    public string PathToInputFile { get; set; }

    [Option('b', "Boring", Required = false, HelpText = "Укажите путь до файла со скучными словами", Default = "../../../TextFiles/Boring.docx")]
    public string PathToBoringWordsFile { get; set; }// =
        //@"C:\Users\nikit\RiderProjects\di\TagsCloudContainer\TextFiles\Boring.txt";

    [Option('c', "Color", Required = false, HelpText = "Цвет слов")]
    public string Color { get; set; } = "White";

    [Option('f', "FontName", Required = false, HelpText = "Тип шрифта")]
    public string FontName { get; set; } = "Ariel";

    [Option('s', "FontSize", Required = false, HelpText = "Размер шрифта")]
    public int FontSize { get; set; } = 14;

    [Option('x', "CenterX", Required = false, HelpText = "Координата х для центра")]
    public int CenterX { get; set; } = 0;

    [Option('y', "CenterY", Required = false, HelpText = "Координата у для центра")]
    public int CenterY { get; set; } = 0;
    
    [Option('o', "ImageFormat", Required = false, HelpText = "Формат изображения", Default = "png")]
    public string ImageFormat { get; set; }
}