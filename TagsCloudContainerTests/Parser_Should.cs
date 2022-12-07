using FluentAssertions;
using TagsCloudContainer;

namespace CloudGeneratorTests;


public class Parser_Should
{
    [Test]
    public void WhenFileDoesntExist_ThrowsException()
    {
        var path = "ThisFileDoesntExist";
        Action act = () =>  new Parser(path);
        act
            .Should()
            .Throw<FileNotFoundException>()
            .WithMessage($"There is no file with path: {path}!");
    }

    [Test]
    public void GetInputWordsFrequency_ReturnsAllDataInLowerCase()
    {
        var parser = new Parser(GetFullPathFromRelative("../../../TestCases/LowerCaseCheck.txt"));
        
        var expectedFreq = new Dictionary<string, double>();
        expectedFreq["привет"] = 0.5;
        expectedFreq["круг"] = 0.5;

        parser.GetInputWordsFrequency().Should().BeEquivalentTo(expectedFreq);
    }
    
    [Test]
    public void GetInputWordsFrequency_WhenDefault_ReturnsCorrectData()
    {
        var parser = new Parser(GetFullPathFromRelative("../../../TestCases/Default.txt"));
        
        var expectedFreq = new Dictionary<string, double>();
        expectedFreq["привет"] = 0.43;
        expectedFreq["земля"] = 0.29;
        expectedFreq["предлагать"] = 0.14;
        expectedFreq["предложить"] = 0.14;

        parser.GetInputWordsFrequency().Should().BeEquivalentTo(expectedFreq);
    }

    [Test]
    public void GetInputWordsFrequency_WhenHasBoringWords_ReturnsDataWithoutThem()
    {
        var parser = new Parser(GetFullPathFromRelative("../../../TestCases/WithBoringWords.txt"));
        
        var expectedFreq = new Dictionary<string, double>();
        expectedFreq["круг"] = 0.5;
        expectedFreq["земля"] = 0.5;

        parser.GetInputWordsFrequency().Should().BeEquivalentTo(expectedFreq);
    }

    private string GetFullPathFromRelative(string relativePath)
    {
        var sCurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var sFile = Path.Combine(sCurrentDirectory, relativePath);
        return Path.GetFullPath(sFile);
    }
}