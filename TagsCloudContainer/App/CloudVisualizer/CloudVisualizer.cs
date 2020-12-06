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
        private readonly IDataReaderFactory inputDataReaderFactory;
        private readonly ICloudPainter painter;
        private readonly ITextParserToFrequencyDictionary textParserToFrequencyDictionary;

        public CloudVisualizer(IDataReaderFactory inputDataReaderFactory,
            ITextParserToFrequencyDictionary textParserToFrequencyDictionary,
            ICloudGenerator cloudGenerator, ICloudPainter painter, IImageHolder imageHolder)
        {
            this.inputDataReaderFactory = inputDataReaderFactory;
            this.textParserToFrequencyDictionary = textParserToFrequencyDictionary;
            this.cloudGenerator = cloudGenerator;
            this.painter = painter;
            this.imageHolder = imageHolder;
        }

        public void Visualize()
        {
            var lines = inputDataReaderFactory.CreateDataReader().ReadLines();
            var frequencyDictionary = textParserToFrequencyDictionary.GenerateFrequencyDictionary(lines);
            var cloud = cloudGenerator.GenerateCloud(frequencyDictionary);
            painter.Paint(cloud, imageHolder.StartDrawing());
            imageHolder.UpdateUi();
        }
    }
}