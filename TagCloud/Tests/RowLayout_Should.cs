using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization.Tests
{
    public class RowLayout_Should : LayouterTestsBase
    {
        private RowLayout layout;
        
        [SetUp]
        public void SetUp()
        {
            var center = new Point(120,150);
            layout = new RowLayout(new Rectangle(center, RandomSize()));
        }

        private Rectangle[] GenerateRectangles(int count) =>
            count.Times(RandomSize).Where(x => x.Height <= layout.Bounds.Height).Select(layout.Add).ToArray();
        
        [Test]
        public void FitRectanglesIntoItsBounds()
        {
            GenerateRectangles(Hundred);
            foreach (var rect in layout.Body)
                Assert.IsTrue(layout.Bounds.Contains(rect),
                    $"{rect}, does not fit into {layout.Bounds}");
        }
        
        [Test]
        public void NotIntersectRectangles()=>
            GenerateRectangles(Hundred).ForAllPairs(AssertDontIntersect);

        [Test]
        public void HaveBoundsWidthEqualReactanglesSum()
        {
            GenerateRectangles(Hundred);
            layout.Body.Sum(x => x.Width).Should().Be(layout.Bounds.Width);            
        }

        [Test]
        public void ThrowArgumentExceptionWhenHeightIsBiggerThanInitial()=>
            Assert.Throws<ArgumentException>(() => layout.Add(new Size(1, layout.Bounds.Height+1)));

        [Test]
        public void DontThrowOtherwise() =>
            Assert.DoesNotThrow(() => layout.Add(new Size(1, layout.Bounds.Height)));

    }
}