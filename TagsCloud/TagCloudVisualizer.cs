using System.Drawing;
using System.Windows.Forms;
using TagsCloud.Settings;
using TagsCloud.WordAnalyzer;

namespace TagsCloud;

public class TagCloudVisualizer
{
    private Graphics graphics;
    private readonly TagSettings tagSettings;
    private ICloudLayouter cloudLayouter;
    public TagCloudVisualizer(Graphics graphics, TagSettings tagSettings, ICloudLayouter cloudLayouter)
    {
        this.graphics = graphics;
        this.tagSettings = tagSettings;
        this.cloudLayouter = cloudLayouter;
    }
    public void RenderCloudImage(IEnumerable<WordInfo> words, Size sizeImage)
    {
        var background = new SolidBrush(Color.Black);
        graphics.FillRectangle(background, new Rectangle(0, 0, sizeImage.Width, sizeImage.Height));
        DrawTagsCloud(words);
    }
    private Tag GetTag(WordInfo wordInfo)
    {
            var font = new Font(tagSettings.FontFamily, wordInfo.Count * tagSettings.Size);
            var textSize = TextRenderer.MeasureText(wordInfo.Word, font);
            var textRectangle = cloudLayouter.PutNextRectangle(new Size(textSize.Width, textSize.Height));
            return new Tag(font, wordInfo.Word, textRectangle, tagSettings.Color);
    }

    private void DrawTagsCloud(IEnumerable<WordInfo>words)
    {
        foreach (var word in words)
        {
            var tag = GetTag(word);
            var brush = new SolidBrush(tag.Color);
            graphics.DrawString(tag.Word, tag.Font, brush, tag.Rectangle.Location);
        }
    }
}