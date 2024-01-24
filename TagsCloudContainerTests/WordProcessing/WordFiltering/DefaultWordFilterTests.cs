using NSubstitute;
using TagsCloudContainer.WordProcessing.WordFiltering;
using TagsCloudContainer.WordProcessing.WordInput;

namespace TagsCloudContainerTests.WordProcessing.WordFiltering;

public class DefaultWordFilterTests
{
    [Test]
    public void FilterWords_ReturnsCorrectResult_OnCorrectInputData()
    {
        var wordProvider = Substitute.For<IWordProvider>();
        wordProvider.Words.Returns(new[] {"word1", "word2", "123"});

        var filter = new DefaultWordFilter(wordProvider);
        var filtered = filter.FilterWords(new[] {"word1", "1234", "word2", "a", "another", "test"});
        
        CollectionAssert.AreEqual(new []{"1234", "a", "another", "test"}, filtered);
    }
}