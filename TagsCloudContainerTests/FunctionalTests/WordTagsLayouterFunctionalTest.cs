using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using RectanglesCloudLayouter.Core;
using TagsCloudContainer.Settings;
using TagsCloudContainer.TextProcessing;

namespace TagsCloudContainerTests.FunctionalTests
{
    public class WordTagsLayouterFunctionalTest
    {
        private WordTagsLayouter _sut;
        private static Font _font = new Font("arial", 5);

        [SetUp]
        public void SetUp()
        {
            var frequency = new WordsFrequency(new WordsFilter(
                new SpeechPartsParser(
                    new MyStemConverter(Path.GetFullPath("mystem.exe").Replace($"Tests", ""), "-ni")),
                new TextProcessingSettings(new[] {"велосипед", "осень"})));
            var cloudLayouter = new CloudLayouter(new ArchimedeanSpiral(new Point(0, 0)), new CloudRadiusCalculator());
            var wordMeasurer = new WordMeasurer();

            _sut = new WordTagsLayouter(frequency, cloudLayouter, wordMeasurer, _font);
        }

        [Test]
        public void GetWordTags_Tags_WhenManyWords()
        {
            var text = "велосипед выход / осень \r\n.. он под";
            var expectedWord = text.Split()[1];
            var expectedFont = new Font(_font.FontFamily, _font.Size + (float) Math.Log2(1) * 7);
            var sizeF = Graphics.FromHwnd(IntPtr.Zero).MeasureString(expectedWord, expectedFont);
            var expectedSize = new Size((int) Math.Ceiling(sizeF.Width), (int) Math.Ceiling(sizeF.Height));
            var expectedResult = new List<WordTag>
                {new WordTag(expectedWord, new Rectangle(Point.Empty - expectedSize / 2, expectedSize), expectedFont)};

            var act = _sut.GetWordTags(text);

            act.Should().BeEquivalentTo(expectedResult);
        }
    }
}