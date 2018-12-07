using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Input;

namespace TagsCloudContainerTests.Input
{
    [TestFixture]
    public class FileReaderShould
    {
        [Test]
        public void ReadTxt()
        {
            var path = Path.GetTempFileName();
            File.WriteAllText(path, "Test text. End.");

            new TxtReader().Read(path).Should().Be("Test text. End.");
        }
    }
}