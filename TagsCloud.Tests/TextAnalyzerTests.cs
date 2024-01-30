using FluentAssertions;
using NUnit.Framework;
using TagsCloud.TextAnalysisTools;
using TagsCloudVisualization;

namespace TagsCloud.Tests;

[TestFixture]
public class TextAnalyzerTests
{
    [Test]
    public void TextAnalyzer_Should_DistinguishRussianAndOtherWords()
    {
        var testData = GetTestGroups().ToArray();
        var groups = testData
                     .Select(data => data.Group)
                     .ToHashSet();

        TextAnalyzer.FillWithAnalysis(groups);

        foreach (var (group, isEnglish) in testData)
            group.WordInfo.IsRussian.Should().Be(isEnglish);
    }

    private static IEnumerable<(WordTagGroup Group, bool isRussian)> GetTestGroups()
    {
        yield return (new WordTagGroup("Apple", 1), false);
        yield return (new WordTagGroup("Игра", 1), true);
        yield return (new WordTagGroup("BMW", 1), false);
        yield return (new WordTagGroup("Богатырь", 1), true);
        yield return (new WordTagGroup("Математика", 1), true);
        yield return (new WordTagGroup("C#", 1), false);
        yield return (new WordTagGroup("Fibonacci", 1), false);
    }
}