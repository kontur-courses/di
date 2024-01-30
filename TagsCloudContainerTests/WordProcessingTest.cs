using TagsCloudContainer;
using TagsCloudContainer.FileReader;
using TagsCloudContainer.WordProcessing;

namespace TagsCloudContainerTests;

[TestFixture]
public class WordProcessingTest
{
    [TestCase("UpperWords.txt")]
    public void WordProcessing_ShouldCorrectToLowerAndCountWords(string filename)
    {
        var testWords = new List<Word>()
        {
            new("привет") { Count = 3 },
            new("как") { Count = 5 },
            new("дела") { Count = 2 },
            new("у") { Count = 1 },
            new("тебя") { Count = 1 }
        };
        filename = "../../../../TagsCloudContainerTests/TextFiles/" + filename;
        var settings = new Settings();
        var reader = new FileReaderFactory().GetReader(filename);
        var file = reader.GetTextFromFile(filename);
        var words = new WordProcessor(settings).ProcessWords(file);
        for (var i = 0; i < words.Count; i++)
            words[i].Count.Should().Be(testWords[i].Count);
    }
}