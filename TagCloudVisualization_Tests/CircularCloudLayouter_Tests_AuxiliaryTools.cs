﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Tests
{
    public static class CircularCloudLayouter_Tests_AuxiliaryTools
    {
        public static double GetCloudRadius(IEnumerable<Rectangle> rectangles)
        {
            var cloudWidth = GetCloudWidth(rectangles);
            var cloudHeight = GetCloudHeight(rectangles);

            return Math.Min(cloudWidth / 2, cloudHeight / 2);
        }

        public static double GetCloudWidth(IEnumerable<Rectangle> rectangles)
        {
            var leftBorder = rectangles.Min(value => value.Left);
            var rightBorder = rectangles.Max(value => value.Right);

            return rightBorder - leftBorder;
        }

        public static double GetCloudHeight(IEnumerable<Rectangle> rectangles)
        {
            var topBorder = rectangles.Min(value => value.Top);
            var bottomBorder = rectangles.Max(value => value.Bottom);

            return bottomBorder - topBorder;
        }
        
        public static List<Rectangle> GetCloudWithEqualRectangles(int rectanglesCount, int width = 15, int height = 15)
        {
            var cloud = new CircularCloudLayouter(new Point(500, 500));
            var rectangles = new List<Rectangle>();
            for (var i = 0; i < rectanglesCount; i++)
            {
                rectangles.Add(cloud.PutNextRectangle(new Size(width, height)));
            }

            return rectangles;
        }

        public static List<Rectangle> GetCloudWithDifferentRectangles(int rectanglesCount, int minWidth, int maxWidth,
            int minHeight, int maxHeight)
        {
            var cloud = new CircularCloudLayouter(new Point(500, 500));
            var rectangles = new List<Rectangle>();
            for (var i = 0; i < rectanglesCount; i++)
            {
                rectangles.Add(cloud.PutNextRectangle(GetRandomSize(minWidth, maxWidth, minHeight, maxHeight)));
            }

            return rectangles;
        }

        private static Size GetRandomSize(int minWidth, int maxWidth, int minHeight, int maxHeight)
        {
            var random = new Random();
            var width = random.Next(minWidth, maxWidth);
            var height = random.Next(minHeight, maxHeight);
            return new Size(width, height);
        }
    }
}