using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.Reflection;
using System.IO;
using FluentAssertions;
using TextConfiguration;
using TextConfiguration.WordProcessors;
using TextConfiguration.TextReaders;
using TextConfiguration.WordFilters;

namespace TagsCloudApplicationTests
{
    [TestFixture]
    public class WordsProvider_Should
    {
        private ContainerBuilder containerBuilder;

        [SetUp]
        public void SetUp()
        {
            containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<RawTextReader>()
                .As<ITextReader>();
            containerBuilder.RegisterType<TextPreprocessor>();
            containerBuilder.RegisterType<ToLowerCaseProcessor>()
                .As<IWordProcessor>();
            containerBuilder.RegisterType<WordsProvider>();
        }

        public WordsProvider GetSimpleWordProvider()
        {
            containerBuilder.RegisterType<EmptyWordFilter>()
                .As<IWordFilter>();
            return containerBuilder.Build().Resolve<WordsProvider>();
        }

        public string GetFilePath(string testFileName)
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"WordsProviderTestFiles", testFileName);
        }

        [Test]
        public void ParseEmptyFile_Properly()
        {
            var wordProvider = GetSimpleWordProvider();
            var testFilePath = GetFilePath("EmptyFile.txt");

            var resultWords = wordProvider.ReadWordsFromFile(testFilePath);

            resultWords.Should().BeEquivalentTo(new List<string>());
        }

        [Test]
        public void ParseWhitespaceFile_Properly()
        {
            var wordProvider = GetSimpleWordProvider();
            var testFilePath = GetFilePath("WhitespaceFile.txt");

            var resultWords = wordProvider.ReadWordsFromFile(testFilePath);

            resultWords.Should().BeEquivalentTo(new List<string>());
        }
    }
}
