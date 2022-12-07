using System;
using System.Drawing;
using TagCloud2.Interfaces;

namespace TagCloud2
{
    public class SpiralTagCloudBitmapDrawer : ITagCloudBitmapDrawer
    {
        public Bitmap Bitmap { get; }
        private readonly Graphics _graphics;
        private readonly string _fontName;
        private readonly int _minFontSize, _maxFontSize;
        private readonly Color maxFontColor, minFontColor, _backgroundColor;


        public SpiralTagCloudBitmapDrawer(Size size, string fontName, int minFontSize, int maxFontSize, Color minFontColor, Color maxFontColor, Color backgroundColor)
        {
            _fontName = fontName;
            _minFontSize = minFontSize;
            _maxFontSize = maxFontSize;
            this.minFontColor = minFontColor;
            this.maxFontColor = maxFontColor;
            _backgroundColor = backgroundColor;
            Bitmap = new Bitmap(size.Width, size.Height);
            _graphics = Graphics.FromImage(Bitmap);
            _graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            _graphics.FillRegion(
                new SolidBrush(_backgroundColor), 
                new Region(new Rectangle(0, 0, size.Width, size.Height))
                );
        }

        public void DrawRectangles(Rectangle[] rectangles)
        {
            _graphics.DrawRectangles(Pens.Black, rectangles);
        }

        public void DrawTags(Rectangle[] rectangles, Tuple<string, int>[] tags)
        {
            var diff = _maxFontSize != _minFontSize ? _maxFontSize - _minFontSize : 1;

            for (var i = 0; i < rectangles.Length; i++)
            {
                var coef = ((double)tags[i].Item2 - _minFontSize) / diff;

                var color = Color.FromArgb(
                    (int)((maxFontColor.R * coef) + (minFontColor.R * (1 - coef))),
                    (int)((maxFontColor.G * coef) + (minFontColor.G * (1 - coef))),
                    (int)((maxFontColor.B * coef) + (minFontColor.B * (1 - coef)))
                );


                //_graphics.DrawRectangle(Pens.Black, rectangles[i]);
                _graphics.DrawString(tags[i].Item1, new Font("Consolas", tags[i].Item2), new SolidBrush(color), rectangles[i]);
            }
        }

        public SizeF GetStringInRectangleSize(string s, int fontSize)
        {
            var sizeF = _graphics.MeasureString(s, new Font(_fontName, fontSize));
            return new SizeF(sizeF.Width + 1, sizeF.Height);
        }
    }
}