using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer
{
    public class Compositor
    {
        private ImageSetting imageSetting;
        private IWordsSelector wordSelector;
        private ICloudLayouter layouter;

        public Compositor(IWordsSelector wordSelector, ICloudLayouter layouter, ImageSetting imageSetting)
        {
            this.wordSelector = wordSelector;
            this.imageSetting = imageSetting;
            this.layouter = layouter;
        }

        public void Composite()
        {
            var words = new HashSet<(Rectangle, LayoutWord)>();
            foreach (var layoutWord in wordSelector.Select())
            {
                var size = new Size(layoutWord.Word.Length * layoutWord.Count,
                    layoutWord.Font.Height * layoutWord.Count);
                var rectangle = layouter.PutNextRectangle(size);
                words.Add((rectangle, layoutWord));
            }

            using (var bitmap = new Bitmap(imageSetting.Width, imageSetting.Height))
            {
                var graphic = Graphics.FromImage(bitmap);
                foreach (var (rectangle, layoutWord) in words)
                {
                    graphic.DrawString(layoutWord.Word, layoutWord.Font, layoutWord.Brush, rectangle);                 
                }

                bitmap.Save("WordsCloud.png",System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}