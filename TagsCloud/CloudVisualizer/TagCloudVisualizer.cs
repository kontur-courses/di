using System.Drawing;
using System.Windows.Forms;
using TagsCloud.App.Settings;
using TagsCloud.CloudLayouter;
using TagsCloud.CloudPainter;
using TagsCloud.WordAnalyzer;

namespace TagsCloud.CloudVisualizer;

public class TagCloudVisualizer
{
    private readonly Graphics graphics;
    private readonly Size sizeImage;
    private readonly TagSettings tagSettings;

    public TagCloudVisualizer(TagSettings tagSettings, Graphics graphics, Size imageSize)
    {
        this.graphics = graphics;
        sizeImage = imageSize;
        this.tagSettings = tagSettings;
    }

    public void DrawTagCloud(ISpiral spiral, IEnumerable<WordInfo> wordInfo)
    {
        spiral.Init(new Point(sizeImage.Width / 2, sizeImage.Height / 2));
        var cloudLayouter = new CloudLayouter.CloudLayouter(spiral);
        var background = new SolidBrush(Color.Black);
        graphics.FillRectangle(background, new Rectangle(0, 0, sizeImage.Width, sizeImage.Height));
        DrawTagsCloud(wordInfo, graphics, cloudLayouter);
    }

    private Tag GetTag(WordInfo wordInfo, ICloudLayouter cloudLayouter)
    {
        var font = new Font(tagSettings.FontFamily, wordInfo.Count * tagSettings.Size);
        var textSize = TextRenderer.MeasureText(wordInfo.Word, font);
        var textRectangle = cloudLayouter.PutNextRectangle(new Size(textSize.Width, textSize.Height));
        return new Tag(font, wordInfo.Word, textRectangle, tagSettings.Color);
    }

    private void DrawTagsCloud(IEnumerable<WordInfo> words, Graphics graphics, ICloudLayouter cloudLayouter)
    {
        foreach (var word in words)
        {
            var tag = GetTag(word, cloudLayouter);
            var brush = new SolidBrush(tag.Color);
            graphics.DrawString(tag.Word, tag.Font, brush, tag.Rectangle.Location);
        }
    }
}