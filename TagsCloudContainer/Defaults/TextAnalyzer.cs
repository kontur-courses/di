using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class TextAnalyzer : ITextAnalyzer
{
    private readonly ITextReader textReader;
    private readonly IWordNormalizer wordNormalizer;
    private readonly IWordFilter wordFilter;
    private readonly char[] wordSeparators;

    public TextAnalyzer(ITextReader textReader, IWordNormalizer wordNormalizer, IWordFilter wordFilter, char[] wordSeparators)
    {
        this.textReader = textReader;
        this.wordNormalizer = wordNormalizer;
        this.wordFilter = wordFilter;
        this.wordSeparators = wordSeparators;
    }

    public ITextStats AnalyzeText()
    {
        var result = new TextStats();
        var count = 0;
        foreach (var line in textReader.ReadLines())
        {
            var words = line
                .Split(wordSeparators)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(wordNormalizer.Normalize)
                .Where(wordFilter.IsValid);
            foreach (var word in words)
            {
                result.UpdateWord(word);
                count++;
            }
        }

        result.SetCount(count);

        return result;
    }
}
