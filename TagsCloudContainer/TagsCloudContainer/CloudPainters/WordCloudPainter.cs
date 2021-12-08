using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public class WordCloudPainter : IWordCloudPainter
    {
        private IWordCloudCreator cloudCreater;

        public WordCloudPainter(IWordCloudCreator cloudCreater)
        {
            this.cloudCreater = cloudCreater;
        }

        public Bitmap PaintWords(ImageSettings imageSettings)
        {
            var imageSize = imageSettings.ImageSize;
            var bitmap = new Bitmap(imageSize.Width, imageSize.Height);

            var graphic = Graphics.FromImage(bitmap);
            graphic.Clear(imageSettings.BackgroundColor);
            var words = cloudCreater.GetWordCloud(graphic, imageSettings);

            foreach (var word in words)
            {
                graphic.DrawString(word.Text, word.Font, new SolidBrush(imageSettings.TextColor), word.Border);
                //graphic.DrawRectangle(Pens.Black, Rectangle.Round(word.Border));
            }

            return bitmap;
        }
    }
}
