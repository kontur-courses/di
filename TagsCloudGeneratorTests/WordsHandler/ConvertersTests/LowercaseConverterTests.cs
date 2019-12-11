using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudGenerator.WordsHandler.Converters;

namespace TagsCloudGeneratorTests.WordsHandler.ConvertersTests
{
    public class LowercaseConverterTests
    {
        private readonly LowercaseConverter converter = new LowercaseConverter();

        [Test]
        public void Convert_ArgumentIsNull_ShouldThrowArgumentNullException()
        {
            Dictionary<string, int> data = null;

            Action act = () => converter.Convert(data);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Convert_AllLetterAlreadyIsLowercase_ShouldReturnInputData()
        {
            var data = new Dictionary<string, int>
            {
                ["fish"] = 2,
                ["sun"] = 1,
                ["cat"] = 40,
                ["sofa"] = 1
            };

            var actual = converter.Convert(data);

            actual.Should().BeEquivalentTo(data);
        }

        [Test]
        public void Convert_FirstLetterIsUppercase_AllLetterInWordShouldBeIsLowercase()
        {
            var data = new Dictionary<string, int>
            {
                ["Fish"] = 2, 
                ["sun"] = 1,
                ["Cat"] = 40,
                ["sofa"] = 1
            };
            var expected = new Dictionary<string, int>
            {
                ["fish"] = 2,
                ["sun"] = 1,
                ["cat"] = 40,
                ["sofa"] = 1
            };

            var actual = converter.Convert(data);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Convert_MiddleLetterIsUppercase_AllLetterInWordShouldBeIsLowercase()
        {
            var data = new Dictionary<string, int>
            {
                ["FiSh"] = 2,
                ["sun"] = 1,
                ["Cat"] = 40,
                ["sOfa"] = 1
            };
            var expected = new Dictionary<string, int>
            {
                ["fish"] = 2,
                ["sun"] = 1,
                ["cat"] = 40,
                ["sofa"] = 1
            };

            var actual = converter.Convert(data);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Convert_ComplexLetterIsUppercase_AllLetterInWordShouldBeIsLowercase()
        {
            var data = new Dictionary<string, int>
            {
                ["FiSh"] = 2,
                ["sUn"] = 1,
                ["CaT"] = 40,
                ["SOFA"] = 1
            };
            var expected = new Dictionary<string, int>
            {
                ["fish"] = 2,
                ["sun"] = 1,
                ["cat"] = 40,
                ["sofa"] = 1
            };

            var actual = converter.Convert(data);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Convert_LastLetterIsUppercase_AllLetterInWordShouldBeIsLowercase()
        {
            var data = new Dictionary<string, int>
            {
                ["Fish"] = 2,
                ["suN"] = 1,
                ["caT"] = 40,
                ["sofa"] = 1
            };
            var expected = new Dictionary<string, int>
            {
                ["fish"] = 2,
                ["sun"] = 1,
                ["cat"] = 40,
                ["sofa"] = 1
            };

            var actual = converter.Convert(data);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}