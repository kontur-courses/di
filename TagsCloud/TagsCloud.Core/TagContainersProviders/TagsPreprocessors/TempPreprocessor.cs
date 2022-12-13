using TagsCloud.Core.WordFilters;
using TagsCloud.Core.WordReaders;

namespace TagsCloud.Core.TagContainersProviders.TagsPreprocessors;

public class TempPreprocessor : ITagsPreprocessor
{
    private readonly IWordReader wordReader;
    private readonly IWordFiltersComposer wordFiltersComposer;

    public TempPreprocessor(IWordReader wordReader, IWordFiltersComposer wordFiltersComposer)
    {
        this.wordReader = wordReader;
        this.wordFiltersComposer = wordFiltersComposer;
    }

    public IEnumerable<Tag> GetTags(int? count = null)
    {
        var tags = new Dictionary<string, int>();
        var words = wordFiltersComposer.Filter(wordReader.ReadWords());

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