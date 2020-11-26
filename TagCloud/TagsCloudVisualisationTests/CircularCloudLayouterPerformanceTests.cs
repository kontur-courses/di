using System.Diagnostics;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using TagsCloudVisualisation.Extensions;
using TagsCloudVisualisation.Layouter;
using TagsCloudVisualisation.Visualisation;
using TagsCloudVisualisationTests.Infrastructure;

namespace TagsCloudVisualisationTests
{
    [SaveLayouterResults(TestStatus.Failed, TestStatus.Inconclusive, TestStatus.Passed, TestStatus.Warning)]
    public class CircularCloudLayouterPerformanceTests : LayouterTestBase
    {
        public override void SetUp()
        {
            base.SetUp();
            Layouter = new CircularCloudLayouter(new Point(0, 0), new Size(3, 3));
        }

        [Test, MaxTime(1000)]
        public void PutNextRectangle_PerformanceTesting_100()
        {
            TestWithRandomSizes(100, 4, 20);
        }

        [Test, MaxTime(11000)]
        public void PutNextRectangle_PerformanceTesting_1000()
        {
            TestWithRandomSizes(1000, 4, 20);
        }

        [Test]
        public void PutNextRectangle_PerformanceTesting_RectanglesPerMinute()
        {
            var random = Randomizer.CreateRandomizer();
            var sw = Stopwatch.StartNew();
            var count = 0;
            var currentRank = 1;
            while (sw.Elapsed.TotalMinutes <= 1)
            {
                count++;
                if (count % currentRank == 0)
                {
                    PrintTestingMessage($"Created {count} rectangles, elapsed {sw.Elapsed}");
                    currentRank *= 10;
                }

                Layouter.PutNextRectangle(new Size(random.Next(4, 20), random.Next(4, 20)));
            }

            PrintTestingMessage($"Created {count} rectangles per minute");
        }

        private void TestWithRandomSizes(int testsCount, int minSize, int maxSize)
        {
            var random = Randomizer.CreateRandomizer();
            var randomSizes = Enumerable.Range(0, testsCount)
                .Select(x => new Size(random.Next(minSize, maxSize), random.Next(minSize, maxSize)))
                .ToArray();

            foreach (var size in randomSizes)
                Layouter.PutNextRectangle(size);
        }

        protected override Image RenderResultImage()
        {
            var visualiser = new RectanglesVisualiser(scale: 3, sourceCenterPoint: Layouter.CloudCenter,
                drawer: (g, r) => RectanglesVisualiser.DrawRectangle(g, new Pen(TestingHelpers.RandomColor, 3), r));

            foreach (var rectangle in ResultRectangles)
                visualiser.Draw(rectangle);

            return visualiser.GetImage().FillBackground(Color.Bisque);
        }
    }
}