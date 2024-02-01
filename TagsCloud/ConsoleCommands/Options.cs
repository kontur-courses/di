using System.ComponentModel;
using System.Drawing;
using CommandLine;
using TagsCloud.ConsoleCommands.Conventers;

namespace TagsCloud.ConsoleCommands;

public class Options
{
    [Option('i', "inputFile", Default = "C:\\Users\\hellw\\Desktop\\C#\\di\\TagsCloud\\source\\input.txt",
        HelpText = "Set input file name.")]
    public string InputFile { get; set; }

    [Option('o', "outputFile", Default = "C:\\Users\\hellw\\Desktop\\C#\\di\\TagsCloud\\source\\output.png",
        HelpText = "Set output file name.")]
    public string OutputFile { get; set; }

    [Option('f', "font", Default = "Arial", HelpText = "Set tagsCloud word Font")]
    public string TagsFont { get; set; }

    [Option('s', "imagesize", Default = null, HelpText = "Set output image size")]
    [TypeConverter(typeof(SizeTypeConverter))]
    public Size ImageSize { get; set; }

    [Option('b', "background", Default = "Empty", HelpText = "Set tagsCloud output background color")]
    public string Background { get; set; }

    [Option('t', "parts", Default = new string[] { "sdsd", "sfdfd" }, HelpText = "Word available parts of speech.")]
    public IEnumerable<string> PartsOfSpeech { get; set; }
}