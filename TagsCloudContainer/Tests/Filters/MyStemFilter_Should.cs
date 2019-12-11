using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Filters;
using YandexMystem.Wrapper.Enums;

namespace TagsCloudContainer.Tests.Filters
{
    public class MyStemFilter_Should
    {
        private IFilter filter;
        [SetUp]
        public void SetUp()
        {
            filter = new MyStemFilter( new []{GramPartsEnum.Noun});
        }

        [Test]
        public void Filtering_WhenBoringWordIsNull_ThrowArgumentException()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Action action = () => { filter.Filtering(null); };
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Filtering_WhenCollectionHaveNoun_ReturnThisCollection()
        {
            var collection = new[]{"Слово","Слово"};
            filter.Filtering(collection).Should().BeEquivalentTo(collection);
        }
        
        [Test]
        public void Filtering_WhenCollectionHaventNoun_ShouldEmptyCollection()
        {
            var collection = new[]{"Проверять","Правильная", "Хорошо"};
            filter.Filtering(collection).Should().HaveCount(0);
        }

    }
}