using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using TagCloud;
using TagCloud.IServices;
using TagCloud.Models;

namespace TagCloudTests
{
    [TestFixture]
    public class ParserTests
    {
        private string pathToBoringWords;
        private string pathToReadWords;
        private IWordsHandler wordsHandler;
        private WindsorContainer container;
        private ITagCollectionFactory tagCollectionFactory;
        private ImageSettings imageSettings;

        [SetUp]
        public void SetUp()
        {
            pathToReadWords = SetUpMethods.GetPathToWordsToRead();
            pathToBoringWords = SetUpMethods.GetPathToBoringWords();
            SetUpMethods.CreateFile(pathToBoringWords);
            SetUpMethods.WriteLinesInFile(pathToBoringWords,"in");
            SetUpMethods.CreateFile(pathToReadWords);
            SetUpMethods.WriteLinesInFile(pathToReadWords, "i ", "   I", "i     ", "I", "Boat  ", "   BoaT", "BoAt", "BOAT", "in", "IN");
            container = TagCloud.Program.GetContainer();
            wordsHandler = container.Resolve<IWordsHandler>();
            tagCollectionFactory = container.Resolve<ITagCollectionFactory>();
            imageSettings = new ImageSettings(300, 300, "Arial", "ShadesOfPink");
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

        [Test]
        public void WordsToTagsParser_Should_NotThrows_When_Parse()
        {
            Action action =  () => tagCollectionFactory.Create(imageSettings, pathToReadWords);
            action.Should().NotThrow();
        }

        [Test]
        public void WordsToTagsParser_Should_ParseWordsCorrectly()
        {
            var collection = tagCollectionFactory.Create(imageSettings, pathToReadWords);
            var font = new Font("Arial", 24, FontStyle.Bold);
            var expectedCollection = new List<Tag>(){new Tag("i",4,font),
                new Tag("boat",4,font)};
            collection.Should().BeEquivalentTo(expectedCollection);
        }
    }
}
