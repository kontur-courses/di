using TagCloudApp.Abstractions;

namespace TagCloudApp.Domain;

public class TagCloudPainter
{
    private readonly IImageHolder _imageHolder;
    private readonly ITagCloudLayouterCreator _layouterCreator;
    private readonly TagCloudPaintSettings _paintSettings;
    private readonly IWordsInfoProvider _wordsInfoProvider;

    public TagCloudPainter(
        IImageHolder imageHolder,
        ITagCloudLayouterCreator layouterCreator,
        TagCloudPaintSettings paintSettings,
        IWordsInfoProvider wordsInfoProvider
    )
    {
        _imageHolder = imageHolder;
        _layouterCreator = layouterCreator;
        _paintSettings = paintSettings;
        _wordsInfoProvider = wordsInfoProvider;
    }

    public void Paint()
    {
        var layouter = _layouterCreator.CreateLayouter();
        var basicFont = _paintSettings.BasicFont;
        var imageSize = _imageHolder.GetImageSize();

        var countSortedWords = _wordsInfoProvider.WordInfos
            .OrderByDescending(word => word.Count)
            .ToArray();
        var minCount = countSortedWords[^1].Count;
        var maxCount = countSortedWords[0].Count;

        using (var graphics = _imageHolder.StartDrawing())
        {
            using (var backgroundBrush = new SolidBrush(_paintSettings.BackgroundColor))
            {
                graphics.FillRectangle(backgroundBrush, 0, 0, imageSize.Width, imageSize.Height);
            }

            using (var wordsBrush = new SolidBrush(_paintSettings.WordsColor))
            {
                for (var i = 0; i < countSortedWords.Length; i++)
                {
                    var (word, count) = countSortedWords[i];
                    using (var font = new Font(
                               basicFont.FontFamily,
                               CalculateFontSize(count, minCount, maxCount),
                               basicFont.Style)
                          )
                    {
                        var rectSize = Size.Ceiling(graphics.MeasureString(word, font));
                        var rect = layouter.PutNextRectangle(rectSize);
                        graphics.DrawString(word, font, wordsBrush, rect);
                    }

                    if (i % 10 == 0)
                        _imageHolder.UpdateUi();
                }
            }
        }

        _imageHolder.UpdateUi();
    }

    private int CalculateFontSize(int count, int minCount, int maxCount)
    {
        var sizeDelta = minCount == maxCount
            ? (_paintSettings.MaxFontSize - _paintSettings.MinFontSize) / 2d
            : (double) (count - minCount) / (maxCount - minCount) *
              (_paintSettings.MaxFontSize - _paintSettings.MinFontSize);
        return _paintSettings.MinFontSize + (int) sizeDelta;
    }
}