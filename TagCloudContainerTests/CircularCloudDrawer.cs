using System;
using System.Collections.Generic;
using System.Drawing;
using CommandLine;
using FluentAssertions;
using TagsCloudContainer;
using NUnit.Framework;
using TagsCloudContainer.FileOpeners;
using TagsCloudContainer.FileReaders;
using TagsCloudContainer.FileSavers;
using TagsCloudContainer.LayouterAlgorithms;
using TagsCloudContainer.UI;
using TagsCloudContainer.WordsColoringAlgorithms;

namespace TagCloudContainerTests
{
    public class CircularCloudDrawer
    {
        private IUi settings;

        [SetUp]
        public void SetUp()
        {
            settings = Parser.Default.ParseArguments<ConsoleUiSettings>(new string[] { }).Value;
        }

        [Test]
        public void CircularCloudDrawer_ShouldThrowArgumentException_OnIncorrectBrushColor()
        {
            settings.BackGroundColor = "Invalid color";
            Action act = () => TagsCloudContainer.CircularCloudDrawer.DrawWords(
                new WordsColoringFactory(() => settings), new FileSaverFactory(() => settings),
                new FileReaderFactory(() => settings), settings, new LayouterFactory(() => settings));
            act.Should().Throw<ArgumentException>().WithMessage("Unknown background color");
        }

        [Test]
        public void CircularCloudDrawer_ShouldThrowArgumentException_OnIncorrectFontName()
        {
            settings.FontName = "Invalid font name";
            Action act = () => TagsCloudContainer.CircularCloudDrawer.DrawWords(
                new WordsColoringFactory(() => settings), new FileSaverFactory(() => settings),
                new FileReaderFactory(() => settings), settings, new LayouterFactory(() => settings));
            act.Should().Throw<ArgumentException>().WithMessage("Unknown font name");
        }

        [Test]
        public void CircularCloudDrawer_ShouldNotThrowArgumentException_OnCorrectData()
        {
            Action act = () => TagsCloudContainer.CircularCloudDrawer.DrawWords(
                new WordsColoringFactory(() => settings), new FileSaverFactory(() => settings),
                new FileReaderFactory(() => settings), settings, new LayouterFactory(() => settings));
            act.Should().NotThrow<Exception>();
        }
    }
}