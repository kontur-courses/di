using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Rendering;

namespace TagsCloudContainer.Tests
{
    public class TagsCloudRendererTests
    {
        private IEnumerable<WordStyle> words;
        private const int width = 100;
        private const int height = 100;
        private const string filename = "testImg.png";

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
        public void Render_WithScale_ScaleImage(float scale)
        {
            var config = new RenderConfig(filename, ImageFormat.Png) {Scale = scale};
            using var output = GetOutputBitmap(config, words);
            output.Size
                .Should().BeEquivalentTo(new Size((int)(width * scale), (int)(height * scale)));
        }

        [TestCase(200, 200)]
        [TestCase(2000, 2000)]
        public void Render_WithDesiredImageSize_ScaleImage(int desiredWidth, int desiredHeight)
        {
            var config = new RenderConfig(filename, ImageFormat.Png)
                {DesiredImageSize = new Size(desiredWidth, desiredHeight)};

            using var output = GetOutputBitmap(config, words);
            output.Size
                .Should().BeEquivalentTo(new Size(desiredWidth, desiredHeight));
        }

        [Test]
        public void Render_OutputBitmapWithBackground_NotEmpty()
        {
            var config = new RenderConfig(filename, ImageFormat.Png) {Background = new SolidBrush(Color.Red)};
            using var output = GetOutputBitmap(config, Enumerable.Empty<WordStyle>());
            for (var x = 0; x < output.Width; x++)
            {
                for (var y = 0; y < output.Height; y++)
                {
                    var pixel = output.GetPixel(x, y);
                    pixel.R.Should().Be(255);
                    pixel.A.Should().BePositive();
                }
            }
        }

        private Bitmap GetOutputBitmap(RenderConfig config, IEnumerable<WordStyle> wordsToRender)
        {
            var renderer = new TabsCloudRenderer(config);
            renderer.Render(wordsToRender, new Size(width, height));
            return new Bitmap(filename);
        }
    }
}