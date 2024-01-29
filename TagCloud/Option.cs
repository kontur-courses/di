using CommandLine;

namespace TagCloudApplication;

public class Options
{
    [Option('d', "destination", HelpText = "Set destination path.", Default = @"..\..\..\Results")]
    public string DestinationPath { get; set; }
    
    [Option('s', "source", HelpText = "Set source path.", Default = @"..\..\..\Results\text.txt")]
    public string SourcePath { get; set; }
    
    [Option('n', "name", HelpText = "Set name.", Default = "default.png")]
    public string Name { get; set; }
        
    [Option('c', "color", 
        HelpText = """
                   Set color.
                   random - Random colors
                   #F0F0F0 - Color hex code
                   """, 
        Default = "random")]
    public string ColorScheme { get; set; }
    
    [Option('f', "font", HelpText = "Set font.", Default = "Arial")]
    public string Font { get; set; }
    
    [Option("size", HelpText = "Set font size.", Default = 20)]
    public int FontSize { get; set; }

    [Option("unusedParts", 
        HelpText = """
                   Set unused parts of speech.
                   A - прилагательное
                   ADV - наречие
                   ADVPRO - местоименное наречие
                   ANUM - числительное-прилагательное
                   APRO - местоимение-прилагательное
                   COM - часть композита - сложного слова
                   CONJ - союз
                   INTJ - междометие
                   NUM - числительное
                   PART - частица
                   PR - предлог
                   S - существительное
                   SPRO - местоимение-существительное
                   V - глагол
                   """,
        Default = new[] { "PR", "PART", "CONJ", "INTJ" })]
    public string[] UnusedPartsOfSpeech { get; set; }
    
    [Option("density", HelpText = "Set density.", Default = 0.1)]
    public double Density { get; set; }
    
    [Option("width", HelpText = "Set width.", Default = 100)]
    public int Width { get; set; }
    
    
    [Option("height", HelpText = "Set height.", Default = 100)]
    public int Height { get; set; }
}