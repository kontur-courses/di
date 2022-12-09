using CircularCloudLayouter.Domain;

namespace TagCloudCreator.Infrastructure;

public readonly record struct WordPaintData(string Word, int FontSize, ImmutableRectangle Rect)
{
    public bool Equals(WordPaintData other) =>
        Word == other.Word &&
        FontSize.Equals(other.FontSize) &&
        Rect.Equals(other.Rect);

    public override int GetHashCode() =>
        HashCode.Combine(Word, FontSize, Rect);
}