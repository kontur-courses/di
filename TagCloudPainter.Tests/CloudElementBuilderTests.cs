using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloudPainter.Builders;
using TagCloudPainter.Common;
using TagCloudPainter.Layouters;
using TagCloudPainter.Sizers;

namespace TagCloudPainter.Tests;

public class CloudElementBuilderTests
{
    public CloudElementBuilder Builder { get; set; }
    public WordSizer Sizer { get; set; }
    public CircularCloudLayouter Layouter { get; set; }

    [SetUp]
    public void Setup()
    {
        var settings = new ImageSettings
        {
            BackgroundColor = Color.Bisque,
            Font = new Font("Arial", 10),
            Size = new Size(500, 500)
        };
        var provider = new ImageSettingsProvider { ImageSettings = settings };
        Sizer = new WordSizer(provider);
        var circularCloudLayouter = new CircularCloudLayouter(new Point(50, 50), Math.PI / 12, 0.25);
        Layouter = new CircularCloudLayouter(new Point(50, 50), Math.PI / 12, 0.25);
        Builder = new CloudElementBuilder(Sizer, circularCloudLayouter);
    }

    [Test]
    public void GetTag_ShouldFail_On_Null_Dictionary()
    {
        Action action = () => Builder.GetTags(null);

        action.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void GetTag_ShouldFail_On_Empty_Dictionary()
    {
        Action action = () => Builder.GetTags(new Dictionary<string, int>());

        action.Should().Throw<ArgumentNullException>();
    }

    [TestCase(null, 1, TestName = "{m}_null_word")]
    [TestCase("", 1, TestName = "{m}_EmptyWord")]
    [TestCase(" ", 1, TestName = "{m}_WhiteSpaceWord")]
    [TestCase("word", 0, TestName = "{m}_ZeroCount")]
    [TestCase("word", -1, TestName = "{m}_NegativeCount")]
    public void GetTag_ShouldFail_On_DictionaryWith(string word, int count)
    {
        Action action = () => Builder.GetTags(new Dictionary<string, int>());

        action.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void GetTag_Basic_Functionality_Test()
    {
        var words = new Dictionary<string, int>
        {
            ["word1"] = 1,
            ["word1"] = 2,
            ["word1"] = 3
        };
        var tags = new List<Tag>();
        foreach (var (word, count) in words)
        {
            var size = Sizer.GetTagSize(word, count);
            var rectangle = Layouter.PutNextRectangle(size);
            tags.Add(new Tag(word, rectangle, count));
        }

        var result = Builder.GetTags(words).ToList();

        result.Should().BeEquivalentTo(tags);
    }
}