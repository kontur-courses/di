using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Preprocessors;

namespace TagsCloudVisualization.Tests.PreprocessorsTests;

[TestFixture]
public class LowerCasePreprocessorTests
{
    private LowerCasePreprocessor preprocessor;

    [SetUp]
    public void SetUp()
    {
        preprocessor = new LowerCasePreprocessor();
    }

    [Test]
    public void Process_ShouldReturnLowerCaseWords()
    {
        var words = new[] { "Abc", "ABC", "aBC", "aBc" };
        var expected = Enumerable.Repeat("abc", 4);

        var processed = preprocessor.Process(words);

        processed.Should().BeEquivalentTo(expected);
    }
}