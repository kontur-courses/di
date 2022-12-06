using System.Drawing;
using TagCloudCreator.Domain.Settings;
using TagCloudCreator.Infrastructure;
using TagCloudCreator.Interfaces.Providers;

namespace TagCloudCreator.Domain;

public class WordsPaintDataProvider : IWordsPaintDataProvider
{
    private readonly Graphics _graphics;
    private readonly ITagCloudLayouterProvider _layouterProvider;
    private readonly IWordsInfosProvider _wordsInfosProvider;
    private readonly TagCloudPaintSettings _paintSettings;

    public WordsPaintDataProvider(
        Graphics graphics,
        ITagCloudLayouterProvider layouterProvider,
        IWordsInfosProvider wordsInfosProvider,
        TagCloudPaintSettings paintSettings
    )
    {
        _graphics = graphics;
        _layouterProvider = layouterProvider;
        _wordsInfosProvider = wordsInfosProvider;
        _paintSettings = paintSettings;
    }

    public IEnumerable<WordPaintData> GetWordsPaintData()
    {
        var layouter = _layouterProvider.CreateLayouter();
        var countSortedWordsInfos = _wordsInfosProvider.WordsInfos
            .OrderByDescending(word => word.Count)
            .ToArray();
        var minCount = countSortedWordsInfos[^1].Count;
        var maxCount = countSortedWordsInfos[0].Count;

        foreach (var (word, count) in countSortedWordsInfos)
        {
            using var font = CreateFont(count, minCount, maxCount);
            var rectSize = Size.Ceiling(_graphics.MeasureString(word, font));
            var rect = layouter.PutNextRectangle(rectSize);

            yield return new WordPaintData(word, font, rect);
        }
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