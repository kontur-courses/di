using CommandLine;

namespace ConsoleApp.CommandLineParsers.Options;

[Verb("generate", HelpText = "Предобработка слов")]
public class PreprocessTextOptions: IOptions
{
    [Option('f', "file", Required = true)]
    public string FilePath { get; set; }
    
    [Option('p', "params")]
    public string Parameters { get; set; }
}