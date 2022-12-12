using FluentAssertions;
using TagsCloud2.TagsCloudMaker.SizeDefiner;

namespace TestsTagsCloud;

public class SizeDefinerTests
{
    [Test]
    public void DefineStringSizeAndOrientation()
    {
        var sizeDefiner = new RectangleSizeDefiner();
        var wordsFont = new Dictionary<string, int>
        {
            { "мама", 50 }
        };


        var wordsAndTextOptions =
            sizeDefiner.DefineStringSizeAndOrientation(wordsFont,
                false, "Arial");

        wordsAndTextOptions["мама"].Size.Height.Should().Be(82);
        wordsAndTextOptions["мама"].Size.Width.Should().Be(192);
        wordsAndTextOptions["мама"].WordOrientation.Should().Be(WordOrientation.Horizontal);
        wordsAndTextOptions["мама"].FontSize.Should().Be(50);
    }
}