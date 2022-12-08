using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
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
        [TestCase("aboutKonturWords.txt", "wordsCloud.png")]
        public void GenerateTagCloud(string wordDictionaryPath, string picturePath)
        {
            var center = new Point();
            var spiralPointGenerator = new SpiralPointGenerator(center);
            var cloudLayouter = new CircularCloudLayouter(spiralPointGenerator);
            var wordsReader = new SingleWordInRowTextFileReader(wordDictionaryPath);
            var boringWordsStorage = new TextFileBoringWordsStorage();
            var wordPreprocessor = new SimpleWordPreprocessor(wordsReader, boringWordsStorage);
            var tagCloud = new WordTagCloudCreator(cloudLayouter, wordPreprocessor).GenerateTagCloud();
            var visualization = new TagCloudBitmapVisualization(tagCloud);
            visualization.Save(picturePath);

            File.Exists(picturePath).Should().BeTrue($"file {picturePath} must be generated");
        }
    }
}
