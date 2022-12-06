using CircularCloudLayouter;
using TagCloudApp.Abstractions;
using TagCloudApp.Domain;

namespace TagCloudApp;

public class TagCloudPainter
{
    private readonly IImageHolder _imageHolder;
    private readonly ITagCloudLayouter _layouter;
    private readonly TagCloudDrawSettings _drawSettings;
    private readonly IWordsInfoProvider _wordsInfoProvider;

    public TagCloudPainter(
        IImageHolder imageHolder,
        ITagCloudLayouter layouter,
        TagCloudDrawSettings drawSettings,
        IWordsInfoProvider wordsInfoProvider
    )
    {
        _imageHolder = imageHolder;
        _layouter = layouter;
        _drawSettings = drawSettings;
        _wordsInfoProvider = wordsInfoProvider;
    }

    public void Paint()
    {
        var imageSize = _imageHolder.GetImageSize();

        var countSortedWords = _wordsInfoProvider.WordInfos
            .OrderByDescending(word => word.Count)
            .ToArray();
        var minCount = countSortedWords[^1].Count;
        var maxCount = countSortedWords[0].Count;

        using (var graphics = _imageHolder.StartDrawing())
        {
            using (var backgroundBrush = new SolidBrush(_drawSettings.BackgroundColor))
            {
                graphics.FillRectangle(backgroundBrush, 0, 0, imageSize.Width, imageSize.Height);
            }

            using (var wordsBrush = new SolidBrush(_drawSettings.WordsColor))
            {
                for (var i = 0; i < countSortedWords.Length; i++)
                {
                    var (word, count) = countSortedWords[i];
                    var fontSize = maxCount == minCount
                        ? _drawSettings.MinFontSize + (_drawSettings.MaxFontSize - _drawSettings.MinFontSize) / 2d
                        : _drawSettings.MinFontSize +
                          (double) (count - minCount) / (maxCount - minCount) *
                          (_drawSettings.MaxFontSize - _drawSettings.MinFontSize);
                    using (var font = new Font(_drawSettings.FontFamily, (int) fontSize))
                    {
                        var rectSize = Size.Ceiling(graphics.MeasureString(word, font));
                        var rect = _layouter.PutNextRectangle(rectSize);
                        graphics.DrawString(word, font, wordsBrush, rect);
                    }

                    if (i % 10 == 0)
                        _imageHolder.UpdateUi();
                }
            }
        }

        _imageHolder.UpdateUi();
    }
}