using NUnit.Framework;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudContainer;

namespace TagsCloudContainerTest
{
    public class TextFileWordsContainerTest
    {
        private IWordReader wordReader;
        private IWordProcessor wordProcessor;

        [SetUp]
        public void InitializeServices()
        {
            wordReader = A.Fake<IWordReader>();
            wordProcessor = A.Fake<IWordProcessor>();
        }

        [Test]
        public void CheckGetWords()
        {
            var path = "pathToTextFile";
            var words = new List<string>();
            var wordsContainer = new TextFileWordsContainer(wordReader, wordProcessor, path);
            A.CallTo(() => wordReader.Read(path)).WithAnyArguments().Returns(words);

            wordsContainer.GetWords();

            A.CallTo(() => wordReader.Read(path)).MustHaveHappenedOnceExactly();
            A.CallTo(() => wordProcessor.Process(words)).MustHaveHappenedOnceExactly();
        }
    }
}
