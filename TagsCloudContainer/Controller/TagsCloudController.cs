using TagsCloudContainer.Configuration;
using TagsCloudContainer.DataReader;
using TagsCloudContainer.ImageWriter;
using TagsCloudContainer.Preprocessor;
using TagsCloudContainer.TagsGenerator;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.WordsCounter;

namespace TagsCloudContainer.Controller
{
    public class TagsCloudController : ITagsCloudController
    {
        private readonly IConfiguration configuration;
        private readonly IDataReader reader;
        private readonly IPreprocessor preprocessor;
        private readonly IWordsCounter wordsCounter;
        private readonly ITagsGenerator tagsGenerator;
        private readonly IVisualizer visualizer;
        private readonly IImageWriter imageWriter;

        public TagsCloudController(
            IConfiguration configuration,
            IDataReader reader,
            IPreprocessor preprocessor,
            IWordsCounter wordsCounter,
            ITagsGenerator tagsGenerator,
            IVisualizer visualizer,
            IImageWriter imageWriter)
        {
            this.configuration = configuration;
            this.reader = reader;
            this.preprocessor = preprocessor;
            this.wordsCounter = wordsCounter;
            this.tagsGenerator = tagsGenerator;
            this.visualizer = visualizer;
            this.imageWriter = imageWriter;
        }

        public void Save()
        {
            var words = reader.Read(configuration.PathToWordsFile);
            var preparedWords = preprocessor.PrepareWords(words);
            var wordsFrequency = wordsCounter.GetWordsFrequency(preparedWords);
            var tags = tagsGenerator.GenerateTags(wordsFrequency);
            var image = visualizer.Visualize(tags);
            imageWriter.Write(image, configuration.OutFileName, configuration.ImageFormat.ToString(), configuration.DirectoryToSave);
        }
    }
}