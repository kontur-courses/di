using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.Words;

namespace TagsCloudVisualization;

public class TagCloudVisualizations
{
    private readonly ICloudLayouter _cloudLayouter;
    private readonly IWordsLoader _wordsLoader;


    public TagCloudVisualizations(ICloudLayouter layouter, IWordsLoader wordsLoader)
    {
        _cloudLayouter = layouter;
        _wordsLoader = wordsLoader;
    }


    public Bitmap DrawCloud(VisualizationOptions options)
    {
        _cloudLayouter.Reset();
        var center = new Point(options.CanvasSize.Width / 2, options.CanvasSize.Height / 2);
        var layoutOptions = new LayoutOptions(center, options.SpiralStep);

        var size = options.CanvasSize;
        var bitmap = new Bitmap(size.Width, size.Height);
        var graphics = Graphics.FromImage(bitmap);

        graphics.Clear(options.BackgroundColor);
        var wordsToDraw = _wordsLoader.LoadWords(options);

        var wordIndex = 0;
        foreach (var word in wordsToDraw)
        {
            var fontForWord = new Font(options.FontFamily, word.Size);
            var stringSize = graphics.MeasureString(word.Text, fontForWord);
            var rectangle = _cloudLayouter.PutNextRectangle(stringSize, layoutOptions);
            if (rectangle.IsEmpty)
                continue;

            var fontColor = GetNextFontColor(options, wordIndex);
            graphics.DrawString(word.Text, fontForWord, fontColor, rectangle);
            wordIndex++;
        }

        return bitmap;
    }

    private static Brush GetNextFontColor(VisualizationOptions options, int wordIndex)
    {
        if (!options.Palette.AvailableBrushes.Any())
            return options.Palette.DefaultBrush;

        if (options.Palette.AvailableBrushes.Count == 1)
            return options.Palette.AvailableBrushes[0];

        var brushesCount = options.Palette.AvailableBrushes.Count;
        var brushIndex = wordIndex % brushesCount;
        return options.Palette.AvailableBrushes[brushIndex];
    }
}