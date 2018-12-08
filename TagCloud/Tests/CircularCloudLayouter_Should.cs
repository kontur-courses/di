using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using MoreLinq;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    internal class CircularCloudLayouter_Should : LayouterTestsBase
    {
        private CircularCloudLayouter layouter;
        private Point center;
        
        [SetUp]
        public void SetUp()
        {
            center = new Point(120,150);
            layouter = new CircularCloudLayouter(center);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var bitmap = new Bitmap(1000,1000);
                using (var g = Graphics.FromImage(bitmap))
                    foreach (var rectangle in layouter.Layout)
                        g.DrawFramedRectangle(rectangle);
                var path = $"{TestContext.CurrentContext.Test.FullName}_visualization";
                bitmap.Save($"./{path}.png",ImageFormat.Png);
                TestContext.WriteLine($"Tag cloud visualization saved to file {path}");
            }
        } 
        
        [Test]
        public void HaveCorrectCenter()
        {
            layouter.Center.Should().BeEquivalentTo(center);
        }

        [Test]
        public void MakeRectangleWithCorrectSize()
        {
            var size = RandomSize();
            layouter.PutNextRectangle(size).Size.Should().BeEquivalentTo(size);
        }

        [Test]
        public void PutFirstRectangleCenterOnLayoutCenter()
        {
            var rect = layouter.PutNextRectangle(RandomSize());
            rect.Center().Should().BeEquivalentTo(center);
        }

        [Test]
        public void MakeManyCorrectRectangles()
        {
            const int count = 100;
            var sizes = count.Times(RandomSize).ToArray();
            sizes.Select(layouter.PutNextRectangle).Select(x => x.Size).Should().BeEquivalentTo(sizes);
        }

        [Test]  
        public void DoNotOverlapManyRectangles()
        {
            var rectangles = GenerateRectangles(Hundred);

            for (int i = 0; i < Hundred; i++) 
            for (int j = 0; j < i; j++)
                Assert.False(rectangles[i].IntersectsWith(rectangles[j]),
                    $"{i}th rectangle was {rectangles[i]}, and {j}th rectangle was{rectangles[j]}");
        }
        
        [Test]
        public void HaveCorrectSizes()=>
            Hundred.Times(() =>
            {
                var size = RandomSize();
                layouter.PutNextRectangle(size).Size.Should().Be(size);
            });

        [Test]
        public void HaveSameLayoutAsReturnedRectangles()
        {
            GenerateRectangles(Hundred).Should().BeEquivalentTo(layouter.Layout);
        }
        
        [Test]
        public void FitManySameSizesInto2TimesBiggerCircle()
        {
            var size = new Size(24,120);
            var space = size.Area() * Thousand;
            var radius = Math.Sqrt(2 * space / Math.PI);
            
            var rects = Thousand.Times(()=>layouter.PutNextRectangle(size));
            
            rects.SelectMany(x => x.Points())
                .Select(x => x.DistanceTo(center))
                .All(x => x < radius)
                .Should().BeTrue();
        }
        
        
        [Test]
        public void FitManyRandomSizesInto5TimesBiggerCircle()
        {
            var sizes = Thousand.Times(RandomSize).ToArray();
            var space = sizes.Aggregate(0, (sum, size) => sum + size.Area());
            var radius = Math.Sqrt(5 * space / Math.PI);
           
            var rects = sizes.Select(layouter.PutNextRectangle);
            
            rects.SelectMany(x => x.Points())
                .Select(x => x.DistanceTo(center))
                .All(x => x < radius)
                .Should().BeTrue();
        }

        [Test]
        public void Have20PercentDisperseOctagonalBounds()
        {
            const double dispersion = 0.2;
            
            GenerateRectangles(Thousand);
            var centerSize = new Size(center);
            var vectors = layouter.Layout
                    .SelectMany(x => x.Points())
                    .Select(x => x - centerSize)
                    .ToArray();
            var edgesDist = OctogonEdgesDist(vectors);

            var meanRadius = edgesDist.Sum() / edgesDist.Length; 
            edgesDist.All(x => Math.Abs(x - meanRadius) / meanRadius < dispersion).Should().BeTrue();
        }

        private double[] OctogonEdgesDist(IEnumerable<Point> vectors) => new[]
        {
            vectors.Max(x => x.X),
            vectors.Max(x => -x.X),
            vectors.Max(x => x.Y),
            vectors.Max(x => -x.Y),
            vectors.MaxBy(x => x.X + x.Y).DistanceTo(Point.Empty),
            vectors.MaxBy(x => x.X + x.Y).DistanceTo(Point.Empty),
            vectors.MaxBy(x => x.X + x.Y).DistanceTo(Point.Empty),
            vectors.MaxBy(x => x.X + x.Y).DistanceTo(Point.Empty)
        };
        
        private Rectangle[] GenerateRectangles(int count)=>
            count.Times(RandomSize).Select(layouter.PutNextRectangle).ToArray();

    }
}
