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
        public List<(string word, Rectangle rect)> Words { get; }
        public Size ImageSize { get; }

        public TagCloud(List<(string word,Rectangle rect)> words, Size imageSize)
        {
            Words = words;
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
                float emSize = pair.rect.Width / pair.word.Length/1.7f;
                g.DrawString(pair.word,
                    new Font(fontFamily, emSize),
                    new SolidBrush(colorScheme.GetNextColorForWord()),
                    pair.rect);
            }
            return resultBitmap;
        }

    }
}
