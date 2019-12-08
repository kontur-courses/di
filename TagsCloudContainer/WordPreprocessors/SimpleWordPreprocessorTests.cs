using Autofac;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.WordPreprocessors
{
    [TestFixture]
    class SimpleWordPreprocessorTests
    {
        private ContainerBuilder containerBuilder;

        [SetUp]
        public void SetUp()
        {
            containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<SimpleWordPreprocessor>().As<IWordPreprocessor>();
        }

        [TestCase("aaa", ExpectedResult = new[] { "aaa" })]
        [TestCase("aaA", ExpectedResult = new[] { "aaa" })]
        [TestCase("AAA", ExpectedResult = new[] { "aaa" })]
        public string[] CountWords(string word)
        {
            var container = containerBuilder.Build();
            var simpleWordPreprocessor = container.Resolve<IWordPreprocessor>();

            var result = simpleWordPreprocessor.WordPreprocessing(word);

            return result;
        }
    }
}
