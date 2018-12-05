using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudApplication.ColorSchemes;

namespace TagCloudApplication
{
    public class TagCloud
    {
        private FontFamily fontFamily = FontFamily.GenericSerif;
        private IColorScheme colorScheme = new SimpleColorScheme();
        private List<(string word, Rectangle rect)> tempRect;

        public TagCloud(List<(string word,Rectangle rect)> tempRect)
        {
            this.tempRect = tempRect;
        }

        public TagCloud ApplyColorScheme(IColorScheme colorScheme)
        {
            this.colorScheme = colorScheme;
            return this;
        }

        public TagCloud ApplyFontFamily(FontFamily fontFamily)
        {
            this.fontFamily = fontFamily;
            return this;
        }



        public void SaveAsImage(string fileName, Size imageSize, int borderWidth, ISaver imageSaver)
        {
            var tagCloudSize = new Size(imageSize.Width - 2*borderWidth, imageSize.Height - 2*borderWidth);
            var rects = ScaleRectangleToSize(tagCloudSize);

            var tCImage = CreateImage(rects, imageSize, borderWidth);
            imageSaver.Save(fileName, tCImage);
        }      

        private Bitmap CreateImage(List<(string word, Rectangle rect)> rects, Size imageSize, int borderWidth)
        {
            var resultBitmap = new Bitmap(imageSize.Width, imageSize.Height);
            var g = Graphics.FromImage(resultBitmap);
            g.FillRectangle(new SolidBrush(colorScheme.BackColor), new Rectangle(0,0, imageSize.Width, imageSize.Height));
            g.TranslateTransform(imageSize.Width/2, imageSize.Height/2);
            foreach (var pair in rects)
            {
                var currentFont = CreateFont(pair.rect, pair.word.Length);
                g.DrawString(pair.word,
                    currentFont,
                    new SolidBrush(colorScheme.GetNextColorForWord()),
                    pair.rect);
            }
            return resultBitmap;
        }

        private List<(string word, Rectangle rect)> ScaleRectangleToSize(Size tagCloudSize)
        {
            var mainSide = tagCloudSize.Height < tagCloudSize.Width ? tagCloudSize.Height : tagCloudSize.Width;
            var scale = mainSide / 100;
            return tempRect.Select(r => (r.word, new Rectangle(r.rect.X * scale, r.rect.Y * scale,
                (r.rect.Width * scale), (r.rect.Height * scale)))).ToList();
        }

        private Font CreateFont(Rectangle rect, int wordLenght)
        { 
            var emSize = rect.Width / wordLenght;
            while (new Font(fontFamily, emSize).Height > rect.Height)
                emSize -= 1;
            return new Font(fontFamily,emSize);
        }


    }
}
