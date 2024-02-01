using FluentAssertions;
using Moq;
using TagsCloud.ConsoleCommands;
using TagsCloud.TextReaders;
using TagsCloud.WordValidators;

namespace TagsCloudTests;

[TestFixture]
public class WordsProviderTests
{
    private Mock<IWordValidator> validator;

    [SetUp]
    public void Setup()
    {
        validator = new Mock<IWordValidator>();
        validator.Setup(v => v.IsWordValid(It.IsAny<string>())).Returns(true);
    }


    [Test]
    public void ReadTextFromIncorrectFilePath()
    {
        var options = new Options();
        options.InputFile =
            Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, "input.txt");
        var textReader = new WordsProvider(validator.Object, options);
        Assert.Throws<FileNotFoundException>(() => textReader.GetWords());
    }

    [Test]
    public void ReadTextFromFile()
    {
        var options = new Options();
        options.InputFile =
            Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName.Replace("\\bin",""),"TextReaders", "input.txt");
        var words = new WordsProvider(validator.Object, options).GetWords();
        var dict = new Dictionary<string, int>()
        {
            { "ренат", 3 }, 
            { "привет", 4 },
            { "дом", 1 },
            { "стол", 2 }
        };
        words.Should().BeEquivalentTo(dict);
    }
}