using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Utility.Container;
using TagCloud.Utility.Models.Tag;

namespace TagCloud.Tests.ForTagUtility
{
    [TestFixture]
    public class TagReader_Should
    {
        private readonly IContainer container = ContainerConfig.StandartContainer;

        [Test]
        public void ReadTags()
        {
            var reader = container.Resolve<TagReader>();

            var tags = reader.ReadTags(new[]
            {
                "Big", "Big", "Big", "Big", "Big", "Big", "Big", "Big", "Big", "Big",
                "Average", "Average", "Average", "Average", "Average", "Average",
                "Small"
            });

            tags.Should().HaveCount(3)
                .And.Contain(item => item.Word == "Big" && item.FontSize == 35)
                .And.Contain(item => item.Word == "Average" && item.FontSize == 25)
                .And.Contain(item => item.Word == "Small" && item.FontSize == 15);
        }

        [Test]
        public void OrderTagsByFontSizeDescending()
        {
            var reader = container.Resolve<TagReader>();

            var tags = reader.ReadTags(new[]
            {
                "Big", "Big", "Big", "Big", "Big", "Big", "Big", "Big", "Big", "Big",
                "Average", "Average", "Average", "Average", "Average", "Average",
                "Small"
            });

            tags.Should().BeInDescendingOrder(t => t.FontSize);
        }
    }
}