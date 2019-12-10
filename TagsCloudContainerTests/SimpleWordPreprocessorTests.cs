using Autofac;
using NUnit.Framework;
using System.Linq;
using TagsCloudContainer.Readers;
using TagsCloudContainer.TokensAndSettings;
using TagsCloudContainer.WordPreprocessors;
using FluentAssertions;

namespace TagsCloudContainerTests
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

            return result.Select(resultWord => resultWord.Word).ToArray();
        }

        [TestCase("бегают", ExpectedResult = new[] { "бегать" })]
        [TestCase("прыгают машут", ExpectedResult = new[] { "прыгать", "махать" })]
        public string[] CountWords_InitialForm(string word)
        {
            var container = containerBuilder.Build();
            var simpleWordPreprocessor = container.Resolve<IWordPreprocessor>();

            var result = simpleWordPreprocessor.WordPreprocessing(new[] { word });

            return result.Select(resultWord => resultWord.Word).ToArray();
        }

        [Test]
        public void CountWords_FromTxt()
        {
            var container = containerBuilder.Build();
            var simpleWordPreprocessor = container.Resolve<IWordPreprocessor>();
            var simpleReader = new SimpleReader(@"TagsCloudContainer\WordsRus.txt");

            var result = simpleWordPreprocessor.WordPreprocessing(simpleReader.ReadAllLines());

            result.Should().BeEquivalentTo(new[] {
                new ProcessedWord("огонь", "S"),
                new ProcessedWord("полено", "S") });
        }
    }
}
