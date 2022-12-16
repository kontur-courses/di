using FluentAssertions;


namespace TagCloudContainer.Tests;

public class WordConfig_Should
{
    private WordConfig _wordConfig;

    [SetUp]
    public void SetUp()
    {
        _wordConfig = new WordConfig();
    }
    
    [Test]
    public void Validate_InvalidLines_ShouldThrowArgumentException()
    {
        var action = () => _wordConfig.Validate(null);
        action
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Argument is null");
    }
    
    [Test]
    public void ShuffleWords_Null_ShouldThrowArgumentException()
    {
        var action = () => WordsShuffler.ShuffleWords(null);
        action
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Argument is null");
    }
}