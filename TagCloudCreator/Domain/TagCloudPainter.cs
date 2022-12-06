using System.Drawing;
using TagCloudCreator.Domain.Settings;
using TagCloudCreator.Interfaces;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreator.Domain;

public class TagCloudPainter
{
    private readonly IImageHolder _imageHolder;
    private readonly ITagCloudLayouterProvider _layouterProvider;
    private readonly IWordsInfosProvider _wordsInfosProvider;
    private readonly TagCloudPaintSettings _paintSettings;

    public TagCloudPainter(
        IImageHolder imageHolder,
        ITagCloudLayouterProvider layouterProvider,
        IWordsInfosProvider wordsInfosProvider,
        TagCloudPaintSettings paintSettings
    )
    {
        _imageHolder = imageHolder;
        _layouterProvider = layouterProvider;
        _wordsInfosProvider = wordsInfosProvider;
        _paintSettings = paintSettings;
    }

    public void Paint()
    {
        var layouter = _layouterProvider.CreateLayouter();
        var imageSize = _imageHolder.GetImageSize();

        var countSortedWordsInfos = _wordsInfosProvider.WordsInfos
            .OrderByDescending(word => word.Count)
            .ToArray();
        var minCount = countSortedWordsInfos[^1].Count;
        var maxCount = countSortedWordsInfos[0].Count;

        using (var graphics = _imageHolder.StartDrawing())
        {
            using (var backgroundBrush = new SolidBrush(_paintSettings.BackgroundColor))
            {
                graphics.FillRectangle(backgroundBrush, 0, 0, imageSize.Width, imageSize.Height);
            }

            using (var wordsBrush = new SolidBrush(_paintSettings.WordsColor))
            {
                for (var i = 0; i < countSortedWordsInfos.Length; i++)
                {
                    var (word, count) = countSortedWordsInfos[i];
                    using (var font = CreateFont(count, minCount, maxCount))
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

    private Font CreateFont(int currentCount, int minCount, int maxCount) =>
        new(
            _paintSettings.BasicFont.FontFamily,
            CalculateFontSize(currentCount, minCount, maxCount),
            _paintSettings.BasicFont.Style
        );

    private int CalculateFontSize(int count, int minCount, int maxCount)
    {
        var sizeDelta = minCount == maxCount
            ? (_paintSettings.MaxFontSize - _paintSettings.MinFontSize) / 2d
            : (double) (count - minCount) / (maxCount - minCount) *
              (_paintSettings.MaxFontSize - _paintSettings.MinFontSize);

        return _paintSettings.MinFontSize + (int) sizeDelta;
    }
}