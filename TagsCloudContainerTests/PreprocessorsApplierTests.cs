using FluentAssertions;
using NUnit.Framework;
using System.IO;
using System.Linq;
using TagsCloudContainer;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainerTests
{
    internal class PreprocessorsApplierTests
    {
        private string textsFolder;
        private IParser parser;
        private IPreprocessorsApplier applier;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            textsFolder = Path.GetFullPath(@"..\..\..\texts");
            parser = new TxtParser();
            applier = new PreprocessorsApplier(SettingsProvider.GetSettings());
        }

        [Test]
        public void Should_ApplyPreprocessorsCorrectly()
        {
            var path = Path.Combine(textsFolder, "test.txt");
            var parsed = parser.Parse(path);

            var result = applier.ApplyPreprocessors(parsed).ToList();

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
