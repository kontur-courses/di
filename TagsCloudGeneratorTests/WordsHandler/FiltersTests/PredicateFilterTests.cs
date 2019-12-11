using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudGenerator.WordsHandler.Filters;

namespace TagsCloudGeneratorTests.WordsHandler.FiltersTests
{
    public class PredicateFilterTests
    {
        [Test]
        public void Filter_ArgumentIsNull_ShouldThrowArgumentNullException()
        {
            Dictionary<string, int> data = null;
            var filter = new PredicateFilter(x=> x.Value<5);

            Action act = () => filter.Filter(data);

            act.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Filter_ShouldApplyPredicateForAllElements()
        {
            var data = new Dictionary<string, int>
            {
                ["fish"] = 2,
                ["sun"] = 1,
                ["cat"] = 40,
                ["sofa"] = 22,
                ["garden"] = 4
            };
            var expected = new Dictionary<string, int>
            {
                ["fish"] = 2,
                ["sun"] = 1,
                ["garden"] = 4
            };
            var filter = new PredicateFilter(x => x.Value > 5);

            var actual = filter.Filter(data);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}