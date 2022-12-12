using FluentAssertions;
using TagsCloud2.Lemmatizer;

namespace TestsTagsCloud;

public class LemmatizerTests
{
    [Test]
    public void Lemmatize_Normalization()
    {
        var mystemExePath = @"D:\шпора-2022\di\TagsCloud2\Lemmatizer\mystem.exe";
        var lemmatizer = new Lemmatizer(mystemExePath);

        var words = new List<string>();
        words.Add("мама");
        words.Add("чистила");
        words.Add("раму");
        var normalizeWords = lemmatizer.Lemmatize(words);

        normalizeWords[0].Should().Be("мама");
        normalizeWords[1].Should().Be("чистить");
        normalizeWords[2].Should().Be("рама");
    }
    
    [Test]
    public void Lemmatize_DeleteSlugWords()
    {
        var mystemExePath = @"D:\шпора-2022\di\TagsCloud2\Lemmatizer\mystem.exe";
        var lemmatizer = new Lemmatizer(mystemExePath);

        var words = new List<string>();
        words.Add("я");
        words.Add("их");
        words.Add("который");
        var normalizeWords = lemmatizer.Lemmatize(words);

        normalizeWords.Count.Should().Be(0);
    }
}