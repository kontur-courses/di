using TagCloud.Domain.Settings;
using TagCloud.Domain.WordEntities;
using TagCloud.Domain.WordProcessing.Extractors;
using TagCloud.Domain.WordProcessing.Interfaces;
using TagCloud.Domain.WordProcessing.WordProcessors;

namespace TagCloud.Tests;

public class WordProcessorTests
{
    [SetUp]
    public void SetUp()
    {
        settings = new TagCloudSettings(
            new LayoutSettings(),
            new FileSettings(),
            new VisualizerSettings(),
            new WordSettings());
        
        settings.WordSettings.Excluded.AddRange(new [] {"abcde", "bcdef"});
    }
    
    private TagCloudSettings settings;

    [Test]
    public void GetClearWordsWithCount_CountsCorrectly()
    {
        new WordProcessor(Enumerable.Empty<IWordExtractor>(), settings)
            .GetClearWordsWithCount(new[] {"a", "b", "b", "c"})
            .Words
            .Should()
            .BeEquivalentTo(new[]
            {
                new WordWithCount("a", 1),
                new WordWithCount("b", 2),
                new WordWithCount("c", 1),
            });
    }
    
    [Test]
    public void GetClearWordsWithCount_SortsIfLayoutBigToCenter()
    {
        settings.LayoutSettings.BigToCenter = true;
        new WordProcessor(Enumerable.Empty<IWordExtractor>(), settings)
            .GetClearWordsWithCount(new[] {"a", "b", "b", "c"})
            .Words
            .Should()
            .BeInDescendingOrder(e => e.Count);
    }

    [Test]
    public void GetClearWordsWithCount_AppliesExtractors()
    {
        var boringExtractor = new BoringLengthExtractor();
        var excludeExtractor = new ExcludeExtractor(settings.WordSettings);
        
        new WordProcessor(new IWordExtractor[]{boringExtractor, excludeExtractor}, settings)
            .GetClearWordsWithCount(new[] {"a", "b", "abcde", "bcdef", "abcdef", "abcdef", "bcdefg"})
            .Words
            .Should()
            .BeEquivalentTo(new []
            {
                new WordWithCount("abcdef", 2),
                new WordWithCount("bcdefg", 1),
            });
    }
}