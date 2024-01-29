using NSubstitute;
using TagsCloudCore.BuildingOptions;
using TagsCloudCore.Drawing.Colorers;
using TagsCloudCore.WordProcessing.WordFiltering;
using TagsCloudCore.WordProcessing.WordGrouping;
using TagsCloudCore.WordProcessing.WordInput;
using TagsCloudVisualization;

namespace TagsCloudCoreTests.WordProcessing.WordGrouping;

public class DefaultWordProcessorTests
{
    [Test]
    public void ProcessedWords_ReturnsCorrectlyProcessedWords_OnCorrectInputData()
    {
        var options = Substitute.For<ICommonOptionsProvider>();
        var words = Substitute.For<IWordProvider>();
        options.CommonOptions.Returns(new CommonOptions(words, Substitute.For<IWordColorer>(),
            Substitute.For<ICloudLayouter>()));
        words.Words.Returns(new[] {"1", "2", "4", "4", "1", "1", "5"});
        var filter1 = GetWordFilter(new[] {"2"});
        var filter2 = GetWordFilter(new[] {"5"});
        var processor = new DefaultWordProcessor(options, new[] {filter1, filter2});

        var result = processor.ProcessedWords;
        
        CollectionAssert.AreEqual(new Dictionary<string, int> {{"1", 3}, {"4", 2}}, result);
    }
    
    private static DefaultWordFilter GetWordFilter(string[] wordsToExclude)
    {
        var wordProvider = Substitute.For<IWordProvider>();
        wordProvider.Words.Returns(wordsToExclude);
        return new DefaultWordFilter(wordProvider);
    }
}