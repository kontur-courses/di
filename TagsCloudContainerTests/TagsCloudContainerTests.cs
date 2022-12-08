using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class TagsCloudContainerTests
    {
        private string defaultPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\Cloud.png");

        [TearDown]
        public void CleanUp()
        {
            File.Delete(defaultPath);
        }
        [Test]
        public void Main_ShouldDrawAPicture()
        {
            Program.Main(new string[]{});

            File.Exists(Path.Combine(defaultPath)).Should().BeTrue();
        }
    }
}