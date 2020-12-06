using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace TagsCloudContainer
{
    class CloudDrawer : ICloudDrawer
    {
        private ICloudLayouter cloudLayouter;
        public IImageSaver ImageSaver { get; set; }
        public IColorProvider ColorProvider { get; set; }
        public int ImageSize { get; set; }


        public CloudDrawer(ICloudLayouter cloudLayouter, IColorProvider colorProvider, IImageSaver imageSaver)
        {
            this.cloudLayouter = cloudLayouter;
            ImageSize = 300;
            cloudLayouter.ChangeCenter(new Point(ImageSize / 2, ImageSize / 2));
            ImageSaver = imageSaver;
            ColorProvider = colorProvider;
        }

        public void DrawCloud(List<WordWithFont> words, string targetPath, string imageName)
        {
            var bitmap = new Bitmap(ImageSize, ImageSize);
            var graphics = Graphics.FromImage(bitmap);
            var layout = MakeLayout(words, graphics);
            graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, ImageSize, ImageSize));
            for (var i = 0; i < words.Count; i++)
            {
                graphics.DrawString(words[i].Word, words[i].Font, new SolidBrush(ColorProvider.GetNextColor()), layout[i].Location);
            }
            ImageSaver.Save(targetPath, imageName, bitmap);
        }

        private List<Rectangle> MakeLayout(IEnumerable<WordWithFont> words, Graphics graphics)
        {
            cloudLayouter.Reset();
            foreach (var word in words)
            {
                var wordSize = graphics.MeasureString(word.Word, word.Font);
                cloudLayouter.PutNextRectangle(wordSize.ToSize());
            }

            return cloudLayouter.Rectangles;
        }

        public void ChangeImageSize(int newSize)
        {
            ImageSize = newSize;
            cloudLayouter.ChangeCenter(new Point(ImageSize / 2, ImageSize / 2));
        }
    }
}
