using System.Drawing;
using TagsCloudGenerator.CloudLayouter;
using TagsCloudGenerator.FileReaders;
using TagsCloudGenerator.Saver;
using TagsCloudGenerator.Visualizer;
using TagsCloudGenerator.WordsHandler;

namespace TagsCloudGenerator
{
    public class CloudGenerator : ICloudGenerator
    {
        private readonly IFileReader reader;
        private readonly IWordHandler handler;
        private readonly ICloudLayouter layouter;
        private readonly ICloudVisualizer visualizer;
        private readonly IImageSaver saver;

        public CloudGenerator(
            IFileReader reader,
            IWordHandler handler,
            ICloudLayouter layouter,
            ICloudVisualizer visualizer,
            IImageSaver saver)
        {
            this.reader = reader;
            this.handler = handler;
            this.layouter = layouter;
            this.visualizer = visualizer;
            this.saver = saver;
        }

        public void Generate(string inputPath, string outputPath, ImageSettings settings)
        {
            var words = reader.ReadWords(inputPath);
            var validWords = handler.GetValidWords(words);

            var cloud = layouter.LayoutWords(validWords, settings.Font);
            var bitmap = visualizer.Draw(cloud, settings);
            saver.Save(bitmap, outputPath, settings.ImageFormat);
        }
    }
}