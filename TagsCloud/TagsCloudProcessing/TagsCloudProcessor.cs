using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.ImageProcessing.ImageBuilders;
using TagsCloud.ImageProcessing.SaverImage;
using TagsCloud.Layouter;
using TagsCloud.TextProcessing;
using TagsCloud.TextProcessing.WordConfig;

namespace TagsCloud.TagsCloudProcessing
{
    public class TagsCloudProcessor
    {
        private readonly ILayouter layouter;
        private readonly IWordsConfig wordsConfig;
        private readonly IImageConfig imageConfig;
        private readonly TextOperator textOperator;
        private readonly IImageBuilder imageBuilder;
        private readonly IImageSaver imageSaver;

        public TagsCloudProcessor(ILayouter layouter, IWordsConfig wordsConfig, TextOperator textOperator,
            IImageBuilder imageBuilder, IImageSaver imageSaver, IImageConfig imageConfig)
        {
            this.layouter = layouter;
            this.wordsConfig = wordsConfig;
            this.textOperator = textOperator;
            this.imageSaver = imageSaver;
            this.imageBuilder = imageBuilder;
            this.imageConfig = imageConfig;
        }

        public Bitmap CreateCloud()
        {
            var imageSize = imageConfig.ImageSize;
            layouter.SetCenter(new Point(imageSize.Width / 2, imageSize.Height / 2));

            var words = textOperator.ReadFromFile(wordsConfig.Path).OrderByDescending(info => info.Frequence).ToList();
            var tags = new List<Tag>();

            var count = 40;
            foreach (var word in words.Take(30))
            {
                var currentFont = new Font(wordsConfig.FontName.FontFamily, count);
                var size = TextRenderer.MeasureText(word.Value, currentFont);
                tags.Add(new Tag(word.Value, layouter.PutNextRectangle(size), currentFont));
                count--;
            }

            var image = imageBuilder.BuildImage(tags);
            imageSaver.SaveImageWithConfig(image, imageConfig);

            return image;
        }
    }
}
