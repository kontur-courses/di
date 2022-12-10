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
        [TestCase(null, "defaultWordsCloud.png", TestName = "with default words")]
        [TestCase("aboutKonturWords.txt", "wordsCloud.png", TestName = "with words collection")]
        public void GenerateTagCloud(string wordDictionaryPath, string picturePath)
        {
            var cloudLayouter = new CircularCloudLayouter(() => new SpiralPointGenerator());
            var wordsReader = new SingleWordInRowTextFileReader();
            if(wordDictionaryPath != null)
                wordsReader.Open(wordDictionaryPath);
            var boringWordsStorage = new TextFileBoringWordsStorage();
            var wordPreprocessor = new SimpleWordPreprocessor(boringWordsStorage);
            var tagCloudCreator = new WordTagCloudCreator(wordsReader, cloudLayouter, wordPreprocessor);
            var settings = new TagCloudVisualizationSettings();
            var visualization = new TagCloudBitmapVisualization(tagCloudCreator);
            visualization.Save(picturePath, settings);

            File.Exists(picturePath).Should().BeTrue($"file {picturePath} must be generated");
        }
    }
}
