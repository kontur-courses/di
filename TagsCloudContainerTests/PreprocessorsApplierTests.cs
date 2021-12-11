using FluentAssertions;
using NUnit.Framework;
using System.IO;
using System.Linq;
using TagsCloudContainer;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainerTests
{
    internal class ApplierTests
    {
        private string textsFolder;
        private IParser parser;
        private IPreprocessorsApplier preprocessorsApplier;
        private IFiltersApplier filtersApplier;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            textsFolder = Path.GetFullPath(@"..\..\..\texts");
            parser = new TxtParser();
            var settings = SettingsProvider.GetSettings();
            preprocessorsApplier = new PreprocessorsApplier(settings);
            filtersApplier = new FiltersApplier(settings);
        }

        [Test]
        public void Should_ApplyPreprocessorsCorrectly()
        {
            var path = Path.Combine(textsFolder, "test.txt");
            var parsed = parser.Parse(path);
            var preprocessed = preprocessorsApplier.ApplyPreprocessors(parsed);

            var result = filtersApplier.ApplyFilters(preprocessed).ToArray();

            var expected = new[] {
                "music",
                "music",
                "music",
                "guitar",
                "guitar",
                "piano",
                "string",
                "banjo"
            };

            result.Should().BeEquivalentTo(expected);
        }
    }
}
