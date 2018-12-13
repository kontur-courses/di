using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using TagCloud.Utility.Models.Tag;
using TagCloud.Utility.Models.TextReader;
using TagCloud.Utility.Models.WordFilter;
using TagCloud.Visualizer;

namespace TagCloud.Utility.Runner
{
    public class TagCloudRunner : ITagCloudRunner
    {
        private readonly ICloudVisualizer visualizer;
        private readonly ITagReader tagReader;
        private readonly IWordFilter wordFilter;
        private readonly ITextReader wordReader;
        private readonly string pathToWords;
        private readonly string pathToPicture;
        private readonly ImageFormat format;

        public TagCloudRunner(ICloudVisualizer visualizer, ITagReader tagReader, IWordFilter wordFilter, ITextReader wordReader, string pathToWords, string pathToPicture, ImageFormat format)
        {
            this.visualizer = visualizer;
            this.tagReader = tagReader;
            this.wordFilter = wordFilter;
            this.wordReader = wordReader;
            this.pathToWords = pathToWords;
            this.pathToPicture = pathToPicture;
            this.format = format;
        }
        public void Run()
        {
            var words = wordReader
                .ReadToEnd(pathToWords);
            var filteredWords = wordFilter
                .FilterWords(words);
            var tags = tagReader
                .ReadTags(filteredWords)
                .OrderByDescending(item => item.FontSize)
                .ToList();
            using (var picture = visualizer.CreatePictureWithItems(tags))
            {
                if (File.Exists(pathToPicture))
                    File.Delete(pathToPicture);
                picture.Save(pathToPicture, format);
            }
        }
    }
}