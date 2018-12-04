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
        private readonly Size imageSize;

        public TagCloud(List<(string word,Rectangle rect)> words, Size imageSize)
        {
            Words = words;
            this.imageSize = imageSize;
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
            var resultBitmap = new Bitmap(imageSize.Width, imageSize.Height);
            var g = Graphics.FromImage(resultBitmap);
            g.FillRectangle(new SolidBrush(colorScheme.BackColor), new Rectangle(0,0, imageSize.Width, imageSize.Height));
            g.TranslateTransform(imageSize.Width/2, imageSize.Height/2);
            foreach (var pair in Words)
            {
                float emSize = pair.rect.Width / pair.word.Length;
                g.DrawString(pair.word,
                    new Font(fontFamily, emSize),
                    new SolidBrush(colorScheme.GetNextColorForWord()),
                    pair.rect);
            }
            return resultBitmap;
        }

    }
}
