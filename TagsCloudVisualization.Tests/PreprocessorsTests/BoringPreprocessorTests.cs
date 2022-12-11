using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Preprocessors;

namespace TagsCloudVisualization.Tests.PreprocessorsTests;

[TestFixture]
public class BoringPreprocessorTests
{
    private BoringPreprocessor preprocessor;

    [SetUp]
    public void SetUp()
    {
        preprocessor = new BoringPreprocessor(new[]
        {
            "abc",
            "a",
            "boring"
        });
    }

    [Test]
    public void Process_ShouldRemoveBoringWords()
    {
        var words = new[] { "abc","notBoring","a","boring" };
        var expected = new[] { "notBoring" };
        var processed = preprocessor.Process(words);

        processed.Should().BeEquivalentTo(expected);
    }
}