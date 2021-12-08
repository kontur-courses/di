using System;
using System.Linq;
using TagsCloudDrawer.ImageCreator;
using TagsCloudVisualization.DrawableFactory;
using TagsCloudVisualization.WordsPreprocessor;
using TagsCloudVisualization.WordsProvider;
using TagsCloudVisualization.WordsToTagsTransformers;

namespace TagsCloudVisualization
{
    public class TagsCloudVisualizer
    {
        private readonly IWordsProvider _wordsProvider;
        private readonly IWordsPreprocessor _preprocessor;
        private readonly ITagDrawableFactory _tagDrawableFactory;
        private readonly IWordsToTagsTransformer _transformer;
        private readonly IImageCreator _imageCreator;

        public TagsCloudVisualizer(IWordsProvider wordsProvider,
            IWordsPreprocessor preprocessor,
            ITagDrawableFactory tagDrawableFactory,
            IWordsToTagsTransformer transformer,
            IImageCreator imageCreator)
        {
            _wordsProvider = wordsProvider ?? throw new ArgumentNullException(nameof(wordsProvider));
            _preprocessor = preprocessor ?? throw new ArgumentNullException(nameof(preprocessor));
            _tagDrawableFactory = tagDrawableFactory ?? throw new ArgumentNullException(nameof(tagDrawableFactory));
            _transformer = transformer ?? throw new ArgumentNullException(nameof(transformer));
            _imageCreator = imageCreator ?? throw new ArgumentNullException(nameof(imageCreator));
        }

        public void Visualize(string filename)
        {
            var words = _wordsProvider.GetWords();
            var processedWords = _preprocessor.Process(words);
            var tags = _transformer.Transform(processedWords);
            var drawables = tags
                            .OrderByDescending(tag => tag.Weight)
                            .Select(_tagDrawableFactory.Create);
            _imageCreator.Create(filename, drawables);
        }
    }
}