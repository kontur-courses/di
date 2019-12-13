using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudContainer.Parsing;
using TagsCloudContainer.RectangleTranslation;
using TagsCloudContainer.Settings_Providing;

namespace TagsCloudContainer.Visualization.Tests
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        private CircularCloudVisualizer visualizer;

        private CircularCloudLayouter layouter;

        private static string TestDirectoryPath => Path.Combine(Directory.GetCurrentDirectory(), @"\visualization\");

        [SetUp]
        public void Initialize()
        {
            var settingsProvider = new SettingsProvider(new TxtParser());
            visualizer = new CircularCloudVisualizer(settingsProvider.GetSettings().ColoringOptions, new PngSaver(),
                new Size(1920, 1080), "Consolas");
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Failure)
            {
                return;
            }

            var fullPath = Path.Combine(TestDirectoryPath, TestContext.CurrentContext.Test.FullName + ".png");
            var image = visualizer.GetVisualization(layouter.GetWordsRectangles());
            image.Save(fullPath, ImageFormat.Png);
            TestContext.WriteLine($"Tag cloud visualization saved to file {fullPath}");
        }


        [TestCase(10, 1, 1, TestName = "onIncreasingRectangles")]
        [TestCase(10, 20, -1, TestName = "onDecreasingRectangles")]
        [TestCase(10, 30, 0, TestName = "onZeroCenter")]
        [TestCase(10, 20, 0, TestName = "onSameRectangles")]
        public void PutRectanglesCorrectly(int count, int startFontSize,
            double step)
        {
            layouter = new CircularCloudLayouter();
            var sizedWords = CreateSizedWordsList(count, startFontSize, step);
            layouter.LayoutWords(sizedWords);

            layouter.GetWordsRectangles().Count.Should().Be(sizedWords.Count);
            ContainsIntersectedRectangles(layouter).Should().Be(false);
        }

        [Timeout(1000)]
        [TestCase(200, TestName = "on 200 rectangles")]
        [TestCase(500, TestName = "on 500 rectangles")]
        [TestCase(1000, TestName = "on 1000 rectangles")]
        public void HaveCorrectTime_OnManyRectangles(int count)
        {
            layouter = new CircularCloudLayouter();
            var wordsToAdd = new List<SizedWord>();
            for (var i = 0; i < count; i++)
            {
                wordsToAdd.Add(new SizedWord(i.ToString(), 10));
            }

            layouter.LayoutWords(wordsToAdd);
            layouter.GetWordsRectangles().Count.Should().Be(count);
        }

        private static List<SizedWord> CreateSizedWordsList(int count, int startFontSize, double step)
        {
            var sizedWords = new List<SizedWord>();
            var fontSize = startFontSize;
            for (var i = 0; i < count; i++)
            {
                fontSize = (int) Math.Round(fontSize + step);
                sizedWords.Add(new SizedWord("word" + i, fontSize));
            }

            return sizedWords;
        }

        private static bool ContainsIntersectedRectangles(CircularCloudLayouter layouter)
        {
            var rectangles = layouter.GetWordsRectangles();
            foreach (var firstRectangle in rectangles)
            {
                foreach (var secondRectangle in rectangles)
                {
                    if (firstRectangle != secondRectangle &&
                        firstRectangle.Rectangle.IntersectsWith(secondRectangle.Rectangle))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}