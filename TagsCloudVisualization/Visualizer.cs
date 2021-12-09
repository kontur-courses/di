using System.Linq;
using TagsCloudVisualization.ImageCreator;
using TagsCloudVisualization.Saver;
using TagsCloudVisualization.TagToDrawableTransformer;
using TagsCloudVisualization.WordsPrepare;
using TagsCloudVisualization.WordsProvider;
using TagsCloudVisualization.WordsToTagTransformer;

namespace TagsCloudVisualization
{
    public class Visualizer
    {
        private readonly IFileReadService fileReadService;
        private readonly IImageCreator imageCreator;
        private readonly IImageSaver imageSaver;
        private readonly ITagToDrawableTransformer tagToDrawableTransformer;
        private readonly IWordsPreparer wordsPreparer;
        private readonly IWordsToTagTransformer wordsToTagTransformer;

        public Visualizer(IFileReadService fileReadService, IWordsPreparer wordsPreparer,
            IWordsToTagTransformer wordsToTagTransformer,
            ITagToDrawableTransformer tagToDrawableTransformer, IImageCreator imageCreator, IImageSaver imageSaver)
        {
            this.fileReadService = fileReadService;
            this.wordsPreparer = wordsPreparer;
            this.wordsToTagTransformer = wordsToTagTransformer;
            this.tagToDrawableTransformer = tagToDrawableTransformer;
            this.imageCreator = imageCreator;
            this.imageSaver = imageSaver;
        }

        public void Visualize()
        {
            var words = fileReadService.Read();
            var preparedWords = wordsPreparer.Prepare(words);
            var tags = wordsToTagTransformer.Transform(preparedWords);
            var drawableTags = tagToDrawableTransformer.Transform(tags.ToList());
            var image = imageCreator.Draw(drawableTags);
            imageSaver.Save(image);
        }
    }
}