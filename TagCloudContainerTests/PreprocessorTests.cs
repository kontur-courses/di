using System.Collections.Generic;
using System.Linq;
using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Common;
using TagsCloudContainer.Preprocessors;

namespace TagCloudContainerTests
{
    [TestFixture]
    public class TagsPreprocessorShould
    {
        private TagsPreprocessor preprocessor;

        public TagsPreprocessorShould()
        {
            var builder = new ContainerBuilder();
            PreprocessorsRegistrator.RegisterPreprocessors(builder);
            var container = builder.Build();
            preprocessor = container.Resolve<TagsPreprocessor>();
        }

        [Test]
        public void ReturnsTagsToLower()
        {
            var tags = new List<SimpleTag>
            {
                new SimpleTag("One", 1),
                new SimpleTag("Two", 2),
                new SimpleTag("ThReE", 3)
            };
            var actual = preprocessor.Process(tags);
            var expected = tags.Select(t => new SimpleTag(t.Word.ToLower(), t.Count))
                .ToList();
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void ReturnsOnlyLongTags()
        {
            var tags = new List<SimpleTag>
            {
                new SimpleTag("раз", 1),
                new SimpleTag("два", 2),
                new SimpleTag("три", 3),
                new SimpleTag("но", 1),
                new SimpleTag("и", 2),
                new SimpleTag("да", 3),
            };
            var actual = preprocessor.Process(tags);
            var expected = tags.Where(t => t.Word.Length > 2)
                .ToList();
            actual.Should().BeEquivalentTo(expected);
        }
    }
}