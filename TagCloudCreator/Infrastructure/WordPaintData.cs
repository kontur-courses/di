using System.Drawing;

namespace TagCloudCreator.Infrastructure;

public readonly record struct WordPaintData(string Word, Font Font, Rectangle Rect)
{
    public bool Equals(WordPaintData other) =>
        Word == other.Word &&
        Font.Equals(other.Font) &&
        Rect.Equals(other.Rect);

    public override int GetHashCode() =>
        HashCode.Combine(Word, Font, Rect);
}