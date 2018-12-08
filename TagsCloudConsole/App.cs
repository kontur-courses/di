using TagsCloudVisualization;
using TagsCloudVisualization.Visualizing;

namespace TagsCloudConsole
{
    class App
    {
        private readonly IFileReader reader;
        private readonly CloudBuilder cloudBuilder;
        private readonly TagsCloudVisualizer cloudVisualizer;
        private readonly string outputFileName;
        private readonly string wordsFileName;

        public App(IFileReader reader, CloudBuilder cloudBuilder, TagsCloudVisualizer cloudVisualizer, 
            string outputFileName, string wordsFileName)
        {
            this.reader = reader;
            this.cloudBuilder = cloudBuilder;
            this.cloudVisualizer = cloudVisualizer;
            this.outputFileName = outputFileName;
            this.wordsFileName = wordsFileName;
        }

        public void Run()
        {
            var words = reader.ReadAllWords(wordsFileName);
            var tagCloud = cloudBuilder.BuildTagCloud(words);
            var picture = cloudVisualizer.GetPictureOfRectangles(tagCloud);
            picture.Save(outputFileName);
        }
    }
}
