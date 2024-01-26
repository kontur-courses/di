using TagCloud.Domain.Settings;
using TagCloud.Domain.WordProcessing.Extractors;

namespace TagCloud.Tests;

public class ExtractorsTests
{
    [SetUp]
    public void SetUp()
    {
        var wordSettings = new WordSettings();
        wordSettings.Excluded.AddRange(new [] {"a", "b", "c"});
        boringLengthExtractor = new BoringLengthExtractor();
        excludeExtractor = new ExcludeExtractor(wordSettings);
    }

    private BoringLengthExtractor boringLengthExtractor;
    private ExcludeExtractor excludeExtractor;

    [Test]
    public void BoringLengthExtractor_ExtractsShortWords()
    {
        new[] {"a", "ab", "abc", "abcd"}
            .Select(e => (boringLengthExtractor.IsSuitable(e), e))
            .Should()
            .BeEquivalentTo(new[]
            {
                (false, "a"),
                (false, "ab"),
                (false, "abc"),
                (true, "abcd"),
            });
    }

    [Test]
    public void ExcludeExtractor_ExtractsGivenWords()
    {
        new[] {"a", "ab", "b", "bc"}
            .Select(e => (excludeExtractor.IsSuitable(e), e))
            .Should()
            .BeEquivalentTo(new[]
            {
                (false, "a"),
                (true, "ab"),
                (false, "b"),
                (true, "bc"),
            });
    }
}