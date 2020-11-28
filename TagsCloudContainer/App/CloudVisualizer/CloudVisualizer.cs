using System.Drawing;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.CloudVisualizer
{
    internal class CloudVisualizer : ICloudVisualizer
    {
        private readonly ICloudGenerator cloudGenerator;
        private readonly IDictionaryGenerator dictionaryGenerator;
        private readonly IImageGenerator imageGenerator;
        private readonly IFileReader inputFileReader;

        public CloudVisualizer(IFileReader inputFileReader, IDictionaryGenerator dictionaryGenerator,
            ICloudGenerator cloudGenerator, IImageGenerator imageGenerator)
        {
            this.inputFileReader = inputFileReader;
            this.dictionaryGenerator = dictionaryGenerator;
            this.cloudGenerator = cloudGenerator;
            this.imageGenerator = imageGenerator;
        }

        public Bitmap Visualize(string inputFileName, Size imageSize)
        {
            var lines = inputFileReader.ReadLines(inputFileName);
            var dictionary = dictionaryGenerator.GenerateDictionary(lines);
            var cloud = cloudGenerator.GenerateCloud(dictionary);
            return imageGenerator.GenerateImage(cloud, imageSize);
        }
    }
}