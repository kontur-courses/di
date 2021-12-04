#nullable enable
using System;
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
            Word = word ?? throw new ArgumentNullException(nameof(word));
            Location = location;
            Font = font ?? throw new ArgumentNullException(nameof(font));
        }

        public override int GetHashCode()
        {
            return Word.GetHashCode() + Location.GetHashCode() + Font.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not WordLayout other)
                return false;

            return GetHashCode() == other.GetHashCode();
        }
    }
}