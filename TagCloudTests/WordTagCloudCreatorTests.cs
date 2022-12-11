﻿using FluentAssertions;
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
        [TestCase(null, null, "defaultWordsCloud.png", TestName = "with default words")]
        [TestCase("aboutKonturWords.txt", @"BoringWordsRepositories\BoringWordsDictionary.txt", "wordsCloud.png", TestName = "with words collection")]
        public void GenerateTagCloud(string wordDictionaryPath, string boringWordDictionaryPath, string picturePath)
        {
            var wordsReader = new SingleWordInRowTextFileReader();
            if(wordDictionaryPath != null)
                wordsReader.Open(wordDictionaryPath);
            var boringWordsStorage = new TextFileBoringWordsStorage();//new SingleWordInRowTextFileReader());
            if(boringWordDictionaryPath != null)
                boringWordsStorage.LoadBoringWords(boringWordDictionaryPath);
            var wordPreprocessor = new SimpleWordPreprocessor();
            var spiralPointGeneratorFactory = (IPointGenerator.Factory)(() => new SpiralPointGenerator());
            var cloudLayouterFactory = (ICloudLayouter.Factory)(() => new CircularCloudLayouter(spiralPointGeneratorFactory));
            var tagCloudCreator = new WordTagCloudCreator(wordsReader, boringWordsStorage, cloudLayouterFactory, wordPreprocessor);
            var settings = new TagCloudVisualizationSettings();
            var visualization = new TagCloudBitmapVisualization(tagCloudCreator);
            visualization.Save(picturePath, settings);

            File.Exists(picturePath).Should().BeTrue($"file {picturePath} must be generated");
        }
    }
}