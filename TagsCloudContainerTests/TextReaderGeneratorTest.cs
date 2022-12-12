using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.TextReaders;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class TextReaderGeneratorTest
    {
        private TextReaderGenerator _generator;
        private string _projectDirectory;

        [SetUp]
        public void SetUp()
        {
            _generator = new TextReaderGenerator();
            _projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        }
        
        [TestCase("OneCharacters.txt")]
        [TestCase("AllWordsAreBoring.txt")]
        [TestCase("UpperCase.txt")]
        [TestCase("LowerCase.txt")]
        public void GetReader_ShouldReturnTxtReader(string fileName)
        {
            var reader = _generator.GetReader($"{_projectDirectory}\\TextFiles\\{fileName}");
            reader.Should().BeOfType<TxtReader>();
        }
        
        [TestCase("OneCharacters.docx")]
        [TestCase("AllWordsAreBoring.docx")]
        [TestCase("UpperCase.docx")]
        public void GetReader_ShouldReturnWordReader(string fileName)
        {
            var reader = _generator.GetReader($"{_projectDirectory}\\TextFiles\\{fileName}");
            reader.Should().BeOfType<WordReader>();
        }

        [TestCase("Example.ini")]
        [TestCase("Example.h")]
        [TestCase("Example.cpp")]
        [TestCase("Example.cs")]
        public void GetReader_ShouldThrowArgumentException(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            Action action = () => _generator.GetReader(fileName);
            action.Should().Throw<ArgumentException>().WithMessage($"This file format is not supported: {extension}");
        }
    }
}