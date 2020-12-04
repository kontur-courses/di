using System;
using System.Drawing;

namespace TagCloud.Visualization.WordsColorings
{
    internal class WordsColoringLineBringhtness : IWordsColoring
    {
        private readonly Color color;

        internal WordsColoringLineBringhtness(Color color) => this.color = color;
        public Color GetColor(string word, Rectangle location, TagCloud cloud)
        {
            var center = new Point(
                (location.Left + location.Right) / 2, 
                (location.Top + location.Bottom) / 2);
            var length = Math.Sqrt(
                (center.X - cloud.Center.X) * (center.X - cloud.Center.X) +
                (center.Y - cloud.Center.Y) * (center.Y - cloud.Center.Y));
            var bringhtness = length == 0 ? 255 : 255 / Math.Sqrt(length);
            return Color.FromArgb((int)bringhtness, color.R, color.G, color.B);
        }
    }
}
