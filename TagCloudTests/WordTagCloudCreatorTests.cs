using FluentAssertions;
using NUnit.Framework;
using System.IO;
using TagCloud;
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
        private ITagCloudVisualizationSettings settings;

        [SetUp]
        public void PrepareSettings()
        {
            settings = new TagCloudVisualizationSettings();
        }

        [TestCase("empty.txt", null, "withoutWordsCloud.png", TestName = "without words")]
        [TestCase(null, null, "defaultWordsCloud.png", TestName = "with default words")]
        [TestCase("aboutKonturWords.txt", @"BoringWordsRepositories\BoringWordsDictionary.txt", "wordsCloud.png", TestName = "with words collection")]
        public void WordTagCloudCreator_GenerateTagCloud(string wordDictionaryPath, string boringWordDictionaryPath, string picturePath)
        {
            File.Delete(picturePath);

            File.Exists(picturePath).Should().BeFalse($"file {picturePath} must be deleted");

            var tagCloud = PrepareTagCloud(wordDictionaryPath, boringWordDictionaryPath);
            var visualization = new WordTagCloudBitmapVisualization();
            visualization.SaveWithOriginSize(picturePath, tagCloud, settings);

            File.Exists(picturePath).Should().BeTrue($"file {picturePath} must be generated");
        }

        private ITagCloud PrepareTagCloud(string wordDictionaryPath, string boringWordDictionaryPath)
        {
            var wordsReader = new SingleWordInRowTextFileReader();
            if (wordDictionaryPath != null)
                wordsReader.SetFile(wordDictionaryPath);
            var boringWordsStorage = new TextFileBoringWordsStorage(new SingleWordInRowTextFileReader());
            if (boringWordDictionaryPath != null)
                boringWordsStorage.LoadBoringWords(boringWordDictionaryPath);
            var wordPreprocessor = new SimpleWordPreprocessor(wordsReader, boringWordsStorage);
            var spiralPointGeneratorFactory = (IPointGenerator.Factory)(() => new SpiralPointGenerator());
            var cloudLayouterFactory = (ICloudLayouter.Factory)(() => new CircularCloudLayouter(spiralPointGeneratorFactory));
            var tagCloudCreator = new WordTagCloudCreator(cloudLayouterFactory, wordPreprocessor, settings);
            return tagCloudCreator.GenerateTagCloud();
        }
    }
}
