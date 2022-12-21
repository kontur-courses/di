using System.Drawing;
using System.Text.RegularExpressions;
using TagsCloudContainer.Core.Layouter;
using TagsCloudContainer.Core.Drawer.Interfaces;

namespace TagsCloudContainer.Core.Drawer
{
    public class LayoutDrawer : ILayoutDrawer
    {
        private static readonly Regex ColorParser = new(@"argb\((?<alpha>\d{1,3}),(?<red>\d{1,3}),(?<green>\d{1,3}),(?<blue>\d{1,3})\)", RegexOptions.Compiled);

        private readonly Random _random;
        private readonly List<WordRectangle> _rectangles;

        public LayoutDrawer()
        {
            _random = new Random();
            _rectangles = new List<WordRectangle>();
        }

        public void AddRectangle(WordRectangle rectangle)
        {
            _rectangles.Add(rectangle);
        }

        public void Draw(Graphics graphics)
        {
            foreach (var rectangle in _rectangles)
            {
                var brush = new SolidBrush(StringToArgbColor(rectangle.FontColor));
                using var font = new Font(rectangle.FontFamily, rectangle.FontSize);
                graphics.DrawString(rectangle.Text, font, brush, rectangle.Rectangle.Location);
            }
        }

        private Color StringToArgbColor(string color)
        {
            if (color == "random")
            {
                return Color.FromArgb(
                    _random.Next(255), 
                    _random.Next(255), 
                    _random.Next(255));
            }

            var match = ColorParser.Match(color).Groups;

            return Color.FromArgb(
                int.Parse(match["alpha"].Value), 
                int.Parse(match["red"].Value), 
                int.Parse(match["green"].Value), 
                int.Parse(match["blue"].Value));
        }
    }
}