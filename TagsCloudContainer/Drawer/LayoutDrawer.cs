using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.Drawer
{
    public class LayoutDrawer : ILayoutDrawer
    {
        private static Regex colorParser =
            new Regex(
                @"argb\((?<alpha>\d{1,3}),(?<red>\d{1,3}),(?<green>\d{1,3}),(?<blue>\d{1,3})\)");

        private readonly Bitmap bitmap;
        private readonly Graphics graphics;
        private readonly Random random;
        private readonly IOptions options;
        private IEnumerable<WordRectangle> rectangles;

        Graphics ILayoutDrawer.Graphics => graphics;
        Bitmap ILayoutDrawer.Bitmap => bitmap;

        public LayoutDrawer(IOptions options)
        {
            this.options = options;
            bitmap = new Bitmap(options.Width, options.Height);
            graphics = Graphics.FromImage(bitmap);
            random = new Random();
        }

        public void AddRectangles(IEnumerable<WordRectangle> rectangles)
        {
            this.rectangles = rectangles;
        }

        public void Draw()
        {
            foreach (var rectangle in rectangles)
            {
                var brush = new SolidBrush(StringToArgbColor(options.FontColor));
                using var arialFont = new Font(options.FontFamily, rectangle.FontSize);
                graphics.DrawString(rectangle.Text, arialFont, brush, rectangle.Rectangle.Location);
            }
        }

        private Color StringToArgbColor(string s)
        {
            if (s == "random")
                return Color.FromArgb(random.Next(255), random.Next(255), random.Next(255));
            var match = colorParser.Match(s).Groups;
            return Color.FromArgb(int.Parse(match["alpha"].Value), int.Parse(match["red"].Value),
                int.Parse(match["green"].Value), int.Parse(match["blue"].Value));
        }
    }
}