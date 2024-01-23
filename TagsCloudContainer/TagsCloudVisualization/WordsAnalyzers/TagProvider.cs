using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.TextReaders;
using TextReader = TagsCloudVisualization.TextReaders.TextReader;

namespace TagsCloudVisualization.WordsAnalyzers;

public class TagProvider
{
    private readonly TextReader textReader;
    
    public TagProvider(TextReader textReader)
    {
        this.textReader = textReader;
    }
    
    public IEnumerable<Tag> GetTags()
    {
        var words = textReader.GetText().GetAllWords();
        var wordsWithFreq = new Dictionary<string, int>();

        foreach (var word in words.Select(x => x.ToLower()).Where(x => x.Length > 3))
        {
            wordsWithFreq[word] = wordsWithFreq.TryGetValue(word, out var value) ? value + 1 : 1;
        }

        var max = wordsWithFreq.Max(x => x.Value);

        return wordsWithFreq.Select(x => new Tag(x.Key, (double)x.Value / max)).OrderByDescending(x => x.Coeff);
    }
}