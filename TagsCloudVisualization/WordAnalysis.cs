namespace TagsCloudVisualization;

public class WordAnalysis
{
    public string Word { get; set; }
    public string? Lexema { get; set; }
    public string? GrammarAnalysis { get; set; }

    public WordAnalysis(string word, string? lexema = null, string? grammarAnalysis = null)
    {
        Word = word;
        Lexema = lexema;
        GrammarAnalysis = grammarAnalysis;
    }
}