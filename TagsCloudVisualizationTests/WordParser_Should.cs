using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

public class WordParser_Should
{
    private string testFilesDirectory = $"{TestContext.CurrentContext.WorkDirectory}/TextSamples/";

    private class TestDullChecker : IDullWordChecker
    {
        public bool Check(string word)
        {
            var testDullWords = new HashSet<string>() { "this", "is" };
            return testDullWords.Contains(word);
        }
    }
    
    [Test]
    public void GetInterestingWords_WhenGivenTxtFileWithOneWordInLine()
    {
        var wordParser = new WordParser();
        var parsedWords = wordParser.GetInterestingWords($"{testFilesDirectory}TestText.txt", 
                new TestDullChecker()).ToArray();

        parsedWords.Should().BeEquivalentTo("a", "test", "text", "file");
    }
}