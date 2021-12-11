using TagsCloudVisualization.DrawableContainers.Builders;
using TagsCloudVisualization.ImageCreators;
using TagsCloudVisualization.Saver;
using TagsCloudVisualization.WordsPreprocessors;
using TagsCloudVisualization.WordsProvider;
using TagsCloudVisualization.WordsToTagTransformers;

namespace TagsCloudVisualization
{
    public class Visualizer
    {
        private readonly IFileReadService fileReadService;
        private readonly IImageCreator imageCreator;
        private readonly IImageSaver imageSaver;
        private readonly IWordsPreprocessor wordsProcessor;
        private readonly IWordsToTagTransformer wordsToTagTransformer;
        private readonly IDrawableContainerBuilder drawableContainerBuilder;

        public Visualizer(IFileReadService fileReadService, IWordsPreprocessor wordsProcessor,
            IWordsToTagTransformer wordsToTagTransformer, IDrawableContainerBuilder drawableContainerBuilder, 
            IImageCreator imageCreator, IImageSaver imageSaver)
        {
            this.fileReadService = fileReadService;
            this.wordsProcessor = wordsProcessor;
            this.wordsToTagTransformer = wordsToTagTransformer;
            this.drawableContainerBuilder = drawableContainerBuilder;
            this.imageCreator = imageCreator;
            this.imageSaver = imageSaver;
        }

        public void Visualize()
        {
            var words = fileReadService.GetFileContent();
            var preparedWords = wordsProcessor.Preprocess(words);
            var tags = wordsToTagTransformer.Transform(preparedWords);
            
            foreach (var tag in tags)
            {
                drawableContainerBuilder.AddTag(tag);
            }

            var drawableContainer = drawableContainerBuilder.Build();
            var image = imageCreator.Draw(drawableContainer);
            imageSaver.Save(image);
        }
    }
}