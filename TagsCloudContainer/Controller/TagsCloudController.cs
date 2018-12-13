using System.Collections.Generic;
using TagsCloudContainer.Configuration;
using TagsCloudContainer.DataReader;
using TagsCloudContainer.ImageWriter;
using TagsCloudContainer.Tag;
using TagsCloudContainer.TagsGenerator;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.WordsCounter;

namespace TagsCloudContainer.Controller
{
    public class TagsCloudController : ITagsCloudController
    {
        private IConfiguration Configuration { get; }
        private IDataReader Reader { get; }
        private IWordsCounter WordsCounter { get; }
        private ITagsGenerator TagsGenerator { get; }
        private IVisualizer Visualizer { get; }
        private IImageWriter ImageWriter { get; }

        private IEnumerable<string> words;
        private IEnumerable<string> Words =>
            words ?? (words = Reader.Read(Configuration.PathToWordsFile));

        private IDictionary<string, int> wordsFrequency;
        private IDictionary<string, int> WordsFrequency =>
            wordsFrequency ?? (wordsFrequency = WordsCounter.GetWordsFrequency(Words));

        private IEnumerable<ITag> tags;
        private IEnumerable<ITag> Tags =>
            tags ?? (tags = TagsGenerator.GenerateTags(WordsFrequency));

        public TagsCloudController(IConfiguration configuration, IDataReader reader, IWordsCounter wordsCounter, ITagsGenerator tagsGenerator, IVisualizer visualizer, IImageWriter imageWriter)
        {
            Configuration = configuration;
            Reader = reader;
            WordsCounter = wordsCounter;
            TagsGenerator = tagsGenerator;
            Visualizer = visualizer;
            ImageWriter = imageWriter;
        }

        public byte[] Visualize()
        {
            return Visualizer.Visualize(Tags);
        }

        public void Save()
        {
            var image = Visualize();
            ImageWriter.Write(image, Configuration.OutFileName, "png", Configuration.DirectoryToSave);
        }
    }
}