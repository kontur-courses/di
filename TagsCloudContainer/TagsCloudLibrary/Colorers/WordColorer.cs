using System;
using System.Drawing;

namespace TagsCloudLibrary.Colorers
{
    public class WordColorer : IColorer
    {
        public string Name { get; } = "Word";
        public Color ColorForWord(string word, double factor)
        {
            var random = new Random(word.GetHashCode());
            var color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            return color;
        }
    }
}
