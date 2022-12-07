using System.Drawing;
using TagCloud.Abstractions;

namespace TagCloud;

public class DrawingCloudCreator : ICloudCreator
{
    private readonly ICloudDrawer drawer;
    private readonly ICloudLayouter layouter;

    public DrawingCloudCreator(ICloudDrawer drawer, ICloudLayouter layouter)
    {
        this.drawer = drawer;
        this.layouter = layouter;
    }

    public Bitmap CreateTagCloud(IEnumerable<string> words)
    {
        var wordsToCount = words
            .GroupBy(w => w)
            .Select(g => (word: g.Key, count: g.Count()))
            .OrderByDescending(t => t.count)
            .ToList();
        var maxCount = wordsToCount.First().count;
        var minCount = wordsToCount.Last().count;
        var wordsToFontSize = wordsToCount
            .Select(t => (t.word, fontsize: GetFontSize(t.count, minCount, maxCount)));
        var wordsToFontsizeToRectangle = wordsToFontSize
            .Select(t => (t.word, t.fontsize, GetRectangle(t.word, t.fontsize)));
        return drawer.Draw(wordsToFontsizeToRectangle);
    }

    private Rectangle GetRectangle(string word, int fontsize)
    {
        using var font = new Font(drawer.FontFamily, fontsize);
        return layouter.PutNextRectangle(drawer.Graphics.MeasureString(word, font).ToSize());
    }

    private int GetFontSize(int count, int minCount, int maxCount)
    {
        var maxSize = drawer.MaxFontSize;
        var minSize = drawer.MinFontSize;
        return - (int)((double)(minSize * maxCount - minSize * count + maxSize * count - maxSize * minCount) /
                     (minCount - maxCount));
    }
}