using TagCloud.Abstractions;

namespace TagCloud;

public class CountWordsTagger : IWordsTagger
{
    public IEnumerable<ITag> ToTags(IEnumerable<string> words)
    {
        return words
            .GroupBy(w => w)
            .Select(g => new Tag(g.Key, g.Count()))
            .OrderByDescending(t => t.Weight)
            .ToList();
    }
}