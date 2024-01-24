namespace TagsCloudVisualization.WordsAnalyzers;

public class Tag
{
    public readonly string Word;
    public readonly double Coeff;

    public Tag(string word, double coeff)
    {
        Word = word;
        Coeff = coeff;
    }
}