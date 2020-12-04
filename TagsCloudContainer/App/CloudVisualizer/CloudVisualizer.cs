using System.Drawing;
using System.IO;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.CloudGenerator;
using TagsCloudContainer.Infrastructure.CloudVisualizer;
using TagsCloudContainer.Infrastructure.DataReader;
using TagsCloudContainer.Infrastructure.TextParserToFrequencyDictionary;

namespace TagsCloudContainer.App.CloudVisualizer
{
    internal class CloudVisualizer : ICloudVisualizer
    {
        private readonly ICloudGenerator cloudGenerator;
        private readonly IImageHolder imageHolder;
        private readonly IDataReader inputDataReader;
        private readonly ICloudLayouterFactory layouterFactory;
        private readonly ICloudPainter painter;
        private readonly ITextParserToFrequencyDictionary textParserToFrequencyDictionary;

        public CloudVisualizer(IDataReader inputDataReader,
            ITextParserToFrequencyDictionary textParserToFrequencyDictionary,
            ICloudGenerator cloudGenerator, ICloudPainter painter, IImageHolder imageHolder,
            ICloudLayouterFactory layouterFactory)
        {
            this.inputDataReader = inputDataReader;
            this.textParserToFrequencyDictionary = textParserToFrequencyDictionary;
            this.cloudGenerator = cloudGenerator;
            this.painter = painter;
            this.imageHolder = imageHolder;
            this.layouterFactory = layouterFactory;
        }

        public void Visualize(AppSettings appSettings)
        {
            var lines = inputDataReader.ReadLines();
            var frequencyDictionary = textParserToFrequencyDictionary.GenerateFrequencyDictionary(lines);
            var cloud = cloudGenerator.GenerateCloud(layouterFactory.CreateCloudLayouter(
                appSettings.LayouterAlgorithm,
                new Size(appSettings.ImageSettings.Width, appSettings.ImageSettings.Height)), frequencyDictionary);
            painter.Paint(cloud, imageHolder.StartDrawing());
            var outputFile = Path.GetFullPath(Path.Combine(
                Directory.GetCurrentDirectory(), "..", "..", "..", "cloud.bmp"));
            imageHolder.UpdateUi();
            imageHolder.SaveImage(outputFile);
        }
    }
}