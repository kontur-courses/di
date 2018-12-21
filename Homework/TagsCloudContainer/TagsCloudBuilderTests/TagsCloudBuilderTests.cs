using System.IO;
using NUnit.Framework;
using TagsCloudBuilder;

namespace TagsCloudBuilderTests
{
    [TestFixture]
    public class TagsCloudBuilder_Should
    {
        [Test]
        public void CreateNewFile()
        {
            var debugPath = TestContext.CurrentContext.TestDirectory;
            var args = new string[]
            {
                $@"-i {debugPath}\testText.txt",
                $@"-o {debugPath}\sample.png"
            };

            Program.Main(args);

            Assert.That(File.Exists($@"{debugPath}\sample.png"));
        }
    }
}
