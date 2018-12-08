using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudContainer.Tests
{
    public class FilterHashSet_Should
    {
        [Test]
        public void DoesNotAcceptItems_FromBlacklist()
        {
            var filterSet = new FilterHashSet<int>(FilterType.BlackList) {1, 4};

            filterSet.PassesFilter(1).Should().BeFalse();
        }

        [Test]
        public void AcceptsItems_NotFromBlacklist()
        {
            var filterSet = new FilterHashSet<int>(FilterType.BlackList) {1, 4};

            filterSet.PassesFilter(0).Should().BeTrue();
        }

        [Test]
        public void DoesNotAcceptItems_NotFromWhitelist()
        {
            var filterSet = new FilterHashSet<int>(FilterType.WhiteList) {1, 4};

            filterSet.PassesFilter(5).Should().BeFalse();
        }

        [Test]
        public void AcceptsItems_FromWhitelist()
        {
            var filterSet = new FilterHashSet<int>(FilterType.WhiteList) {1, 4};

            filterSet.PassesFilter(4).Should().BeTrue();
        }
    }
}
