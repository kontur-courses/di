using System.Drawing;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer;

public static class TarDrawingRetriever
{
    public static IEnumerable<TagDrawingItem> GetTagDrawingItems(
        this IEnumerable<CloudWord> cloudWords,
        ILayouterAlgorithm algorithm,
        Func<CloudWord, Font> fontResolver,
        Func<TagDrawingItem, Size> sizeResolver,
        Func<TagDrawingItem, bool> filter)
    {
        return cloudWords.Select(x => new TagDrawingItem(x.Text, fontResolver(x), Rectangle.Empty))
            .Select(x => x with { Rectangle = new(Point.Empty, sizeResolver(x)) })
            .Select(x => x with { Rectangle = algorithm.PutNextRectangle(x.Rectangle.Size) })
            .Where(filter);
    }
}