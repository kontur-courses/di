using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualisation.Layouting;

namespace TagsCloudVisualisationTests.Infrastructure
{
    /// <summary>
    /// Base class for ICircularCloudLayouter test
    /// Provides functionality to save visualisations of failed or broken tests
    /// </summary>
    [TestFixture]
    public abstract class LayouterTestBase
    {
        private TestStatus[] statusesToSaveImage;

        private static string TestOutputDirectory =>
            TestingHelpers.GetOutputDirectory(TestContext.CurrentContext.Test.ClassName);

        private Measurement testTimeMeasurement;

        /// <summary>
        /// Test subject
        /// </summary>
        protected ITagCloudLayouter Layouter
        {
            get => layouterHolder;
            set => layouterHolder.Layouter = value;
        }

        private TagCloudLayouterHolder layouterHolder;

        [OneTimeSetUp]
        public virtual void OneTimeSetUp()
        {
            statusesToSaveImage = GetType().GetCustomAttribute<SaveLayouterResultsAttribute>()?.ValidStatuses ??
                                  new TestStatus[0];

            TestingHelpers.ClearDirectory(TestOutputDirectory);
        }

        [SetUp]
        public virtual void SetUp()
        {
            layouterHolder = new TagCloudLayouterHolder();
            testTimeMeasurement = new Measurement("Test", TestContext.Progress);
        }

        [TearDown]
        public virtual void TearDown()
        {
            testTimeMeasurement.Stop();
            if (statusesToSaveImage.Contains(TestContext.CurrentContext.Result.Outcome.Status))
            {
                using var saveMeasurement = new Measurement("Saving result", TestContext.Progress);
                SaveTestResult();
            }
        }

        private void SaveTestResult()
        {
            if (layouterHolder.Layouter == null)
            {
                PrintTestingMessage($"Test subject {nameof(Layouter)} is null, can't save");
                return;
            }

            if (!layouterHolder.ResultRectangles.Any())
            {
                PrintTestingMessage("Nothing to save, output is empty");
                return;
            }

            var image = RenderResultImage();
            var filePath = Path.Combine(TestOutputDirectory, $"{TestContext.CurrentContext.Test.Name}.png");
            try
            {
                image.Save(filePath);
            }
            catch (Exception e)
            {
                PrintTestingMessage($"Can't save cloud visualisation: {e}");
            }

            PrintTestingMessage($"Tag cloud visualization saved to file <{filePath}>");
        }

        protected abstract Image RenderResultImage();

        protected static void PrintTestingMessage(string message) => TestContext.Progress.WriteLine(message);
        protected ICollection<Rectangle> ResultRectangles => layouterHolder.ResultRectangles;

        private class TagCloudLayouterHolder : ITagCloudLayouter
        {
            private ITagCloudLayouter layouter;
            public List<Rectangle> ResultRectangles = new List<Rectangle>();

            public ITagCloudLayouter Layouter
            {
                get => layouter;
                set
                {
                    layouter = value;
                    ResultRectangles = new List<Rectangle>();
                }
            }

            public Point CloudCenter => layouter.CloudCenter;

            public Rectangle PutNextRectangle(Size rectangleSize)
            {
                var result = layouter.PutNextRectangle(rectangleSize);
                ResultRectangles.Add(result);
                return result;
            }
        }
    }
}