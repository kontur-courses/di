using NSubstitute;
using TagsCloudContainer.WordProcessing.WordFiltering;
using TagsCloudContainer.WordProcessing.WordGrouping;
using TagsCloudContainer.WordProcessing.WordInput;

namespace TagsCloudContainerTests.WordProcessing.WordGrouping;

public class DefaultWordProcessorTests
{
    [Test]
    public void ProcessedWords_ReturnsCorrectlyProcessedWords_OnCorrectInputData()
    {
        var words = Substitute.For<IWordProvider>();
        words.Words.Returns(new[] {"1", "2", "4", "4", "1", "1", "5"});
        var filter1 = GetWordFilter(new[] {"2"});
        var filter2 = GetWordFilter(new[] {"5"});
        var processor = new DefaultWordProcessor(words, new[] {filter1, filter2});

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