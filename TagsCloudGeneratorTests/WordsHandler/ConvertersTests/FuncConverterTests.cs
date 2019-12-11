using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudGenerator.WordsHandler.Converters;

namespace TagsCloudGeneratorTests.WordsHandler.ConvertersTests
{
    public class FuncConverterTests
    {
        [Test]
        public void Convert_ArgumentIsNull_ShouldThrowArgumentNullException()
        {
            Dictionary<string, int> data = null;
            var converter = new FuncConverter(x => x);

            Action act = () => converter.Convert(data);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Convert_ShouldApplyFuncForAllElements()
        {
            var data = new Dictionary<string, int>
            {
                ["fish"] = 2,
                ["sun"] = 1,
                ["cat"] = 40,
                ["sofa"] = 1
            };
            var expected = new Dictionary<string, int>
            {
                ["new fish"] = -3,
                ["new sun"] = -4,
                ["new cat"] = 35,
                ["new sofa"] = -4
            };
            var converter = new FuncConverter(x => new KeyValuePair<string, int>("new " + x.Key, x.Value - 5));

            var actual = converter.Convert(data);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}