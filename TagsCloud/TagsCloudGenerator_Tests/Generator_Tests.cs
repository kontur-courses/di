using System;
using NUnit.Framework;
using FluentAssertions;
using TagsCloudGeneratorExtensions;
using System.Reflection;
using TagsCloudGenerator.Attributes;
using TagsCloudGenerator.WordsLayouters;
using TagsCloudGenerator.WordsParsers;
using TagsCloudGenerator.PointsSearchers;
using TagsCloudGenerator.Savers;
using TagsCloudGenerator.Painters;
using TagsCloudGenerator.WordsFilters;
using TagsCloudGenerator.WordsConverters;
using System.Drawing;
using System.IO;

namespace TagsCloudGenerator_Tests
{
    internal class Generator_Tests
    {
        private TagsCloudGenerator.Generators.TagsCloudGenerator sut;

        private SingletonScopeInstancesContainer container;

        [OneTimeSetUp]
        public void OneTimeSetUp() => container = new SingletonScopeInstancesContainer();

        [SetUp]
        public void SetUp()
        {
            sut = container.Get<TagsCloudGenerator.Generators.TagsCloudGenerator>();

            var settings = container.Get<Settings>();
            settings.Font = "Arial";
            settings.ImageSize = null;

            settings.PainterSettings.BackgroundColor = Color.Black;
            settings.PainterSettings.Colors = new[] { Color.White, Color.Red, Color.Yellow };
        }

        [TearDown]
        public void TearDown() => container.Get<Settings>().Reset();

        [TestCase("DataForImageTxt.txt", "TempImageForTxtData.png", "ExpectedImageForTxtData.png")]
        public void UTF8Lines_ToLower_BoringWords_SpiralPoints_FrequencyLayout_UserColorsPaint_Png(
            string readFrom,
            string writeTo,
            string expectedPath)
        {
            readFrom = Metadata.WorkingDirectory + readFrom;
            writeTo = Metadata.WorkingDirectory + writeTo;
            expectedPath = Metadata.WorkingDirectory + expectedPath;

            var settings = container.Get<Settings>();

            settings.FactorySettings.PainterId = GetFactorialIdForType(typeof(PainterWithUserColors));
            settings.FactorySettings.SaverId = GetFactorialIdForType(typeof(PngSaver));
            settings.FactorySettings.PointsSearcherId = GetFactorialIdForType(typeof(PointsSearcherOnSpiral));
            settings.FactorySettings.WordsParserId = GetFactorialIdForType(typeof(UTF8LinesParser));
            settings.FactorySettings.WordsLayouterId = GetFactorialIdForType(typeof(WordsFrequencyLayouter));
            settings.FactorySettings.WordsFiltersIds = new[] { GetFactorialIdForType(typeof(BoringWordsFilter)) };
            settings.FactorySettings.WordsConvertersIds = new[] { GetFactorialIdForType(typeof(WordsToLowerConverter)) };

            GeneratorTest(readFrom, writeTo, expectedPath);
        }

        [TestCase("DataForImageDocx.docx", "TempImageForDocxData.jpeg", "ExpectedImageForDocxData.jpeg")]
        public void DocxLines_ToLowerAndInitialForm_BoringWordsAndPartsOfSpeech_SpiralPoints_ReverseFrequencyLayout_UserColorPaint_Jpeg(
            string readFrom,
            string writeTo,
            string expectedPath)
        {
            readFrom = Metadata.WorkingDirectory + readFrom;
            writeTo = Metadata.WorkingDirectory + writeTo;
            expectedPath = Metadata.WorkingDirectory + expectedPath;

            var settings = container.Get<Settings>();

            settings.FactorySettings.PainterId = GetFactorialIdForType(typeof(PainterWithUserColors));
            settings.FactorySettings.SaverId = GetFactorialIdForType(typeof(JpegSaver));
            settings.FactorySettings.PointsSearcherId = GetFactorialIdForType(typeof(PointsSearcherOnSpiral));
            settings.FactorySettings.WordsParserId = GetFactorialIdForType(typeof(DocxLinesParser));
            settings.FactorySettings.WordsLayouterId = GetFactorialIdForType(typeof(ReverseFrequencyWordsLayouter));
            settings.FactorySettings.WordsFiltersIds = new[]
            {
                GetFactorialIdForType(typeof(BoringWordsFilter)),
                GetFactorialIdForType(typeof(TakenPartsOfSpeechFilter))
            };
            settings.FactorySettings.WordsConvertersIds = new[]
            {
                GetFactorialIdForType(typeof(WordsToLowerConverter)),
                GetFactorialIdForType(typeof(InitialWordsFormConverter))
            };

            settings.TakenPartsOfSpeech = new[] { "v", "s" };

            GeneratorTest(readFrom, writeTo, expectedPath);
        }

        private void GeneratorTest(string readFrom, string writeTo, string expectedPath)
        {
            var expected = File.ReadAllBytes(expectedPath);

            sut.TryGenerate(readFrom, writeTo);

            var actual = File.ReadAllBytes(writeTo);
            actual.Should().Equal(expected);

            File.Delete(writeTo);
        }

        private string GetFactorialIdForType(Type type) =>
            type
                .GetCustomAttribute<FactorialAttribute>()
                .FactorialId;
    }
}