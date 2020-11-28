using System.Drawing;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.CloudVisualizer
{
    internal class CloudVisualizer : ICloudVisualizer
    {
        private readonly ICloudGenerator cloudGenerator;
        private readonly IFrequencyDictionaryGenerator dictionaryGenerator;
        private readonly IImageGenerator imageGenerator;
        private readonly IFileReader inputFileReader;

        public CloudVisualizer(IFileReader inputFileReader, IFrequencyDictionaryGenerator dictionaryGenerator,
            ICloudGenerator cloudGenerator, IImageGenerator imageGenerator)
        {
            this.inputFileReader = inputFileReader;
            this.dictionaryGenerator = dictionaryGenerator;
            this.cloudGenerator = cloudGenerator;
            this.imageGenerator = imageGenerator;
        }

        public Bitmap Visualize(string inputFileName, ImageSettings imageSettings)
        {
            var lines = inputFileReader.ReadLines(inputFileName);
            var dictionary = dictionaryGenerator.GenerateFrequencyDictionary(lines);
            var cloud = cloudGenerator.GenerateCloud(dictionary, imageSettings.FontName);
            return imageGenerator.GenerateImage(cloud, imageSettings);
        }
    }
}