using System;
using System.Drawing;
using FluentAssertions;
using GroboContainer.Core;
using NUnit.Framework;
using TagCloud.CloudVisualizerSpace;
using TagCloud.CloudVisualizerSpace.CloudViewConfigurationSpace;
using TagCloud.WordsPreprocessing;

namespace TagCloudTests.CloudVisualizerSpace
{
    public class CloudVisualizer_Should
    {
        private Container container;
        private Size imageSize;
        private Word testWord;
        private CloudVisualizer cloudVisualizer;
        private Size tempSize;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            container = TagCloud.Program.InitializeContainer();
            imageSize = new Size(1920, 1080);
        }

        [SetUp]
        public void SetUp()
        {
            cloudVisualizer = container.Create<CloudVisualizer>();
            testWord = new Word("Hello", SpeechPart.Noun)
            {
                Frequency = 0.25
            };
            tempSize = container.Get<CloudViewConfiguration>().ImageSize;
        }

        [TestCase(false, TestName = "Without Changing ImageSize")]
        [TestCase(true, TestName = "With Changing ImageSize")]
        public void GetCloud_HaveSameSizeAsCloudConfiguration(bool changed)
        {
            var configuration = container.Get<CloudViewConfiguration>();
            if (changed)
                configuration.ImageSize = imageSize;

            var image = cloudVisualizer.GetCloud(new [] {testWord});
            image.Size.Should().Be(configuration.ImageSize);
        }

        [Test]
        public void GetCloud_WithoutWords_DoesNotCrash()
        {
            Action action = () => cloudVisualizer.GetCloud(new Word[0]);

            action.Should().NotThrow();
        }

        [Test]
        public void GetCloud_WhenWordHaveZeroFrequency_DoesNotCrash()
        {
            var zeroFrequencyWord = new Word("ADs", SpeechPart.Noun)
            {
                Frequency = 0
            };
            Action action = () => cloudVisualizer.GetCloud(new[] {zeroFrequencyWord});
            
            action.Should().NotThrow();
        }

        [Test]
        public void GetCloud_WhenZeroImageSize_Throws()
        {
            container.Get<CloudViewConfiguration>().ImageSize = Size.Empty;

            Action action = () => cloudVisualizer.GetCloud(new Word[0]);

            action.Should().Throw<ArgumentException>();
        }

        [TearDown]
        public void TearDown()
        {
            container.Get<CloudViewConfiguration>().ImageSize = tempSize;
        }
    }
}