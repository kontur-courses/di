using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.ApplicationRunning;
using TagsCloudContainer.ApplicationRunning.Commands;
using TagsCloudContainer.CloudLayouters;
using TagsCloudContainer.CloudVisualizers;
using TagsCloudContainer.CloudVisualizers.ImageSaving;
using TagsCloudContainer.TextParsing.CloudParsing;

namespace TagsCloudContainer.Tests.Commands
{
    [TestFixture]
    public class GenerateCloudCommand_Test
    {
        private GenerateCloudCommand command;

        [SetUp]
        public void SetUp()
        {
            var settings = new SettingsManager();
            var parser = new CloudWordsParser(() => settings.GetWordsParserSettings());
            var layouter = new CloudLayouter(() => settings.GetLayouterSettings());
            var visualizer = new CloudVisualizer(() => settings.GetVisualizerSettings());
            var saver = new ImageSaver(() => settings.GetImageSaverSettings());
            var cloud = new TagsCloud(parser, layouter, visualizer, saver);
            command = new GenerateCloudCommand(cloud, settings);
        }

        [Test]
        public void Act_Should_ThrowArgumentException_When_WrongArgumentsCount()
        {
            Following.Code(() => command.Act(new[] {"circle", "100", "0,1"})).ShouldThrow<ArgumentException>();
        }

        [TestCase("0,01", TestName = "when too small")]
        [TestCase("0", TestName = "when zero")]
        [TestCase("-0,1", TestName = "when negative")]
        [TestCase("ab", TestName = "when not a number")]
        public void Act_Should_ThrowArgumentException_When_IncorrectStepValue(string step)
        {
            Following.Code(() => command.Act(new[] {"circle", "100", step, "1"})).ShouldThrow<ArgumentException>();
        }

        [TestCase("0.1", TestName = "when too small")]
        [TestCase("-1", TestName = "when negative")]
        [TestCase("0", TestName = "when zero")]
        [TestCase("ab", TestName = "when not a number")]
        public void Act_Should_ThrowArgumentException_When_IncorrectSizeValue(string size)
        {
            Following.Code(() => command.Act(new[] {"circle", size, "0,1", "1"})).ShouldThrow<ArgumentException>();
        }

        [TestCase("0.1", TestName = "when too small")]
        [TestCase("-1", TestName = "when negative")]
        [TestCase("0", TestName = "when zero")]
        [TestCase("ab", TestName = "when not a number")]
        [TestCase("3", TestName = "when out of range")]
        public void Act_Should_ThrowArgumentException_When_IncorrectBroadnessValue(string broadness)
        {
            Following.Code(() => command.Act(new[] {"circle", "100", "0,1", broadness}))
                .ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Act_Should_ThrowArgumentException_When_IncorrectAlgorithm()
        {
            Following.Code(() => command.Act(new[] {"notACorrectAlgorithm", "100", "0,1", "1"}))
                .ShouldThrow<ArgumentException>();
        }
    }
}