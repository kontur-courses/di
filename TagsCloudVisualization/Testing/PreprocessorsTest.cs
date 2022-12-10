﻿using FluentAssertions;
using TagsCloudVisualization;

namespace Testing;

[TestFixture]
public class PreprocessorsTest
{
    private IPreprocessor preprocessor;

    [SetUp]
    public void SetUp()
    {
        preprocessor = new DefaultPreprocessor();
    }

    [TestCase("a")]
    [TestCase("a b c")]
    public void SeparatedBySpaceTest(string text)
    {
        var result = preprocessor.Preprocessing(text);
        var exepectedResultKeys = text.Split(" ");
        var expectedResultValues = exepectedResultKeys.Select(key => text.SubstringCount(key));
        result.Keys
            .Should()
            .BeEquivalentTo(exepectedResultKeys);
        result.Values
            .Should()
            .BeEquivalentTo(expectedResultValues);
    }
}