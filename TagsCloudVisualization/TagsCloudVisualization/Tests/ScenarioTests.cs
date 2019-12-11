using System;
using System.Collections.Generic;
using System.Drawing;
using Autofac;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using TagsCloudVisualization.Logic;
using TagsCloudVisualization.Services;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class ScenarioTests
    {
        private IContainer container;
        private string testTextDirectory;

        [OneTimeSetUp]
        public void InitializeTextsDirectory()
        {
            testTextDirectory = TestContext.CurrentContext.TestDirectory + "\\Tests\\TestTexts\\";
        }

        [SetUp]
        public void InitializeProperties()
        {
            container = EntryPoint.InitializeContainer();
        }

        [Test]
        public void ImageIsCreated_WithCorrectSize_AfterResizingImage()
        {
            var settings = container.Resolve<IImageSettingsProvider>();
            settings.ImageSettings.ImageSize = new Size(
                settings.ImageSettings.ImageSize.Width * 2,
                settings.ImageSettings.ImageSize.Height * 2
            );
            var visualizer = container.Resolve<IVisualizer>();

            var resultImage = visualizer.VisualizeTextFromFile(testTextDirectory + "animals.txt");

            resultImage.Size.Should().Be(settings.ImageSettings.ImageSize);
        }

        [Test]
        public void ImageIsCreated_WithoutBoringWords_WithCustomBoringWordsCollection()
        {
            var boringText = "bathroom\nbedroom\nkitchen";
            var boringWordsProvider = container.Resolve<IBoringWordsProvider>();
            var textParser = container.Resolve<IParser>();

            boringWordsProvider.BoringWords = new HashSet<string>(boringText.Split('\n'));

            textParser.ParseToTokens(boringText).Should().BeEmpty();
        }

        [TestCase("100tags.txt")]
        [TestCase("1000tags.txt")]
        public void ImageCreation_IsTimePermissible_OnBigAmountOfTags(string fileName)
        {
            var visualizer = container.Resolve<IVisualizer>();
            var millisecondsForEachWord = 5;
            var expectedTime = TextRetriever
                                   .RetrieveTextFromFile(testTextDirectory + fileName)
                                   .Split('\n')
                                   .Length
                               * millisecondsForEachWord;

            Action action = () => visualizer.VisualizeTextFromFile(testTextDirectory + fileName);

            action.ExecutionTime().Should().BeLessThan(expectedTime.Milliseconds());
        }
    }
}