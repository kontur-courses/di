using Autofac;
using NUnit.Framework;
using System;
using TagsCloudContainer.Readers;

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

        [TestCase("Олень", ExpectedResult = new[] { "олень" })]
        [TestCase("лЕС", ExpectedResult = new[] { "лес" })]
        [TestCase("ДОРОГА", ExpectedResult = new[] { "дорога" })]
        public string[] CountWords_ToLower(string word)
        {
            var container = containerBuilder.Build();
            var simpleWordPreprocessor = container.Resolve<IWordPreprocessor>();

            var result = simpleWordPreprocessor.WordPreprocessing(new[] { word });

            return result;
        }

        [TestCase("бегают", ExpectedResult = new[] { "бегать" })]
        [TestCase("прыгают машут", ExpectedResult = new[] { "прыгать", "махать" })]
        public string[] CountWords_InitialForm(string word)
        {
            var container = containerBuilder.Build();
            var simpleWordPreprocessor = container.Resolve<IWordPreprocessor>();

            var result = simpleWordPreprocessor.WordPreprocessing(new[] { word });

            return result;
        }

        [Test]
        public void CountWords_FromTxt()
        {
            var container = containerBuilder.Build();
            var simpleWordPreprocessor = container.Resolve<IWordPreprocessor>();
            var simpleReader = new SimpleReader(@"E:\Projects\Shpora1\di\TagsCloudContainer\WordsRus.txt");

            var result = simpleWordPreprocessor.WordPreprocessing(simpleReader.ReadAllLines());

            Assert.AreEqual(new[] { "огонь", "а", "а", "полено" }, result);
        }
    }
}
