using NSubstitute;
using TagsCloudCore.BuildingOptions;
using TagsCloudCore.Common.Enums;
using TagsCloudCore.WordProcessing.WordFiltering;
using TagsCloudCore.WordProcessing.WordGrouping;
using TagsCloudCore.WordProcessing.WordInput;

namespace TagsCloudCoreTests.WordProcessing.WordGrouping;

public class DefaultWordProcessorTests
{
    [Test]
    public void ProcessedWords_ReturnsCorrectlyProcessedWords_OnCorrectInputData()
    {
        var options = Substitute.For<ICommonOptionsProvider>();
        options.CommonOptions.Returns(new CommonOptions(new WordProviderInfo(WordProviderType.Txt, ""), WordColorerAlgorithm.Default,
            CloudBuildingAlgorithm.Circular));
        var source = Substitute.For<IWordProvider>();
        source.GetWords("").Returns(new[] {"1", "2", "4", "4", "1", "1", "5"});
        source.Match(WordProviderType.Txt).Returns(true);
        var filter1 = GetWordFilter(new[] {"2"});
        var filter2 = GetWordFilter(new[] {"5"});
        var processor = new DefaultWordProcessor(options, new[] {filter1, filter2},
            new [] {source});

        var result = processor.ProcessedWords;

        CollectionAssert.AreEqual(new Dictionary<string, int> {{"1", 3}, {"4", 2}}, result);
    }

    private static DefaultWordFilter GetWordFilter(string[] wordsToExclude)
    {
        var wordProvider = Substitute.For<IWordProvider>();
        wordProvider.GetWords("").Returns(wordsToExclude);
        return new DefaultWordFilter(wordProvider, "");
    }
}