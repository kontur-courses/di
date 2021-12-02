using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using TagsCloudContainer.Layout;
using TagsCloudContainer.Preprocessing;
using TagsCloudContainer.Rendering;
using TagsCloudContainer.WordsLoading;
using TagsCloudVisualization;

namespace TagsCloudContainer.Tests
{
    public class StaticWordsProvider : IWordsProvider
    {
        public IEnumerable<string> GetWords() => Words.AboutSpace;
    }

    public class TagsCloudDirectorTests
    {
        private TagsCloudDirector director;
        private const string filename = "test.png";
        private Size imageSize;

        [SetUp]
        public void SetUp()
        {
            imageSize = new Size(500, 500);

            var services = new ServiceCollection();
            services
                // WordsLoading
                .AddTransient<IWordsProvider, StaticWordsProvider>()
                // Preprocessing
                .AddTransient<IWordSpeechPartParser, WordSpeechPartParser>()
                .AddTransient(s => new IWordsFilter[]
                {
                    new SpeechPartWordsFilter(s.GetRequiredService<IWordSpeechPartParser>(),
                        new HashSet<SpeechPart> {SpeechPart.INTJ, SpeechPart.PART, SpeechPart.PR, SpeechPart.CONJ})
                })
                // Layout
                .AddTransient<ITagsCloudLayouter>(s => new FontBasedLayouter(FontFamily.GenericMonospace,
                    s.GetRequiredService<IFontSizeSelector>(), s.GetRequiredService<ICloudLayouter>()))
                .AddTransient<IFontSizeSelector, FrequencyLinearFontSizeSelector>()
                .AddTransient<ICloudLayouter, CircularCloudLayouter>()
                .AddSingleton(_ => new FontSizeRange(32, 12))
                // Rendering
                .AddTransient<IWordColorMapper>(s =>
                    new SpeechPartWordColorMapper(s.GetRequiredService<IWordSpeechPartParser>(),
                        new Dictionary<SpeechPart, Color>
                            {[SpeechPart.A] = Color.Crimson, [SpeechPart.S] = Color.DeepSkyBlue}, Color.Azure))
                .AddTransient<ITagsCloudRenderer, TabsCloudRenderer>()
                .AddTransient(_ => new RenderConfig(filename, ImageFormat.Png)
                {
                    DesiredImageSize = imageSize,
                    Background = new SolidBrush(Color.DarkSlateGray)
                })
                // Director
                .AddTransient<TagsCloudDirector, TagsCloudDirector>();

            var provider = services.BuildServiceProvider();
            director = provider.GetRequiredService<TagsCloudDirector>();
        }

        [Test]
        public void TagsCloudDirector_Render_ImageSizeMatches()
        {
            director.Render();
            using var output = new Bitmap(filename);
            output.Size
                .Should().Be(imageSize);
        }

        [Test]
        public void TagsCloudDirector_Render_ImageNotEmpty()
        {
            director.Render();
            using var output = new Bitmap(filename);
            for (var x = 0; x < output.Width; x++)
            {
                for (var y = 0; y < output.Height; y++)
                {
                    var pixel = output.GetPixel(x, y);
                    pixel.A.Should().BePositive();
                    if (!(pixel.R > 0 || pixel.G > 0 || pixel.B > 0))
                        Assert.Fail("One of RGB should be positive.");
                }
            }
        }
    }
}