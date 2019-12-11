using NUnit.Framework;
using System.Collections.Generic;
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
            containerBuilder.RegisterType<TextPreprocessor>()
                .As<ITextPreprocessor>();
            containerBuilder.RegisterType<ToLowerCaseProcessor>()
                .As<IWordProcessor>();
            containerBuilder.RegisterType<WordsProvider>()
                .AsSelf()
                .As<IWordsProvider>();
            containerBuilder.RegisterType<EmptyWordFilter>()
                .As<IWordFilter>();
        }

        private WordsProvider GetSimpleWordProvider()
        {
            containerBuilder.RegisterType<BoringWordsFilter>()
                .As<IWordFilter>();
            return containerBuilder.Build().Resolve<WordsProvider>();
        }

        private WordsProvider GetWordProviderWithCustomBoringWordsFilter(string boringWordsListFilename)
        {
            var boringWordsListPath = GetFilePath(boringWordsListFilename);
            containerBuilder.Register(c =>
                new CustomBoringWordsFilter(
                    c.Resolve<ITextReader>(),
                    boringWordsListPath))
                .As<IWordFilter>();

            return containerBuilder.Build().Resolve<WordsProvider>();
        }

        private string GetFilePath(string testFileName)
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"WordsProviderTestFiles", testFileName);
        }

        [Test]
        public void ParseEmptyFile_Properly()
        {
            TestSimpleWordsProvider("EmptyFile.txt", new List<string>());
        }

        [Test]
        public void ParseWhitespaceFile_Properly()
        {
            TestSimpleWordsProvider("WhitespaceFile.txt", new List<string>());
        }

        [Test]
        public void ConvertToLowerCase_WithLowerCaseProcessor()
        {
            var expectedWords = new List<string>() { "first", "second", "third", "fourth", "fifth" };
            TestSimpleWordsProvider("VariousCaseFile.txt", expectedWords);
        }

        [Test]
        public void FilterBoringWords_WithBoringWordsFilter()
        {
            var expectedWords = new List<string>() { "first", "second", "third" };
            TestSimpleWordsProvider("BoringWordsFile.txt", expectedWords);
        }

        [Test]
        public void FilterCustomBoringWords_WhenProvided()
        {
            var wordProvider = GetWordProviderWithCustomBoringWordsFilter("BoringWordsList.txt");
            var testFilePath = GetFilePath("CustomBoringWordsFile.txt");
            var expectedWords = new List<string>() { "it's", "wrong" };

            var resultWords = wordProvider.ReadWordsFromFile(testFilePath);

            resultWords.Should().BeEquivalentTo(expectedWords);
        }

        private void TestSimpleWordsProvider(string filename, List<string> expectedWords)
        {
            var wordProvider = GetSimpleWordProvider();
            var testFilePath = GetFilePath(filename);

            var resultWords = wordProvider.ReadWordsFromFile(testFilePath);

            resultWords.Should().BeEquivalentTo(expectedWords);
        }
    }
}
