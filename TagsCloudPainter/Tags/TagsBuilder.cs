using TagsCloudPainter.Settings;

namespace TagsCloudPainter.Tags;

public class TagsBuilder : ITagsBuilder
{
    private readonly TagSettings _settings;

    public TagsBuilder(TagSettings settings)
    {
        _settings = settings;
    }

    public List<Tag> GetTags(List<string> words)
    {
        var countedWords = CountWords(words);
        var tags = new List<Tag>();

        foreach (var wordWithCount in countedWords)
        {
            var tagFontSize = GetTagFontSize(_settings.TagFontSize, wordWithCount.Value, countedWords.Count);
            var tag = new Tag(wordWithCount.Key, tagFontSize, wordWithCount.Value);
            tags.Add(tag);
        }

        return tags;
    }

    private static Dictionary<string, int> CountWords(List<string> words)
    {
        var countedWords = new Dictionary<string, int>();

        foreach (var word in words)
            if (!countedWords.TryAdd(word, 1))
                countedWords[word] += 1;

        return countedWords;
    }

    private static float GetTagFontSize(int fontSize, int tagCount, int wordsAmount)
    {
        return (float)tagCount / wordsAmount * fontSize * 100;
    }
}