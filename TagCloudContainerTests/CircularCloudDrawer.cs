using System;
using System.Collections.Generic;
using System.Drawing;
using CommandLine;
using FluentAssertions;
using TagsCloudContainer;
using NUnit.Framework;
using TagsCloudContainer.FileSavers;
using TagsCloudContainer.LayouterAlgorithms;
using TagsCloudContainer.UI;

namespace TagCloudContainerTests
{
    public class CircularCloudDrawer
    {
        private IUi settings;
        private Color[] colors;
        private Dictionary<string, int> frequencyDictionary;
        private IFileSaver fileSaver;
        private int coefficient;
        private ICloudLayouterAlgorithm layouter;
        [SetUp]
        public void SetUp()
        {
            settings = Parser.Default.ParseArguments<ConsoleUi>(new string[] { }).Value;
            colors = new[] {Color.Black, Color.Black};
            frequencyDictionary = new Dictionary<string, int> {{"happy", 1}, {"unhappy", 1}};
            fileSaver = new PngSaver();
            coefficient = 6;
            layouter = new CircularCloudLayouter(new Spiral(settings));
        }
        [Test]
        public void CircularCloudDrawer_ShouldThrowArgumentException_OnIncorrectBrushColor()
        {
            settings.BackGroundColor = "Invalid color";
            Action act = () =>TagsCloudContainer.CircularCloudDrawer.DrawWords(colors, frequencyDictionary, fileSaver, settings, coefficient, layouter);
            act.Should().Throw<ArgumentException>().WithMessage("Unknown background color");
        }
        [Test]
        public void CircularCloudDrawer_ShouldThrowArgumentException_OnIncorrectFontName()
        {
            settings.FontName = "Invalid font name";
            Action act = () =>TagsCloudContainer.CircularCloudDrawer.DrawWords(colors, frequencyDictionary, fileSaver, settings, coefficient, layouter);
            act.Should().Throw<ArgumentException>().WithMessage("Unknown font name");
        }
        
        [Test]
        public void CircularCloudDrawer_ShouldNotThrowArgumentException_OnCorrectData()
        {
            Action act = () =>TagsCloudContainer.CircularCloudDrawer.DrawWords(colors, frequencyDictionary, fileSaver, settings, coefficient, layouter);
            act.Should().NotThrow<Exception>();
        }
        
    }
}