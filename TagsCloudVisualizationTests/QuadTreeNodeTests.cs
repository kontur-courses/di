using System;
using System.Drawing;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using TagsCloudVisualization.Structures;

namespace TagsCloudVisualizationTests
{
    [TestFixture]
    public class QuadTreeNodeTests
    {
        protected QuadTreeNode QuadTreeNode;
        protected Random Random;
        private readonly Size nodeSize = new(100, 100);

        [SetUp]
        public void SetUp()
        {
            QuadTreeNode = new QuadTreeNode(new Rectangle(new Point(0, 0), nodeSize));
            Random = new Random();
        }

        [Test]
        public void Insert_ShouldThrowArgumentOutOfRangeException_WhenRectangleOutOfBounds()
        {
            Action action = () => { QuadTreeNode.Insert(new Rectangle(new Point(1000, 1000), new Size(10, 10))); };
            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Test]
        public void IntersectsWith_ShouldTrue_WhenRectanglesIntersects()
        {
            QuadTreeNode.Insert(new Rectangle(new Point(0, 0), new Size(10, 10)));
            var result = QuadTreeNode.IntersectsWith(
                new Rectangle(new Point(5, 5), new Size(20, 20)));
            result.Should().BeTrue();
        }


        [Test]
        public void IntersectsWith_ShouldFalse_WhenRectanglesNotIntersects()
        {
            QuadTreeNode.Insert(new Rectangle(new Point(0, 0), new Size(10, 10)));
            var result = QuadTreeNode.IntersectsWith(
                new Rectangle(new Point(50, 50), new Size(10, 10)));
            result.Should().BeFalse();
        }

        [Test]
        public void Count_ShouldReturnCountOfRectangles_AfterInsertionsOnDifferentTreeDepths()
        {
            const int iterationsCount = (int)1e5;
            for (var i = 0; i < iterationsCount; i++)
            {
                //сложно считать потомка в котором будет прямоугольник, поэтому рандом
                //1e5 итераций дают детерминированность теста на практике
                var point = new Point(Random.Next(0, nodeSize.Width - 20), Random.Next(0, nodeSize.Height - 20));
                var size = new Size(Random.Next(5, 20), Random.Next(5, 20));
                var rectangle = new Rectangle(point, size);
                QuadTreeNode.Insert(rectangle);
            }
            var result = QuadTreeNode.Count;
            result.Should().Be(iterationsCount);
        }
    }
}