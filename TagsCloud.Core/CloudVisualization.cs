using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloud.Visualization;

namespace TagsCloud.Core
{
    public class CloudVisualization
    {
        private static readonly Random Random = new Random();
        private readonly Dictionary<char, Color> letterColor = new Dictionary<char, Color>();

        private readonly Image image;
        private readonly Palette palette;
        private readonly List<(string, int)> words;
        private readonly List<Rectangle> rectangles;
        private readonly ColorAlgorithm colorAlgorithm;
        private readonly List<Font> newFonts;

        public CloudVisualization(Image image, Palette palette,
            ColorAlgorithm colorAlgorithm, List<(string, int)> words, 
            List<Rectangle> rectangles, List<Font> newFonts)
        {
            this.colorAlgorithm = colorAlgorithm;
            this.palette = palette;
            this.words = words;
            this.image = image;
            this.rectangles = rectangles;
            this.newFonts = newFonts;
        }

        public void Paint()
        {
            using (var graphics = Graphics.FromImage(image))
            {
                var imageSize = image.Size;

                using (var brush = new SolidBrush(palette.BackgroundColor))
                    graphics.FillRectangle(brush, 0, 0, imageSize.Width, imageSize.Height);
                
                for (var i = 0; i < rectangles.Count; ++i)
                {
                    var drawFormat = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    graphics.DrawRectangle(new Pen(Color.White), rectangles[i]);

                    using (var brush = new SolidBrush(GetColor(words[i].Item1[0])))
                        graphics.DrawString(words[i].Item1, newFonts[i], brush, rectangles[i], drawFormat);
                }
            }
        }

        private Color GetColor(char letter)
        {
            switch (colorAlgorithm.Type)
            {
                case ColorAlgorithmType.MultiColor:
                    return palette.ForeColors[Random.Next(0, palette.ForeColors.Length)];
                case ColorAlgorithmType.SameFirstLetterHasSameColor:
                    if (!letterColor.ContainsKey(letter))
                        letterColor[letter] =
                            Color.FromArgb(Random.Next(0, 255), Random.Next(0, 255), Random.Next(0, 255));
                    return letterColor[letter];
                default:
                    return palette.ForeColor;
            }
        }
    }
}