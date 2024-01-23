using System.Drawing;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.Tags;
using TagsCloudVisualization.TextHandlers;

namespace TagsCloudVisualization.CloudLayouters;

public class TagsLayouter : ITagLayouter
{
    private readonly ICloudLayouter cloudLayouter;
    private readonly ITextHandler textHandler;
    private readonly Graphics graphics;
    private readonly FontSettings fontSettings;

    public TagsLayouter(ICloudLayouter cloudLayouter, ITextHandler textHandler, FontSettings fontSettings) 
    {
        this.cloudLayouter = cloudLayouter;
        this.textHandler = textHandler;
        this.fontSettings = fontSettings;
        graphics = Graphics.FromHwnd(IntPtr.Zero);
    }

    public IEnumerable<Tag> GetTags()
    {
        var handledText = textHandler.HandleText();
        var sortedWords = handledText.OrderByDescending(p => p.Value);
        var maxWordCount = sortedWords.First().Value;
        var minWordCount = sortedWords.Last().Value;

        foreach (var wordWithCount in sortedWords)
        {
            var fontSize = GetFontSize(minWordCount, maxWordCount, wordWithCount.Value);
            yield return new Tag(wordWithCount.Key, 
                fontSize,
                cloudLayouter.PutNextRectangle(GetWordSize(wordWithCount.Key, fontSize)));
        }
    }

    private int GetFontSize(int minWordCount, int maxWordCount, int wordCount)
    {
        return maxWordCount > minWordCount ? fontSettings.MinSize 
            + (fontSettings.MaxSize - fontSettings.MinSize) 
            * (wordCount - minWordCount) 
            / (maxWordCount - minWordCount) : fontSettings.MaxSize;
    }

    private Size GetWordSize(string content, int fontSize)
    {
        var sizeF = graphics.MeasureString(content, new Font(fontSettings.FontFamily, fontSize));
        return new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));
    }
}