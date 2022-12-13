using TagsCloud.Core.WordFilters;
using TagsCloud.Core.WordReaders;

namespace TagsCloud.Core.TagContainersCreators.TagsPreprocessors;

public class TempPreprocessor : ITagsPreprocessor
{
    private readonly IWordReader wordReader;
    private readonly IWordFilter wordFilter;

    public TempPreprocessor(IWordReader wordReader, IWordFilter wordFilter)
    {
        this.wordReader = wordReader;
        this.wordFilter = wordFilter;
    }

    public IEnumerable<Tag> GetTags(int? count = null)
    {
        var tags = new Dictionary<string, int>();
        var words = wordFilter.Filter(wordReader.ReadWords());

        foreach (var word in words)
        {
            tags.TryAdd(word, 0);
            tags[word]++;
        }

        return tags
            .OrderByDescending(pair => pair.Value)
            .Take(count ?? tags.Count)
            .Select(pair => new Tag(pair.Key, pair.Value));
    }
}