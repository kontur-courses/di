using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Preprocessors;

namespace TagsCloudVisualization.Tests.PreprocessorsTests;

[TestFixture]
public class MultiPreprocessorTests
{
    private MultiPreprocessor preprocessor;

    [SetUp]
    public void SetUp()
    {
        var boringWords = new[]
        {
            "abc",
            "a",
            "boring"
        };
        preprocessor = new MultiPreprocessor(new IPreprocessor[]
        {
            new LowerCasePreprocessor(),
            new BoringPreprocessor(boringWords)
        });
    }

    [Test]
    public void Process_ShouldReturnLowerCaseWords()
    {
        var words = new[] { "Abc", "NotBoring", "A", "BORING" };
        var expected = new[] { "notboring" };

        var processed = preprocessor.Process(words);

        processed.Should().BeEquivalentTo(expected);
    }
}