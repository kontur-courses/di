using System.Drawing;
using TagsCloudVisualization.DrawableContainers.Builders;
using TagsCloudVisualization.ImageCreators;
using TagsCloudVisualization.WordsPreprocessors;
using TagsCloudVisualization.WordsProvider;
using TagsCloudVisualization.WordsToTagTransformers;

namespace TagsCloudVisualization
{
    public class Visualizer
    {
        private readonly IFileReadService fileReadService;
        private readonly IImageCreator imageCreator;
        private readonly IWordsPreprocessor wordsProcessor;
        private readonly IWordsToTagTransformer wordsToTagTransformer;
        private readonly IDrawableContainerBuilder drawableContainerBuilder;

        public Visualizer(IFileReadService fileReadService, IWordsPreprocessor wordsProcessor,
            IWordsToTagTransformer wordsToTagTransformer, IDrawableContainerBuilder drawableContainerBuilder, 
            IImageCreator imageCreator)
        {
            this.fileReadService = fileReadService;
            this.wordsProcessor = wordsProcessor;
            this.wordsToTagTransformer = wordsToTagTransformer;
            this.drawableContainerBuilder = drawableContainerBuilder;
            this.imageCreator = imageCreator;
        }

        public Image Visualize()
        {
            var words = fileReadService.GetFileContent();
            var preparedWords = wordsProcessor.Preprocess(words);
            var tags = wordsToTagTransformer.Transform(preparedWords);
            
            foreach (var tag in tags)
            {
                drawableContainerBuilder.AddTag(tag);
            }

            var drawableContainer = drawableContainerBuilder.Build();
            return imageCreator.Draw(drawableContainer);
        }
    }
}