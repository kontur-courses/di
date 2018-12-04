using TagsCloudVisualization;
using TagsCloudVisualization.Visualizing;

namespace TagsCloudConsole
{
    class App
    {
        private readonly IReader reader;
        private readonly CloudBuilder cloudBuilder;
        private readonly TagsCloudVisualizer cloudVisualizer;
        private readonly string outputFileName;

        public App(IReader reader, CloudBuilder cloudBuilder, TagsCloudVisualizer cloudVisualizer, string outputFileName)
        {
            this.reader = reader;
            this.cloudBuilder = cloudBuilder;
            this.cloudVisualizer = cloudVisualizer;
            this.outputFileName = outputFileName;
        }

        public void Run()
        {
            var words = reader.ReadAllWords();
            var tagCloud = cloudBuilder.BuildTagCloud(words);
            var picture = cloudVisualizer.GetPictureOfRectangles(tagCloud);
            picture.Save(outputFileName);
        }
    }
}
