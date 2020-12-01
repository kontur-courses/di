using System.Drawing;
using TagsCloud.ImageProcessing.Config;
using TagsCloud.ImageProcessing.ImageBuilders;
using TagsCloud.ImageProcessing.SaverImage;
using TagsCloud.Layouter.Factory;
using TagsCloud.TagsCloudProcessing.TagsGeneratorFactory;
using TagsCloud.TextProcessing;
using TagsCloud.TextProcessing.WordConfig;

namespace TagsCloud.TagsCloudProcessing
{
    public class TagsCloudProcessor
    {
        private readonly ILayouterFactory layouterFactory;
        private readonly IWordsConfig wordsConfig;
        private readonly IImageConfig imageConfig;
        private readonly TextProcessor textOperator;
        private readonly IImageBuilder imageBuilder;
        private readonly IImageSaver imageSaver;
        private readonly ITagsGeneratorFactory tagsGeneratorFactory;

        public TagsCloudProcessor(ILayouterFactory layouterFactory, IWordsConfig wordsConfig, TextProcessor textOperator,
            IImageBuilder imageBuilder, IImageSaver imageSaver,
            IImageConfig imageConfig, ITagsGeneratorFactory tagsGeneratorFactory)
        {
            this.layouterFactory = layouterFactory;
            this.tagsGeneratorFactory = tagsGeneratorFactory;
            this.wordsConfig = wordsConfig;
            this.textOperator = textOperator;
            this.imageSaver = imageSaver;
            this.imageBuilder = imageBuilder;
            this.imageConfig = imageConfig;
        }

        public void CreateCloud()
        {
            var imageSize = imageConfig.ImageSize;
            var layouter = layouterFactory.Create(new Point(imageSize.Width / 2, imageSize.Height / 2));
            var tagsGenerator = tagsGeneratorFactory.Create();

            var words = textOperator.ReadFromFile(wordsConfig.Path);
            var tags = tagsGenerator.CreateTags(words, layouter, wordsConfig.FontName);

            using var image = imageBuilder.BuildImage(tags);
            imageSaver.SaveImageWithConfig(image, imageConfig);
        }
    }
}
