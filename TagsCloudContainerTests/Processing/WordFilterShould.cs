using System;
using System.IO;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Processing.Filtering;

namespace TagsCloudContainerTests.Processing
{
    [TestFixture]
    public class WordFilterShould
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (dir != null)
            {
                Environment.CurrentDirectory = dir;
                Directory.SetCurrentDirectory(dir);
            }
            else
                throw new NullReferenceException(
                    "Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) returns null");
        }

        [Test]
        public void FilterNullOrEmptyWords()
        {
            new DefaultFilter().Filter(new[] {"", "пример", null, "2"}).Should().BeEquivalentTo("пример", "2");
        }

        [Test]
        public void FilterCommonWords()
        {
            var words = new[]
            {
                "он", "второй", "и", "пример", "Павел", "не", "агрегатор", "идти", "пре", "Павел"
            };

            new CommonWordsFilter().Filter(words).Should().BeEquivalentTo("Павел", "пример", "агрегатор", "идти", "Павел");
        }

        [Test]
        public void FilterBlackListedWords()
        {
            var blackList = new[] {"преисподняя", "чернь", "ад"};
            var words = new[] {"преисподняя", "пляж", "чернь", "ад", "солнце"};

            new BlackListFilter(blackList).Filter(words).Should().BeEquivalentTo("пляж", "солнце");
        }
    }
}