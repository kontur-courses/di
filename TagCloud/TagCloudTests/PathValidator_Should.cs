using FluentAssertions;
using NUnit.Framework;
using TagCloudApp;

namespace TagCloudTests
{
    [TestFixture]
    public class PathValidator_Should
    {
        [TestCase(@"/abc/cde")]
        [TestCase(@"/abc/cde/1.txt")]
        [TestCase(@"C:\abc\cde\1.txt")]
        [TestCase(@".")]
        [TestCase(@"/.././..")]
        public void ReturnTrue_WhenPathIsValid(string path)
        {
            new PathValidator().Validate(path)
                               .Should()
                               .BeTrue();
        }

        [TestCase(
            @"/eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee",
            TestName = "path is too long")]
        [TestCase(@"/abc/:cde/1.txt", TestName = "path contains colon")]
        public void ReturnFalse_When(string path)
        {
            new PathValidator().Validate(path)
                               .Should()
                               .BeFalse();
        }
    }
}
