using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudGenerator.WordsHandler.Converters;

namespace TagsCloudGeneratorTests.WordsHandler.ConvertersTests
{
    public class InitialFormConverterTests
    {
        private InitialFormConverter converter;

        [SetUp]
        public void SetUp()
        {
            var pathToBuild = Path.GetDirectoryName(typeof(InitialFormConverter).Assembly.Location);
            converter = new InitialFormConverter(Path.Combine(pathToBuild, "en-GB","index.aff"), Path.Combine(pathToBuild , "en-GB","index.dic"));
        }

        [Test]
        public void Convert_ArgumentIsNull_ShouldThrowArgumentNullException()
        {
            Dictionary<string, int> data = null;

            Action act = () => converter.Convert(data);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Convert_WordsInNonInitialForm_ShouldReturnDictionaryWithKeysInInitialForms()
        {
            var data = new Dictionary<string, int>
            {
                ["fishes"] = 2,
                ["suns"] = 1,
                ["sofa"] =42,
                ["loved"] = 40
            };
            var expected = new Dictionary<string, int>
            {
                ["fish"] = 2,
                ["sun"] = 1,
                ["sofa"] = 42,
                ["love"] = 40
            };

            var actual = converter.Convert(data);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Convert_WordInNonInitialFormAndThisWordInInitialForm_ShouldSumCountsThisWordForResult()
        {
            var data = new Dictionary<string, int>
            {
                ["fishes"] = 2,
                ["sun"] = 1,
                ["loved"] = 40,
                ["love"]=2
            };
            var expected = new Dictionary<string, int>
            {
                ["fish"] = 2,
                ["sun"] = 1,
                ["love"] = 42
            };

            var actual = converter.Convert(data);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}