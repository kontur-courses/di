using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

[TestFixture]
public class WordParser_Should
{
    private string testFilesDirectory = $"{TestContext.CurrentContext.WorkDirectory}/TextSamples/";

    [Test]
    public void GetAllWords_WhenGivenTxtFileWithOneWordInLine()
    {
        var wordParser = new WordParser();
        var parsedWords = wordParser.GetAllWords($"{testFilesDirectory}TestText.txt").ToArray();

        parsedWords.Should().BeEquivalentTo("this", "is", "a", "test", "text", "file");
    }

    [Test]
    public void RemoveDullWords_Removes()
    {
        var wordParser = new WordParser();
        var parsedWords = wordParser.GetAllWords($"{testFilesDirectory}TestText.txt").ToArray();
        var dullWords = new[] { "test", "text" };
        var noDullWords = wordParser.RemoveDullWords(parsedWords, s => !dullWords.Contains(s));

        noDullWords.Should().BeEquivalentTo("this", "is", "a", "file");
    }
}