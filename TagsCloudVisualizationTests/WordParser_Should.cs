using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

public class WordParser_Should
{
    private string testFilesDirectory = $"{TestContext.CurrentContext.WorkDirectory}/TextSamples/";

    [Test]
    public void GetInterestingWords_WhenGivenTxtFileWithOneWordInLine()
    {
        var wordParser = new InterestingWordsParser(new NoWordsDullChecker());
        var parsedWords = wordParser.GetInterestingWords($"{testFilesDirectory}TestText.txt").ToArray();

        parsedWords.Should().BeEquivalentTo("this", "is", "a", "test", "text", "file");
    }
}