namespace TagCloud.WordsPreprocessing.TextAnalyzers
{
    public interface ITextAnalyzer
    {
        Word[] GetWords();
    }
}