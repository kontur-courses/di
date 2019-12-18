using System.Collections.Generic;
using NUnit.Framework;
using FakeItEasy;
using FluentAssertions;

namespace TagsCloudContainer
{
    [TestFixture]
    public class TextHandlerTests
    {
        [Test]
        public void TextHandler_GivesRightFrequencyDict_OnSingleWords()
        {
            var lines = new[] {"Hi", "my", "dear", "friend"};
            var textReader = A.Fake<ITextReader>();
            A.CallTo(() => textReader.GetLines()).Returns(lines);
            var wordsEliminator = A.Fake<IDullWordsEliminator>();
            A.CallTo(() => wordsEliminator.IsDull(null)).WithAnyArguments().Returns(false);
            var defaultTextHandler = new TextHandler(textReader, wordsEliminator);

            var dict = defaultTextHandler.GetWordsFrequencyDict();

            foreach (var word in lines)
            {
                dict.Should().ContainKey(word.ToLower());
                dict.Should().Contain(new KeyValuePair<string, int>(word.ToLower(), 1));
            }
        }

        [Test]
        public void TextHandler_GivesRightFrequencyDict_OnMultiplyWords()
        {
            var lines = new[] { "Hi", "my", "dear", "friend", "my", "friend", "friend" };
            var textReader = A.Fake<ITextReader>();
            A.CallTo(() => textReader.GetLines()).Returns(lines);
            var wordsEliminator = A.Fake<IDullWordsEliminator>();
            A.CallTo(() => wordsEliminator.IsDull(null)).WithAnyArguments().Returns(false);
            var defaultTextHandler = new TextHandler(textReader, wordsEliminator);

            var dict = defaultTextHandler.GetWordsFrequencyDict();

            dict.Should().ContainKey("hi");
            dict.Should().Contain(new KeyValuePair<string, int>("hi", 1));
            dict.Should().ContainKey("my");
            dict.Should().Contain(new KeyValuePair<string, int>("my", 2));
            dict.Should().ContainKey("dear");
            dict.Should().Contain(new KeyValuePair<string, int>("dear", 1));
            dict.Should().ContainKey("friend");
            dict.Should().Contain(new KeyValuePair<string, int>("friend", 3));
        }

        [Test]
        public void TextHandler_SplittedLinesIntoWords()
        {
            var lines = new[] { "Hi my friend.", "Hi, my friend!" };
            var textReader = A.Fake<ITextReader>();
            A.CallTo(() => textReader.GetLines()).Returns(lines);
            var wordsEliminator = A.Fake<IDullWordsEliminator>();
            A.CallTo(() => wordsEliminator.IsDull(null)).WithAnyArguments().Returns(false);
            var defaultTextHandler = new TextHandler(textReader, wordsEliminator);

            var dict = defaultTextHandler.GetWordsFrequencyDict();

            dict.Should().ContainKey("hi");
            dict.Should().Contain(new KeyValuePair<string, int>("hi", 2));
            dict.Should().ContainKey("my");
            dict.Should().Contain(new KeyValuePair<string, int>("my", 2));
            dict.Should().ContainKey("friend");
            dict.Should().Contain(new KeyValuePair<string, int>("friend", 2));
        }
    }
}