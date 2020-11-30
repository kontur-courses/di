using System.Drawing;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure.CloudGenerator;
using TagsCloudContainer.Infrastructure.CloudVisualizer;
using TagsCloudContainer.Infrastructure.DictionaryGenerator;
using TagsCloudContainer.Infrastructure.DataReader;

namespace TagsCloudContainer.App.CloudVisualizer
{
    internal class CloudVisualizer : ICloudVisualizer
    {
        private readonly ICloudGenerator cloudGenerator;
        private readonly IFrequencyDictionaryGenerator dictionaryGenerator;
        private readonly IImageGenerator imageGenerator;
        private readonly IDataReader inputDataReader;

        public CloudVisualizer(IDataReader inputDataReader, IFrequencyDictionaryGenerator dictionaryGenerator,
            ICloudGenerator cloudGenerator, IImageGenerator imageGenerator)
        {
            this.inputDataReader = inputDataReader;
            this.dictionaryGenerator = dictionaryGenerator;
            this.cloudGenerator = cloudGenerator;
            this.imageGenerator = imageGenerator;
        }

        public Bitmap Visualize(string inputFileName, ImageSettings imageSettings)
        {
            var lines = inputDataReader.ReadLines();
            var dictionary = dictionaryGenerator.GenerateFrequencyDictionary(lines);
            var cloud = cloudGenerator.GenerateCloud(dictionary);
            return imageGenerator.GenerateImage(cloud);
        }
    }
}