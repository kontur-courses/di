using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class VisualizerTests
    {
        [Test]
        public void GetCloudVisualization_Throws_WhenWordsIsNull()
        {
            Action act = () => Visualizer.GetCloudVisualization(null, new List<Color>() {Color.Aqua}, Color.Aqua,
                new Size(50, 50), new Size(100, 50),
                new CircularCloudLayouter(new SpiralPointsGenerator(new Point(500, 500))), 0.8, 1,
                FontFamily.GenericSansSerif, Brushes.Aqua);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetCloudVisualization_Throws_WhenTagsColorsIsNull()
        {
            Action act = () => Visualizer.GetCloudVisualization(new List<string>(), null, Color.Aqua, new Size(50, 50),
                new Size(100, 50), new CircularCloudLayouter(new SpiralPointsGenerator(new Point(500, 500))), 0.8, 1,
                FontFamily.GenericSansSerif, Brushes.Aqua);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetCloudVisualization_Throws_WhenMinHeightIsGreaterThanMaxHeight()
        {
            Action act = () => Visualizer.GetCloudVisualization(new List<string>(), new List<Color>() {Color.Aqua},
                Color.Aqua, new Size(50, 500), new Size(100, 50),
                new CircularCloudLayouter(new SpiralPointsGenerator(new Point(500, 500))), 0.8, 1,
                FontFamily.GenericSansSerif, Brushes.Aqua);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetCloudVisualization_Throws_WhenMinWidthIsGreaterThanMaxWidth()
        {
            Action act = () => Visualizer.GetCloudVisualization(new List<string>(), new List<Color>() {Color.Aqua},
                Color.Aqua, new Size(500, 50), new Size(100, 100),
                new CircularCloudLayouter(new SpiralPointsGenerator(new Point(500, 500))), 0.8, 1,
                FontFamily.GenericSansSerif, Brushes.Aqua);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetCloudVisualization_Throws_WhenLayouterIsNull()
        {
            Action act = () => Visualizer.GetCloudVisualization(new List<string>(), new List<Color>() {Color.Aqua},
                Color.Aqua, new Size(50, 50), new Size(100, 100), null, 0.8, 1, FontFamily.GenericSansSerif,
                Brushes.Aqua);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetCloudVisualization_Throws_WhenCoefficientIsNotPositive()
        {
            Action act = () => Visualizer.GetCloudVisualization(new List<string>(), new List<Color>() {Color.Aqua},
                Color.Aqua, new Size(50, 50), new Size(100, 100),
                new CircularCloudLayouter(new SpiralPointsGenerator(new Point(500, 500))), 0, 1,
                FontFamily.GenericSansSerif, Brushes.Aqua);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetCloudVisualization_Throws_WhenCoefficientIsGreaterOrEqualOne()
        {
            Action act = () => Visualizer.GetCloudVisualization(new List<string>(), new List<Color>() {Color.Aqua},
                Color.Aqua, new Size(50, 50), new Size(100, 100),
                new CircularCloudLayouter(new SpiralPointsGenerator(new Point(500, 500))), 1, 1,
                FontFamily.GenericSansSerif, Brushes.Aqua);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetCloudVisualization_Throws_WhenMinFontSizeIsNotPositive()
        {
            Action act = () => Visualizer.GetCloudVisualization(new List<string>(), new List<Color>() {Color.Aqua},
                Color.Aqua, new Size(50, 50), new Size(100, 100),
                new CircularCloudLayouter(new SpiralPointsGenerator(new Point(500, 500))), 0.5, -1,
                FontFamily.GenericSansSerif, Brushes.Aqua);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetCloudVisualization_Throws_WhenFontFamilyIsNull()
        {
            Action act = () => Visualizer.GetCloudVisualization(new List<string>(), new List<Color>() {Color.Aqua},
                Color.Aqua, new Size(50, 50), new Size(100, 100),
                new CircularCloudLayouter(new SpiralPointsGenerator(new Point(500, 500))), 0.5, 1, null, Brushes.Aqua);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetCloudVisualization_Throws_WhenBrushIsNull()
        {
            Action act = () => Visualizer.GetCloudVisualization(new List<string>(), new List<Color>() {Color.Aqua},
                Color.Aqua, new Size(50, 50), new Size(100, 100),
                new CircularCloudLayouter(new SpiralPointsGenerator(new Point(500, 500))), 0.5, 1,
                FontFamily.GenericSansSerif, null);

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GetCloudVisualization_Throws_WhenWordIsTooLong()
        {
            Action act = () => Visualizer.GetCloudVisualization(
                new List<string>() {"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"}, new List<Color>() {Color.Aqua},
                Color.Aqua, new Size(50, 50), new Size(100, 100),
                new CircularCloudLayouter(new SpiralPointsGenerator(new Point(500, 500))), 0.8, 5,
                FontFamily.GenericSansSerif, Brushes.Aqua);

            act.Should().Throw<ArgumentException>().WithMessage("word is too long for tag cloud");
        }
    }
}