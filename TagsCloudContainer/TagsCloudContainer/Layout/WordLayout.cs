using System.Drawing;

namespace TagsCloudContainer.Layout
{
    public class WordLayout
    {
        public string Word { get; }
        public Point Location { get; }
        public Font Font { get; }

        public WordLayout(string word, Point location, Font font)
        {
            Word = word;
            Location = location;
            Font = font;
        }

        public override int GetHashCode() =>
            Word.GetHashCode() + Location.GetHashCode() + Font.GetHashCode();

        public override bool Equals(object? obj)
        {
            if (obj is not WordLayout other)
                return false;

            return GetHashCode() == other.GetHashCode();
        }
    }
}