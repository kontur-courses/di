using System.Drawing;
using FluentAssertions;
using TagsCloud.ConsoleCommands;
using TagsCloud.WordFontCalculators;

namespace TagsCloudTests.WordFontCalculators;

[TestFixture]
public class SimpleWordFontCalculatorTests
{
    [TestCase("Hello", 4)]
    [TestCase("World", 1)]
    [TestCase("Work", 12)]
    [TestCase("Home", 5)]
    [TestCase("Big", 134)]
    public void SimpleWordFontCalculator_ShouldReturnFontSizeAsWordCount(string word, int count)
    {
        var options = new Options() { TagsFont = "Arial" };
        var fontCalculator = new SimpleWordFontCalculator(options);
        fontCalculator.GetWordFont(word, count).Should().BeEquivalentTo(new Font("Arial", count));
    }
}