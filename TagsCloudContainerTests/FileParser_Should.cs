using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Algorithm;

namespace TagsCloudContainerTests
{
    public class FileParser_Should
    {
        private FileParser parser;

        [SetUp]
        public void SetUp() 
        {  
            parser = new FileParser();
        }

        [Test]
        public void ThrowException_IfMoreThanOneWordInLine()
        {
            var incorrectDataFilePath = GetProjectDirectory() + @"\src\incorrectData.txt";

            Action action = () => parser.ReadWordsInFile(incorrectDataFilePath);

            action.Should().Throw<Exception>();
        }

        [Test]
        public void ReduceWordsToLowercase()
        {
            var expectedList = new List<string> { "привет", "занятие", "яблоко", "домашка" };
            var uppercaseDataFilePath = GetProjectDirectory() + @"\src\upperCase.txt";
            var words = parser.ReadWordsInFile(uppercaseDataFilePath);

            words.Should().BeEquivalentTo(expectedList);
        }

        [Test]
        public void RemoveSpacesFromTheBeginningAndEndOfline()
        {
            var expectedList = new List<string> { "привет", "занятие", "яблоко", "домашка" };
            var uppercaseDataFilePath = GetProjectDirectory() + @"\src\trimData.txt";
            var words = parser.ReadWordsInFile(uppercaseDataFilePath);

            words.Should().BeEquivalentTo(expectedList);
        }

        [Test]
        public void ParseEmptyList_WhenFileIsEmpty()
        {
            var emptyDataFilePath = GetProjectDirectory() + @"\src\emptyData.txt";
            var words = parser.ReadWordsInFile(emptyDataFilePath);

            words.Should().BeEmpty();
        }


        public string GetProjectDirectory()
        {
            var binDirectory = AppContext.BaseDirectory;
            var projectDirectory = Directory.GetParent(binDirectory).Parent.Parent.Parent.FullName;

            return projectDirectory;
        }
    }
}
