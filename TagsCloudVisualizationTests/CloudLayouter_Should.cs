using System.Drawing;
using FluentAssertions;
using TagsCloudVisualization;

namespace TagsCloudVisualizationTests;

public class CloudLayouter_Should
{
    private CloudLayouter? circularCloudLayouter;

    [SetUp]
    public void SetCircularCloudFieldToNull()
    {
        circularCloudLayouter = null;
    }

    [Test]
    public void CreateLayout_WithDecreasingFontSize_WhenGivenLowercaseStringArray()
    {
        var words = new[] { "apple", "apple", "peach", "apple", "orange" };
        circularCloudLayouter = new CloudLayouter(new SpiralPointGenerator(), new Font("Arial", 25), words);
        
        var layout = circularCloudLayouter.CreateLayout();
        var layoutedWords = layout.Select(rectangle => rectangle.Text);
        var layoutedWordsFonts = layout.Select(rectangle => rectangle.Font.Size);
        
        layoutedWords.Should().BeEquivalentTo("apple", "peach", "orange");
        layoutedWordsFonts.Should().StartWith(25).And.BeInDescendingOrder();
    }
}