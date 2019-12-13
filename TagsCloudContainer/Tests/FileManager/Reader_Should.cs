using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.FileManager;

namespace TagsCloudContainer.Tests.FileManager
{
    [TestFixture]
    public class Reader_Should
    {
        private FileReader fileReader;

        [SetUp]
        public void SetUp()
        {
            fileReader = new FileReader();
        }

        [Test]
        public void Read_ShouldReturnThatWrittenInFile()
        {
            var fileName = fileReader.MakeFile();
            var text =  "Текст просто текст";
            fileReader.WriteInFile(fileName,text);
            fileReader.Read(fileName).Should().Be(text);
        }
    }
}