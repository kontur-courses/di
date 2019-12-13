using System;
using FluentAssertions;
using NUnit.Framework;
using TagsCloud.FileReader;
using System.IO;
using System.Collections.Generic;
using TagsCloud.PathValidators;

namespace TagsCloudTests
{
    class TxtReader_Should
    {
        private TxtReader txtReader;

        [SetUp]
        public void SetUp()
        {
            var pathValidator = new PathValidator();
            txtReader = new TxtReader(pathValidator);
        }

        [Test]
        public void ReadFile_Should_ThrowArgumentException_When_FileNotExists()
        {
            var currentFilePath = GenerateNotExistPath();
            Action action = () => txtReader.ReadFile(currentFilePath);
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ReadFile_Should_ReturnFileContent()
        {
            var currentFilePath = GenerateNotExistPath();
            var words = new List<string>() { "съешь", "ещё", "этих", "мягких", "французских", "булок", "да", "выпей", "чаю" };
            var inputString = string.Join(Environment.NewLine, words);
            File.WriteAllText(currentFilePath, inputString);
            txtReader.ReadFile(currentFilePath).Should().BeEquivalentTo(inputString);
            File.Delete(currentFilePath);
        }

        private string GenerateNotExistPath()
        {
            var rnd = new Random();
            var currentDir = Directory.GetCurrentDirectory();
            var currentFilePath = Path.Combine(currentDir, rnd.Next(100000).ToString() + ".txt");
            while (File.Exists(currentFilePath))
            {
                currentFilePath = Path.Combine(currentDir, rnd.Next(100000).ToString() + ".txt");
            }
            return currentFilePath;
        }
    }
}
