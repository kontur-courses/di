using System;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using TagsCloudVisualization.Extensions;

namespace TagsCloudVisualization.Tests
{
    public class LayouterTestsBase
    {
        private  readonly Random rnd = new Random();
        private const int MinHeight = 6;
        private const int MaxHeight = 50;
        private const int MaxWidthHeightRatio = 10;
        protected const int Hundred = 100;
        protected const int Thousand = 1000;

        
        protected Size RandomSize()
        {
            var height = rnd.Next(MinHeight, MaxHeight);
            var width = rnd.Next(height, height * MaxWidthHeightRatio);
            return new Size(width, height);
        }

        protected static void AssertDontIntersect(Rectangle x, Rectangle y)
        {
            Assert.False(x.IntersectsWith(y),
                $"One rectangle was {x} on height, and other rectangle was{y}");
        }

        
    }
}