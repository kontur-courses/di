using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudApplication.ColorSchemes;
using TagCloudApplication.Savers;

namespace TagCloudApplication
{
    public class TagCloud
    {
        private FontFamily fontFamily = FontFamily.GenericSerif;
        private IColorScheme colorScheme = new SimpleColorScheme();
        private readonly List<(string word, Rectangle rect)> tempRect;
        private readonly Size imSize;

        public TagCloud(List<(string word,Rectangle rect)> tempRect, Size imSize)
        {
            this.tempRect = tempRect;
            this.imSize = imSize;
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

        public void SaveAsImage(string fileName, ISaver imageSaver)
        {
            var tCImage = CreateImage();
            imageSaver.Save(fileName, tCImage);
        }      

        private Bitmap CreateImage()
        {
            var resultBitmap = new Bitmap(imSize.Width, imSize.Height);
            using (var g = Graphics.FromImage(resultBitmap))
            {
                g.FillRectangle(new SolidBrush(colorScheme.BackColor),
                    new Rectangle(0, 0, imSize.Width, imSize.Height));
                g.TranslateTransform(imSize.Width / 2, imSize.Height / 2);
                foreach (var pair in tempRect)
                {
                    var currentFont = CreateFont(pair.rect, pair.word.Length);
                    g.DrawString(pair.word,
                        currentFont,
                        new SolidBrush(colorScheme.GetNextColorForWord()),
                        pair.rect);
                }
            }

            return resultBitmap;
        }

        private Font CreateFont(Rectangle rect, int wordLength)
        {
            if (wordLength+1 > rect.Width) return new Font(fontFamily, 1);
            float emSize = rect.Width / wordLength+1;
            
            while (new Font(fontFamily,emSize).Height > rect.Height)
                emSize -= 1.0f;
            while (new Font(fontFamily, emSize).Height < rect.Height)
                emSize += 1.0f;

            return new Font(fontFamily,emSize);
        }


    }
}
