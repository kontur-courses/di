using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloudPainter.Common;
using TagCloudPainter.Sizers;

namespace TagCloudPainter.Tests;

public class WordSizerTests
{
    public WordSizer Sizer { get; set; }
    public ImageSettings Settings { get; set; }

    [SetUp]
    public void Setup()
    {
        Settings = new ImageSettings
        {
            BackgroundColor = Color.Bisque,
            Font = new Font("Arial", 10),
            Size = new Size(500, 500)
        };
        var provider = new ImageSettingsProvider { ImageSettings = Settings };
        Sizer = new WordSizer(provider);
    }

    [TestCase("", 1, TestName = "{m}_Empty")]
    [TestCase(" ", 1, TestName = "{m}_WhiteSpace")]
    [TestCase(null, 1, TestName = "{m}_null")]
    [TestCase("word", 0, TestName = "{m}_ZeroCount")]
    [TestCase("word", -1, TestName = "{m}_NegativeCount")]
    public void GetTagSize_Should_Fail_On_(string word, int count)
    {
        Action action = () => Sizer.GetTagSize(word, count);

        action.Should().Throw<ArgumentException>();
    }

    [TestCase("word", 1)]
    [TestCase("1", 3)]
    [TestCase("word", 4)]
    public void GetSize_Basic_Functionality_Test(string word, int count)
    {
        var width = word.Length * (int)(Settings.Font.Size + 1) + 3 * (count - 1);
        var height = (Settings.Size.Height + Settings.Size.Width) / 40 + 2 * (count - 1);

        var size = Sizer.GetTagSize(word, count);

        size.Should().BeEquivalentTo(new Size(width, height));
    }
}