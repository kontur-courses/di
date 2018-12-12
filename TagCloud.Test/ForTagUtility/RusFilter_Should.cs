using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Utility.Container;
using TagCloud.Utility.Models.WordFilter;

namespace TagCloud.Tests.ForTagUtility
{
    [TestFixture]
    public class RusFilter_Should
    {
        private readonly IContainer container = ContainerConfig.StandartContainer;

        [Test]
        public void FilterWords()
        {
            var filter = container.Resolve<RusFilter>();

            var result = filter.FilterWords(new[] {"большой", "маленький", "я", "сам", "1", "22"});

            result.Should().BeEquivalentTo("большой", "маленький");
        }

        [Test]
        public void AddStopWords()
        {
            var filter = container.Resolve<RusFilter>();
            filter.Add("большой");

            var result = filter.FilterWords(new[] { "большой", "маленький", "я", "сам", "1", "22" });

            result.Should().BeEquivalentTo("маленький");
        }
    }
}