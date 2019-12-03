using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Filters;

namespace TagsCloudContainer.Tests.Filters
{
    [TestFixture]
    public class BoringFilter_Should
    {
        private BoringFilter boringFilter;

        [SetUp]
        public void SetUp()
        {
            boringFilter = new BoringFilter(new []{"Boring"});
        }

        [Test]
        public void Filtering_WhenFilterHavntBoringWord_ReturnThisCollection()
        {
            var boringFilter = new BoringFilter(new string[0]);
            var collection = new[]{"NonBoring","Boring","NonBoring"};
            boringFilter.Filtering(collection).Should().BeEquivalentTo(collection);
        }

        [Test]
        public void Filtering_WhenCollectionHavntBoringWord_ReturnThisCollection()
        {
            var collection = new[]{"NonBoring","NonBoring"};
            boringFilter.Filtering(collection).Should().BeEquivalentTo(collection);
        }
        
        [Test]
        public void Filtering_WhenBoringWordWithDifferentRegister_ShouldntContainBoringWord()
        {
            var collection = new[]{"NonBoring","boring","Boring","NonBoring"};
            boringFilter.Filtering(collection).Should().NotContain("boring").And.NotContain("Boring");
        }

        [Test]
        public void Filtering_WhenBoringWordIsNull_ThrowArgumentException()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Action action = () => { new BoringFilter(null); };
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Filtering_WhenCollectionIsNull_ThrowArgumentException()
        {
            Action action = () => { boringFilter.Filtering(null); };
            action.Should().Throw<ArgumentException>();
        }
    }
}