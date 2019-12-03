using System.Drawing;
using TagsCloudContainer;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    public class Tests
    {
        private readonly ArgumentParser parser = new ArgumentParser();
        
        [Test]
        public void SetFilePath_IfGetFileName()
        {
            var args = new[]{"-f", "file"};

            var actual = parser.ParseArgument(args);
            actual.Path.Should().Be("file");
        }

        [Test]
        public void SetFont()
        {
            var args = new[] {"-f", "file", "-o", "Universe"};

            var actual = parser.ParseArgument(args);

            actual.Font.Should().Be(new Font("Universe", 10));
        }
    }
}