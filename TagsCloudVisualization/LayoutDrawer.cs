using System.Drawing.Imaging;
using System.Drawing;

namespace TagsCloudVisualization;

public class LayoutDrawer
{
    private IInterestingWordsParser interestingWordsParser;
    private IRectangleLayouter rectangleLayouter;
    private IPalette palette;
    private Font font;

    public LayoutDrawer(IInterestingWordsParser interestingWordsParser,
        IRectangleLayouter rectangleLayouter,
        IPalette palette,
        Font font)
    {
        this.interestingWordsParser = interestingWordsParser;
        this.rectangleLayouter = rectangleLayouter;
        this.palette = palette;
        this.font = font;
    }

    public Bitmap CreateLayoutImageFromFile(string inputFilePath,
        Size imageSize,
        int minimumFontSize)
    {
        var bitmap = new Bitmap(imageSize.Width, imageSize.Height);
        using var graphics = Graphics.FromImage(bitmap);

        inputFilePath = Path.GetFullPath(inputFilePath);

        var sortedWordsCount = interestingWordsParser.GetInterestingWords(inputFilePath)
            .GroupBy(s => s)
            .Select(group => new { Word = group.Key, Count = group.Count() })
            .OrderByDescending(wordCount => wordCount.Count);
        var mostWordOccurrencies = sortedWordsCount.Max(arg => arg.Count);

        graphics.Clear(palette.GetBackgroundColor());
        
        foreach (var wordCount in sortedWordsCount)
        {
            var rectangleFont = new Font(font.FontFamily, 
                Math.Max(font.Size * wordCount.Count / mostWordOccurrencies, minimumFontSize));
            var rectangleSize = graphics.MeasureString(wordCount.Word, rectangleFont);

            var textRectangle = rectangleLayouter.PutNextRectangle(rectangleSize);
            var x = textRectangle.X + imageSize.Width / 2;
            var y = textRectangle.Y + imageSize.Height / 2;

            using var brush = new SolidBrush(palette.GetNextWordColor());
            graphics.DrawString(wordCount.Word, rectangleFont, brush, x, y);
        }

        return bitmap;
    }
}