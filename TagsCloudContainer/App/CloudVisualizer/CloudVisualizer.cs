using System.Drawing;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure.CloudGenerator;
using TagsCloudContainer.Infrastructure.CloudVisualizer;
using TagsCloudContainer.Infrastructure.TextParserToFrequencyDictionary;
using TagsCloudContainer.Infrastructure.DataReader;

namespace TagsCloudContainer.App.CloudVisualizer
{
    internal class CloudVisualizer : ICloudVisualizer
    {
        private readonly ICloudGenerator cloudGenerator;
        private readonly ITextParserToFrequencyDictionary textParserToTextParserToFrequencyDictionary;
        private readonly IImageGenerator imageGenerator;
        private readonly IDataReader inputDataReader;

        public CloudVisualizer(IDataReader inputDataReader, ITextParserToFrequencyDictionary textParserToTextParserToFrequencyDictionary,
            ICloudGenerator cloudGenerator, IImageGenerator imageGenerator)
        {
            this.inputDataReader = inputDataReader;
            this.textParserToTextParserToFrequencyDictionary = textParserToTextParserToFrequencyDictionary;
            this.cloudGenerator = cloudGenerator;
            this.imageGenerator = imageGenerator;
        }

        public Bitmap Visualize(string inputFileName, ImageSettings imageSettings)
        {
            var lines = inputDataReader.ReadLines();
            var frequencyDictionary = textParserToTextParserToFrequencyDictionary.GenerateFrequencyDictionary(lines);
            var cloud = cloudGenerator.GenerateCloud(frequencyDictionary);
            return imageGenerator.GenerateImage(cloud);
        }
    }
}