using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.FileReaders;

namespace TagsCloudContainerTests
{
    public class TxtFileReader_Tests
    {
        private TxtFileReader txtFileReader;
        private const string FileName = "TxtTestFile.txt";
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            txtFileReader = new TxtFileReader();
            File.WriteAllText(FileName, "firstWord\nsecondWord\nthirdWord");
        }
        
        [Test]
        public void ReadWordsFromFile_ThrowsFileNotFoundException_WhenFileDoesNotExist()
        {
            Action act = () => txtFileReader.ReadWordsFromFile("fakeFile");

            act.Should().Throw<FileNotFoundException>();
        }
        
        [Test]
        public void ReadWordsFromFile_WorksCorrectlyWithTxt()
        {
            var expectedWords = new[] { "firstWord", "secondWord", "thirdWord" };
            
            var words = txtFileReader.ReadWordsFromFile(FileName);

            words.Should().BeEquivalentTo(expectedWords);
        }
    }
}