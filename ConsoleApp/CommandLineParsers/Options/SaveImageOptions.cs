using System.Drawing;
using CommandLine;

namespace ConsoleApp.CommandLineParsers.Options;

[Verb("image", HelpText = "Настройка изображения")]
public class SaveImageOptions: IOptions
{
    [Option('c', "color")]
    public Color PrimaryColor { get; set; }
    
    [Option('s', "secondcolor")]
    public Color SecondaryColor { get; set; }

    [Option('b', "background")]
    public Color BackgroundColor { get; set; }
    
    [Option('p', "path", Required = true)]
    public string FilePath { get; set; }

    [Option('w', "width")] 
    public int Width { get; set; }

    [Option('h', "height")] 
    public int Height { get; set; }
    
    [Option('f', "font")]
    public Font Font { get; set; }
}