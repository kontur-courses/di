using System.Linq;
using TagsCloudVisualization.ImageCreator;
using TagsCloudVisualization.Saver;
using TagsCloudVisualization.WordsPrepare;
using TagsCloudVisualization.WordsProvider;
using TagsCloudVisualization.WordsToTagTransformer;

namespace TagsCloudVisualization
{
    public class Visualizer
    {
        private readonly IFileReadService fileReadService;
        private readonly IWordsPreparer wordsPreparer;
        private readonly IWordsToTagTransformer wordsToTagTransformer;
        private readonly TagToDrawableTagTransformer tagToDrawableTagTransformer;
        private readonly IImageCreator imageCreator;
        private readonly IImageSaver imageSaver;

        public Visualizer(IFileReadService fileReadService, IWordsPreparer wordsPreparer,
            IWordsToTagTransformer wordsToTagTransformer,
            TagToDrawableTagTransformer tagToDrawableTagTransformer, IImageCreator imageCreator, IImageSaver imageSaver)
        {
            this.fileReadService = fileReadService;
            this.wordsPreparer = wordsPreparer;
            this.wordsToTagTransformer = wordsToTagTransformer;
            this.tagToDrawableTagTransformer = tagToDrawableTagTransformer;
            this.imageCreator = imageCreator;
            this.imageSaver = imageSaver;
        }

        public void Visualize()
        {
            var words = fileReadService.Read();
            var preparedWords = wordsPreparer.Prepare(words);
            var tags = wordsToTagTransformer.Transform(preparedWords);
            var drawableTags = tagToDrawableTagTransformer.Transform(tags.ToList());
            var image = imageCreator.Draw(drawableTags);
            imageSaver.Save(image);
        }
    }
}