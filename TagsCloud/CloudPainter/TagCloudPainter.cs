using System.Drawing;
using System.Windows.Forms;
using TagsCloud.CloudLayouter;
using TagsCloud.Infrastructure;
using TagsCloud.Settings;
using TagsCloud.WordAnalyzer;

namespace TagsCloud.CloudPainter;

public class TagCloudPainter
{
    
    private readonly IImageHolder imageHolder;
    private readonly TagSettings tagSettings;
    private readonly WordAnalyzer.WordAnalyzer wordAnalyzer;
    private readonly FileReader fileReader;

    public TagCloudPainter(IImageHolder imageHolder, TagSettings tagSettings, WordAnalyzer.WordAnalyzer wordAnalyzer,
        FileReader reader)
    {
        this.imageHolder = imageHolder;
        this.tagSettings = tagSettings;
        this.wordAnalyzer = wordAnalyzer;
        fileReader = reader;
    }
    public void Paint(ISpiral spiral, string path)
    {
        using var graphics = imageHolder.StartDrawing();
        var sizeImage = imageHolder.GetImageSize();
        var cloudLayouter = new CloudLayouter.CloudLayouter(spiral);
        var wordList = fileReader.GetWords(path);
        var background = new SolidBrush(Color.Black);
        graphics.FillRectangle(background, new Rectangle(0, 0, sizeImage.Width, sizeImage.Height));
        DrawTagsCloud(wordAnalyzer.GetFrequencyList(wordList), graphics, cloudLayouter);
        imageHolder.UpdateUi();
    }
    
    private Tag GetTag(WordInfo wordInfo, ICloudLayouter cloudLayouter)
    {
        var font = new Font(tagSettings.FontFamily, wordInfo.Count * tagSettings.Size);
        var textSize = TextRenderer.MeasureText(wordInfo.Word, font);
        var textRectangle = cloudLayouter.PutNextRectangle(new Size(textSize.Width, textSize.Height));
        return new Tag(font, wordInfo.Word, textRectangle, tagSettings.Color);
    }

    private void DrawTagsCloud(IEnumerable<WordInfo>words,Graphics graphics, ICloudLayouter cloudLayouter)
    {
        foreach (var word in words)
        {
            var tag = GetTag(word, cloudLayouter);
            var brush = new SolidBrush(tag.Color);
            graphics.DrawString(tag.Word, tag.Font, brush, tag.Rectangle.Location);
        }
    }
}