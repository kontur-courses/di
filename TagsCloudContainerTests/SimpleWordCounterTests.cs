using Autofac;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TokensAndSettings;
using TagsCloudContainer.WordCounters;
using TagsCloudContainer.WordFilters;
using TagsCloudContainer.WordPreprocessors;

namespace TagsCloudContainerTests
{
    [TestFixture]
    class SimpleWordCounterTests
    {
        [Test]
        public void CountWords()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<SimpleWordCounter>().As<IWordCounter>();
            containerBuilder.RegisterType<SimpleWordFilter>().As<IWordFilter>();
            containerBuilder.RegisterType<SimpleWordPreprocessor>().As<IWordPreprocessor>();

            var container = containerBuilder.Build();

            var words = new[] {
                new ProcessedWord("a", "v"),
                new ProcessedWord("a", "v"),
                new ProcessedWord("d", "v"),
                new ProcessedWord("d", "v"),
                new ProcessedWord("j", "v"),
                new ProcessedWord("a", "v"),
                new ProcessedWord("h", "v"),
                new ProcessedWord("a", "v") };
            var expect = new WordToken[]
            {
                new WordToken("a", 4),
                new WordToken("d", 2),
                new WordToken("j", 1),
                new WordToken("h", 1),
            };

            var counter = container.Resolve<IWordCounter>();

            var result = counter.CountWords(words);

            result.Should().BeEquivalentTo(expect);
        }
    }
}
