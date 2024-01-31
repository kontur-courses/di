using CommandLine;

namespace ConsoleApp.Options;

[Verb("generate", HelpText = "Предобработка слов")]
public class GenerateCloudOptions: IOptions
{
    [Option('i', "input", Required = true, HelpText = "Путь к файлу текста для анализа.")]
    public string InputFile { get; set; }
    
    [Option('o', "output", Required = true, HelpText = "Путь к сохранению изображения.")]
    public string OutputFile { get; set; }
    
    [Option('p', "params", HelpText = "Параметры вывода MyStem")]
    public string AnalyseParameters { get; set; }
    
    [Value(1, Max = 14, HelpText = "Части речи, которые буду задействованы при анализе.")] 
    public IEnumerable<string> ValidSpeechParts { get; set; } = new string[0];
}