using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudContainer;
using TagsCloudContainer.WordsInterfaces;

namespace CloudGeneratorTests;

public class WordsReader_Should
{
    [Test]
    public void Read_WhenFileWasFound_ReturnsAllWords()
    {
        var path = TestsUtility.GetFullPathFromRelative("../../../TestCases/Words/WordsReader/inputReader.txt");
        var actual = AppDIInitializer.Container.GetService<IWordsReader>().Read(path);
        var expected = new List<string> { "I", "am", "reader" };
        actual.Should().BeEquivalentTo(expected);
    }
}