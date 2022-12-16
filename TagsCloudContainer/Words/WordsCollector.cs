using TagsCloudContainer.WordsInterfaces;

namespace TagsCloudContainer;

public class WordsCollector : IWordsCollector
{
    private readonly IWordsAnalyzer _wordsAnalyzer;
    private readonly IWordsFrequencyCounter _wordsFrequencyCounter;
    private readonly IWordsReader _wordsReader;

    public WordsCollector(IWordsReader wordsReader, IWordsAnalyzer wordsAnalyzer,
        IWordsFrequencyCounter wordsFrequencyCounter)
    {
        _wordsReader = wordsReader;
        _wordsAnalyzer = wordsAnalyzer;
        _wordsFrequencyCounter = wordsFrequencyCounter;
    }

    public (Dictionary<string, double>, int) Collect(string? path, HashSet<string> boringWords,
        HashSet<string> spPartToIgnore)
    {
        try
        {
            var words = _wordsReader.Read(path);
            var analyzedWords = _wordsAnalyzer.Analyze(words, boringWords, spPartToIgnore);
            return (_wordsFrequencyCounter.Count(analyzedWords), words.Count);
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}