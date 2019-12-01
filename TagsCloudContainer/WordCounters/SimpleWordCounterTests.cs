using Autofac;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer.WordFilters;
using TagsCloudContainer.WordPreprocessors;

namespace TagsCloudContainer.WordCounters
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

            var words = new string[] { "a", "d", "d", "j", "a", "h", "a" };
            var expect = new WordToken[]
            {
                new WordToken("a", 3),
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
