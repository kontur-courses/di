using System;
using System.IO;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualisation.App.InputStream;
using TagsCloudVisualisation.App.InputStream.FileInputStream;
using TagsCloudVisualisation.App.InputStream.FileInputStream.Exceptions;
using TagsCloudVisualisation.Tests.FileInputStream.Infrastructure;

namespace TagsCloudVisualisation.Tests.FileInputStream
{
    [TestFixture]
    public class FromFileInputWordsStreamTest
    {
        private string path = "test.txt";

        [OneTimeSetUp]
        public void StartTests()
        {
            path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            File.Create(path + "test.txt");
        }
        
        [Test]
        public void Next_ShouldReturnFalse_WhenNoNextWord()
        {
            var sut = CreateFileInputStream("");
            sut.Next().Should().BeFalse();
        }

        [Test]
        public void Creation_ShouldThrowFileNotFoundException_WhenIncorrectFilename()
        {
            Action _ = () =>
            {
                var __ = new FromFileInputWordsStream("incorrect/filename",
                    new FileEncoderСheater("", false, ""),
                    s => s.Split('\n'));
            };
            _.Should().Throw<FileNotFoundException>();
        }
        
        [Test]
        public void Creation_ShouldThrowIncorrectFileTypeException_WhenIncorrectFileType()
        {
            Action _ = () =>
                CreateFileInputStream("", fileType:".docx");
            _.Should().Throw<IncorrectFileTypeException>();
        }
        
        [Test]
        public void GetWord_ShouldThrowIncorrectCallException_WhenNoFirstCallNext()
        {
            Action _ = () =>    
                CreateFileInputStream("word").GetWord();
            _.Should().Throw<IncorrectCallException>();
        }

        [Test]
        public void GetWord_ShouldThrowEndOfStreamException_WhenNoMoreWords()
        {
            var sut = CreateFileInputStream("word");
            sut.Next();
            sut.Next();
            Action _ = () => sut.GetWord();
            _.Should().Throw<EndOfStreamException>();
        }

        [Test]
        public void Next_ShouldReturnTrue_WhenHasMoreWords()
        {
            var sut = CreateFileInputStream("word1\nword2\nword3");
            sut.Next().Should().BeTrue();
        }

        [Test]
        public void Next_ShouldMoveIterator()
        {
            var sut = CreateFileInputStream("word1\nword2\nword3");
            sut.Next().Should().BeTrue();
            sut.Next().Should().BeTrue();
            sut.Next().Should().BeTrue();
            sut.Next().Should().BeFalse();
        }

        [Test]
        public void GetWord_ShouldReturnSameWords_WhenNoCollNext()
        {
            var sut = CreateFileInputStream("word1\nword2\nword3");
            sut.Next();
            var w1 = sut.GetWord();
            sut.GetWord().Should().Be(w1);
        }
        
        [OneTimeTearDown]
        public void StopTests()
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
        private IInputWordsStream CreateFileInputStream(string text, bool existsFile = true, string fileType = "txt")
        {
            return new FromFileInputWordsStream(path + "test.txt", new FileEncoderСheater(text, existsFile, fileType),
                s => s.Split('\n'));
        }
    }
}