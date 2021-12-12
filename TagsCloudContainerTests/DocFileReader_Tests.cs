using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.FileReaders;

namespace TagsCloudContainerTests
{
    public class DocFileReader_Tests
    {
        private DocFileReader docFileReader = new DocFileReader();
        private const string FileName = "../../../DocTestFile.docx";
        
        [Test]
        public void ReadWordsFromFile_ThrowsFileNotFoundException_WhenFileDoesNotExist()
        {
            Action act = () => docFileReader.ReadWordsFromFile("fakeFile");

            act.Should().Throw<FileNotFoundException>();
        }
        
        [Test]
        public void ReadWordsFromFile_WorksCorrectlyWithDoc()
        {
            var expectedWords = new[] { "firstWord", "secondWord", "thirdWord" };
            
            var words = docFileReader.ReadWordsFromFile(FileName);

            words.Should().BeEquivalentTo(expectedWords);
        }
    }
}