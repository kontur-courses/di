using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudApplication
{
    public class TagCloud
    {
        private FontFamily fontFamily = FontFamily.GenericMonospace;
        private IColorScheme colorScheme;
        public List<(string word, Rectangle rect)> Words { get; }
        public Size ImageSize { get; }

        public TagCloud(List<(string word,Rectangle rect)> words, Size imageSize)
        {
            Words = words.OrderByDescending(tuple => tuple.rect.Size.Height*tuple.rect.Height).ToList();
            ImageSize = imageSize;
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

        public Bitmap CreateImage()
        {
            var resultBitmap = new Bitmap(ImageSize.Width, ImageSize.Height);
            var g = Graphics.FromImage(resultBitmap);
            g.FillRectangle(new SolidBrush(colorScheme.BackColor), new Rectangle(0,0, ImageSize.Width, ImageSize.Height));
            g.TranslateTransform(ImageSize.Width/2, ImageSize.Height/2);
            foreach (var pair in Words)
            {
                float emSize = pair.rect.Width / pair.word.Length/1.8f;
                g.DrawString(pair.word,
                    new Font(fontFamily, emSize),
                    new SolidBrush(colorScheme.GetNextColorForWord()),
                    pair.rect);
            }
            return resultBitmap;
        }

    }
}
