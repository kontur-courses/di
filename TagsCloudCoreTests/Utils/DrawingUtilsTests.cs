using System.Drawing;
using FluentAssertions;
using TagsCloudCore.Utils;

namespace TagsCloudCoreTests.Utils;

public class DrawingUtilsTests
{
    [TestCase(0, 2)]
    [TestCase(2, 0)]
    [TestCase(2, -2)]
    [TestCase(-2, 2)]
    public void GetStringSize_ThrowsArgumentException_OnIncorrectParameters(int frequency, int frequencyScaling)
    {
        Assert.Throws<ArgumentException>(() =>
            DrawingUtils.GetStringSize("word", frequency, frequencyScaling, new Font(FontFamily.GenericMonospace, 20)));
    }

    [TestCase("word", 2, 3, "longer_word", 2, 3)]
    [TestCase("word", 2, 3, "word", 5, 3)]
    [TestCase("word", 2, 3, "word", 2, 5)]
    public void GetStringSize_CorrectlyDeterminesWordSize_BasedOnWordLengthAndFrequency(
        string word1,
        int frequency1,
        int frequencyScaling1,
        string word2,
        int frequency2,
        int frequencyScaling2)

    {
        var size1 = DrawingUtils.GetStringSize(word1, frequency1, frequencyScaling1,
            new Font(FontFamily.GenericMonospace, 20));

        var size2 = DrawingUtils.GetStringSize(word2, frequency2, frequencyScaling2,
            new Font(FontFamily.GenericMonospace, 20));

        var area1 = size1.Height * size1.Width;
        var area2 = size2.Height * size2.Width;

        area2
            .Should()
            .BeGreaterThan(area1);
    }

    [TestCase("")]
    [TestCase("255 255 256")]
    [TestCase("-1 255 255")]
    [TestCase("255 255 255 255")]
    [TestCase("255 255")]
    [TestCase("255 notint 255")]
    public void TryParseRgb_ReturnsFalse_OnIncorrectInputData(string rgbString)
    {
        var result = DrawingUtils.TryParseRgb(rgbString, out _);

        result
            .Should()
            .BeFalse();
    }

    [Test]
    public void TryParseRgb_ReturnsCorrectColor_OnCorrectInputData()
    {
        _ = DrawingUtils.TryParseRgb("123 43 5", out var color);

        CollectionAssert.AreEqual(new[] {123, 43, 5}, new[] {color.R, color.G, color.B});
    }
}