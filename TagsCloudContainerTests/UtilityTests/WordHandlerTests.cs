using FluentAssertions;
using TagsCloudContainer.utility;

namespace TagsCloudContainerTests.UtilityTests;

[TestFixture]
public class WordHandlerTests
{
    [Test]
    public void PreprocessingExcludeRule_Should_ExcludeShortWords()
    {
        var before = new List<(string word, int count)>
            {
                ("run", 3),
                ("table", 1),
                ("chair", 10),
                ("of", 2),
                ("a", 220),
                ("second", 2)
            };

        var actual = new WordHandler().Preprocessing(before, excludeRule: w => w.Length > 2);
        
        var expected = new List<(string word, int count)>
        {
            ("run", 3),
            ("table", 1),
            ("chair", 10),
            ("second", 2)
        };

        actual.Should().BeEquivalentTo(expected);
    }
    
    [Test]
    public void PreprocessingExcludeWords_Should_ExcludeFromFile()
    {
        var before = new List<(string word, int count)>
        {
            ("Three", 3),
            ("The", 3),
            ("two", 2),
            ("of", 2),
            ("a", 220),
            ("second", 2)
        };

        var actual = new WordHandler().Preprocessing(before, 
            new FileTextHandler().ReadText(Utility.GetAbsoluteFilePath("src/boringWords.txt")));
        
        var expected = new List<(string word, int count)>
        {
            ("a", 220),
            ("second", 2)
        };

        actual.Should().BeEquivalentTo(expected);
    }
}