using FluentAssertions;
using NUnit.Framework;
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
        [TestCase("empty.txt", null, "withoutWordsCloud.png", TestName = "without words")]
        [TestCase(null, null, "defaultWordsCloud.png", TestName = "with default words")]
        [TestCase("aboutKonturWords.txt", @"BoringWordsRepositories\BoringWordsDictionary.txt", "wordsCloud.png", TestName = "with words collection")]
        public void GenerateTagCloud(string wordDictionaryPath, string boringWordDictionaryPath, string picturePath)
        {
            File.Delete(picturePath);

            File.Exists(picturePath).Should().BeFalse($"file {picturePath} must be deleted");

            var wordsReader = new SingleWordInRowTextFileReader();
            if(wordDictionaryPath != null)
                wordsReader.Open(wordDictionaryPath);
            var boringWordsStorage = new TextFileBoringWordsStorage(new SingleWordInRowTextFileReader());
            if(boringWordDictionaryPath != null)
                boringWordsStorage.LoadBoringWords(boringWordDictionaryPath);
            var wordPreprocessor = new SimpleWordPreprocessor(wordsReader, boringWordsStorage);
            var spiralPointGeneratorFactory = (IPointGenerator.Factory)(() => new SpiralPointGenerator());
            var cloudLayouterFactory = (ICloudLayouter.Factory)(() => new CircularCloudLayouter(spiralPointGeneratorFactory));
            var settings = new TagCloudVisualizationSettings();
            var tagCloudCreatorFactory = (ITagCloudCreator.Factory)((cloudLayouterFactory, wordPreprocessor, settings) => new WordTagCloudCreator(cloudLayouterFactory, wordPreprocessor, settings));
            var visualization = new WordTagCloudBitmapVisualization(cloudLayouterFactory, wordPreprocessor, tagCloudCreatorFactory);
            visualization.Save(picturePath, settings);

            File.Exists(picturePath).Should().BeTrue($"file {picturePath} must be generated");
        }
    }
}
