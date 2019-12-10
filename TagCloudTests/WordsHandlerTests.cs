using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TagCloud;

namespace TagCloudTests
{
    [TestFixture]
    public class WordsHandlerTests
    {
        private string pathToBoringWords;
        private string pathToReadWords;
        private IWordsHandler wordsHandler;
        private WindsorContainer container;

        [SetUp]
        public void SetUp()
        {
            pathToReadWords = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}\\ForParsingTests.txt";
            pathToBoringWords = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}\\BoringWords.txt";
            CreateBoringWordsFile(pathToBoringWords);
            CreateTestFile(pathToReadWords);
            container = TagCloud.Program.GetContainer();
            wordsHandler = container.Resolve<IWordsHandler>();
        }

        private void CreateTestFile(string path)
        {
            var lines = new string[] { "i", "I", "i", "I", "Boat", "BoaT", "BoAt", "BOAT", "in", "IN" };
            var f = File.Create($"{Directory.GetParent(Environment.CurrentDirectory).FullName}\\ForParsingTests.txt");
            f.Close();
            using (var sw = new StreamWriter(path))
            {
                foreach (var line in lines)
                    sw.WriteLine(line);
            }
        }

        private void CreateBoringWordsFile(string path)
        {
            var lines = new string[] {"in"};
            var f = File.Create($"{Directory.GetParent(Environment.CurrentDirectory).FullName}\\ForParsingTests.txt");
            f.Close();
            using (var sw = new StreamWriter(path))
            {
                foreach (var line in lines)
                    sw.WriteLine(line);
            }
        }

        [Test]
        public void WordsHandler_Should_ThrowsException_When_FileNotExist()
        {
            Func<Dictionary<string, int>> getDictionary = () => wordsHandler.GetWordsAndCount("kkk");
            getDictionary.Should().Throw<FileNotFoundException>().WithMessage("Файла не существует");
        }

        [Test]
        public void WordsHandler_Should_ParseWordsCorrectly()
        {
            var expectedDictionary = new Dictionary<string, int>() { { "i", 4 }, { "boat", 4 }, { "in", 2 } };
            wordsHandler.GetWordsAndCount(pathToReadWords).Should().BeEquivalentTo(expectedDictionary);
        }

        [Test]
        public void WordsHandler_Should_ConvertWordsCorrectly()
        {
            var expectedDictionary = new Dictionary<string, int>() { { "i", 4 }, { "boat", 4 } };
            var primaryCollection = wordsHandler.GetWordsAndCount(pathToReadWords);
            wordsHandler.Conversion(primaryCollection).Should().BeEquivalentTo(expectedDictionary);
        }
    }
}
