using FluentAssertions;
using NUnit.Framework;
using System.Drawing;
using System.IO;
using TagCloud.BoringWordsRepositories;
using TagCloud.CloudLayouters;
using TagCloud.PointGenerators;
using TagCloud.Readers;
using TagCloud.TagCloudCreators;
using TagCloud.TagCloudVisualizations;
using TagCloud.WordPreprocessors;

namespace TagCloudTests
{
    public class WordTagCloudCreatorTests
    {
        [TestCase("aboutKonturWords.txt", "wordsCloud.png")]
        public void GenerateTagCloud(string wordDictionaryPath, string picturePath)
        {
            var center = new Point();
            var spiralPointGenerator = new SpiralPointGenerator(center);
            var cloudLayouter = new CircularCloudLayouter(spiralPointGenerator);
            var wordsReader = new SingleWordInRowTextFileReader(wordDictionaryPath);
            var boringWordsStorage = new TextFileBoringWordsStorage();
            var wordPreprocessor = new SimpleWordPreprocessor(wordsReader, boringWordsStorage);
            var tagCloudCreator = new WordTagCloudCreator(cloudLayouter, wordPreprocessor);
            var settings = TagCloudVisualizationSettings.Default();
            var visualization = new TagCloudBitmapVisualization(tagCloudCreator);
            visualization.Save(picturePath, settings);

            File.Exists(picturePath).Should().BeTrue($"file {picturePath} must be generated");
        }
    }
}
