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
    public class ParseCommand_Test
    {
        private ParseCommand command;

        [SetUp]
        public void SetUp()
        {
            var settings = new SettingsManager();
            var parser = new CloudWordsParser(() => settings.GetWordsParserSettings());
            var layouter = new CloudLayouter(() => settings.GetLayouterSettings());
            var visualizer = new CloudVisualizer(() => settings.GetVisualizerSettings());
            var saver = new ImageSaver(() => settings.GetImageSaverSettings());
            var cloud = new TagsCloud(parser, layouter, visualizer, saver);
            command = new ParseCommand(cloud, settings);
        }

        [Test]
        public void Act_Should_ThrowArgumentException_When_FileDoesntExist()
        {
            Following.Code(() => command.Act(new[] {"idontexist"})).ShouldThrow<ArgumentException>();
        }
    }
}