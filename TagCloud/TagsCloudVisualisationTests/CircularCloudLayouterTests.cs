using System.Drawing;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualisation.Extensions;
using TagsCloudVisualisation.Layouter;
using TagsCloudVisualisation.Visualisation;
using TagsCloudVisualisationTests.Infrastructure;

namespace TagsCloudVisualisationTests
{
    [SaveLayouterResults(TestStatus.Failed, TestStatus.Inconclusive, TestStatus.Passed, TestStatus.Warning)]
    public class CircularCloudLayouterTests : LayouterTestBase
    {
        public override void SetUp()
        {
            base.SetUp();
            Layouter = new CircularCloudLayouter(new Point(0, 0), new Size(3, 3));
        }

        [Test]
        public void PutNextRectangleShould_FirstRectangle_PutAtCenter()
        {
            var size = new Size(10, 10);
            Layouter.PutAndTest(size, new Point(-5, -5));
        }

        [Test]
        public void PutNextRectangleShould_SecondRectangle_PutOnTopOfFirst()
        {
            Layouter.Put(new Size(10, 10), out _)
                .PutAndTest(new Size(7, 5), new Point(-5, -11));
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