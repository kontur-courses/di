using System;
using FluentAssertions;
using NUnit.Framework;
using System.IO;
using TagsCloud.PathValidators;

namespace TagsCloudTests
{
    public class PathValidator_Should
    {
        private PathValidator pathValidator;

        [SetUp]
        public void SetUp()
        {
            pathValidator = new PathValidator();
        }

        [Test]
        public void PathValidator_Should_ThrowArgumentException_WhenPathIsNull()
        {
            Action action = () => pathValidator.IsValidPath(null);
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PathValidator_Should_ReturnFalse_When_FileNotExists()
        {
            var currentFilePath = GenerateNotExistPath();
            pathValidator.IsValidPath(currentFilePath).Should().BeFalse();
        }

        [Test]
        public void PathValidator_Should_ReturnTrue_When_FileExists()
        {
            var currentFilePath = GenerateNotExistPath();
            using (var file = File.Create(currentFilePath)) { }
            pathValidator.IsValidPath(currentFilePath).Should().BeTrue();
            File.Delete(currentFilePath);
        }

        private string GenerateNotExistPath()
        {
            var rnd = new Random();
            var currentDir = Directory.GetCurrentDirectory();
            var currentFilePath = Path.Combine(currentDir, rnd.Next(100000).ToString() + ".png");
            while (File.Exists(currentFilePath))
            {
                currentFilePath = Path.Combine(currentDir, rnd.Next(100000).ToString() + ".png");
            }
            return currentFilePath;
        }
    }
}
