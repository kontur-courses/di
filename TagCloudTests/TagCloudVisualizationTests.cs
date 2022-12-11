﻿using System.Collections.Generic;
using System.Drawing;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.CloudLayouters;
using TagCloud.PointGenerators;
using TagCloud.TagCloudCreators;
using TagCloud.TagCloudVisualizations;

namespace TagCloudTests
{
    public class TagCloudVisualizationTests
    {
        [Test]
        public void SaveAsBitmap_TagCloudInFile_Success()
        {
            var center = new Point(50, -50);
            var tempBmpFile = "temp.bmp";

            File.Delete(tempBmpFile);

            File.Exists(tempBmpFile).Should().BeFalse($"file {tempBmpFile} must be deleted");

            var layoutSizes = new List<Size>();
            for (int i = 400; i > 1; i -= 2)
                layoutSizes.Add(new Size(i, i / 2));
            var circularCloudLayouter = new CircularCloudLayouter(() => new SpiralPointGenerator(center));
            var tagCloudCreator = new LayoutTagCloudCreator(circularCloudLayouter, layoutSizes);
            var settings = new TagCloudVisualizationSettings();
            var visualization = new TagCloudBitmapVisualization(tagCloudCreator);
            visualization.Save(tempBmpFile, settings);

            File.Exists(tempBmpFile).Should().BeTrue($"file {tempBmpFile} must be generated");
        }
    }
}