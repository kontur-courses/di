using FakeItEasy;
using FluentAssertions;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Infrastructure.Settings;
using TagsCloudContainer.Infrastructure.WordColorProviders;
using TagsCloudContainer.Infrastructure.WordColorProviders.Factories;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    internal class WordPlateVisualizerTests
    {
        private WordPlateVisualizer plateVisualizer;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var colorProviderFake = new Fake<IWordColorProvider>();
            colorProviderFake.CallsTo(fake => fake.GetColor(string.Empty)).WithAnyArguments().Returns(Result.Ok(Color.Red));

            var colorProviderFactoryFake = new Fake<IWordColorProviderFactory>();
            colorProviderFactoryFake.CallsTo(fake => fake.CreateDefault(null)).WithAnyArguments().Returns(colorProviderFake.FakedObject);

            plateVisualizer = new WordPlateVisualizer(colorProviderFactoryFake.FakedObject);
        }
        
        [TestCase(0, 100, TestName = "{m}_ReturnFailedResult_WhenBitmapWidthIsZero")]
        [TestCase(100, 0, TestName = "{m}_ReturnFailedResult_WhenBitmapHeightIsZero")]
        [TestCase(10, 10, TestName = "{m}_ReturnFailedResult_WhenBitmapSizeIsLessThanTagsCloud")]
        public void DrawPlates_Should(int width, int height)
        {
            var size = new Size(width, height);
            var plates = new WordPlate[]
            {
                new WordPlate() 
                { 
                    Font = new Font("Consolas", 14F),
                    WordRectangle = new WordRectangle() 
                    {
                        Word = "qwerty",
                        Rectangle = new RectangleF(default, new SizeF(50, 50))
                    }
                }
            };

            var result = plateVisualizer.DrawPlates(plates, size, null);

            result.IsFailed.Should().BeTrue();
        }
    }
}