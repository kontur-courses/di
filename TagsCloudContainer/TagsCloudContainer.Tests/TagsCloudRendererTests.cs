﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Rendering;
using TagsCloudContainer.Settings;
using TagsCloudContainer.Settings.Interfaces;
using TagsCloudVisualizationTests.TestingLibrary;

namespace TagsCloudContainer.Tests
{
    public class TagsCloudRendererTests
    {
        private IEnumerable<WordStyle> words;
        private const int width = 100;
        private const int height = 100;

        [SetUp]
        public void SetUp()
        {
            var font = new Font(FontFamily.GenericMonospace, 10);
            var brush = new SolidBrush(Color.Blue);
            words = Enumerable.Range(1, 10)
                .Select(i => new WordStyle(new string('a', i), font, new Point(i, i), brush));
        }

        [TestCase(0.5f)]
        [TestCase(2)]
        public void GetBitmap_WithScale_ScaleImage(float scale)
        {
            var config = new RenderingSettings {Scale = scale};
            using var output = GetOutputBitmap(config, words);
            output.Size
                .Should().BeEquivalentTo(new Size((int)(width * scale), (int)(height * scale)));
        }

        [TestCase(200, 200)]
        [TestCase(2000, 2000)]
        public void GetBitmap_WithDesiredImageSize_ScaleImage(int desiredWidth, int desiredHeight)
        {
            var config = new RenderingSettings {DesiredImageSize = new Size(desiredWidth, desiredHeight)};
            using var output = GetOutputBitmap(config, words);
            output.Size
                .Should().BeEquivalentTo(new Size(desiredWidth, desiredHeight));
        }

        [Test]
        public void GetBitmap_OutputBitmapWithBackground_NotEmpty()
        {
            var config = new RenderingSettings {Background = new SolidBrush(Color.Red)};
            using var output = GetOutputBitmap(config, Enumerable.Empty<WordStyle>());
            foreach (var color in output.ToEnumerable())
            {
                color.R.Should().Be(255);
                color.A.Should().BePositive();
            }
        }

        private static Bitmap GetOutputBitmap(IRenderingSettings settings, IEnumerable<WordStyle> wordsToRender)
        {
            var renderer = new TagsCloudRenderer(settings);
            using var bitmap = renderer.GetBitmap(wordsToRender, new Size(width, height));
            return new Bitmap(bitmap);
        }
    }
}