using TagsCloud.ImageProcessing.ImageBuilders;
using TagsCloud.ImageProcessing.SaverImage.Factory;
using TagsCloud.TagsCloudProcessing.TagsGeneratorFactory;
using TagsCloud.TextProcessing;

namespace TagsCloud.TagsCloudProcessing
{
    public class TagsCloudCreator
    {
        private readonly TextProcessor textProcessor;
        private readonly IImageBuilder imageBuilder;
        private readonly IImageSaverFactory imageSaverFactory;
        private readonly ITagsGeneratorFactory tagsGeneratorFactory;

        public TagsCloudCreator(TextProcessor textProcessor,
             IImageBuilder imageBuilder, IImageSaverFactory imageSaverFactory,
             ITagsGeneratorFactory tagsGeneratorFactory)
        {
            this.tagsGeneratorFactory = tagsGeneratorFactory;
            this.textProcessor = textProcessor;
            this.imageSaverFactory = imageSaverFactory;
            this.imageBuilder = imageBuilder;
        }

        public void CreateCloud(string textPath, string imageSavePath)
        {
            var words = textProcessor.ReadFromFile(textPath);
            var tags = tagsGeneratorFactory.Create().CreateTags(words);

            using var image = imageBuilder.BuildImage(tags);
            imageSaverFactory.Create().SaveImage(image, imageSavePath);
        }
    }
}
